using Amazon.CDK;
using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace AwsCdkDotnetSamples
{
    internal class StaticSiteWithS3AndCloudFrontStack : Stack
    {
        internal StaticSiteWithS3AndCloudFrontStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // 1. Create S3 bucket
            var siteBucket = new Bucket(this, "Bucket", new BucketProps
            {
                Versioned = false,
                PublicReadAccess = false,
                BlockPublicAccess = BlockPublicAccess.BLOCK_ALL
            });

            // 2. Creating the CloudFront distribution that serves the files from the S3 bucket
            var behavior = new Behavior();
            behavior.IsDefaultBehavior = true;
            behavior.ViewerProtocolPolicy = ViewerProtocolPolicy.REDIRECT_TO_HTTPS;

            var originAccessIdentity = new OriginAccessIdentity(this, "OAI");
            siteBucket.GrantRead(originAccessIdentity);

            var distribution = new CloudFrontWebDistribution(this, "WebDistribution", new CloudFrontWebDistributionProps
            {
                PriceClass = PriceClass.PRICE_CLASS_ALL,
                ErrorConfigurations = new CfnDistribution.ICustomErrorResponseProperty[]
                {
                    new CfnDistribution.CustomErrorResponseProperty
                    {
                        ErrorCode = 403,
                        ResponseCode = 200,
                        ResponsePagePath = "/index.html"
                    },
                    new CfnDistribution.CustomErrorResponseProperty
                    {
                        ErrorCode = 404,
                        ResponseCode = 200,
                        ResponsePagePath = "/index.html"
                    }
                },
                OriginConfigs = new ISourceConfiguration[]
                {
                    new SourceConfiguration
                    {
                        S3OriginSource = new S3OriginConfig
                        {
                            S3BucketSource = siteBucket,
                            OriginAccessIdentity = originAccessIdentity
                        },
                        Behaviors = new Behavior[] { behavior }
                    }
                }
            });
        }
    }
}
