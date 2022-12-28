using Amazon.CDK;
using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Deployment;
using Constructs;
using System.Net.Sockets;

namespace AwsCdkDotnetSamples
{
    internal class StaticSiteWithS3AndCloudFrontStack : Stack
    {
        internal StaticSiteWithS3AndCloudFrontStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // 1. Create S3 bucket
            var siteBucket = new Bucket(this, "Bucket", new BucketProps
            {
                BucketName = id,
                Versioned = false,
                PublicReadAccess = true,
            });

            // 2. Creating the CloudFront distribution that serves the files from the S3 bucket
            var behavior = new Behavior();
            behavior.IsDefaultBehavior = true;

            var originAccessIdentity = new OriginAccessIdentity(this, "OAI");
            siteBucket.GrantRead(originAccessIdentity);

            var distribution = new CloudFrontWebDistribution(this, "WebDistribution", new CloudFrontWebDistributionProps
            {
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
