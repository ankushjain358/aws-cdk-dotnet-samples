using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwsCdkDotnetSamples
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            
            // Uncomment this to deploy static site resources including S3 & CloudFront
            new StaticSiteWithS3AndCloudFrontStack(app, "StaticSite_Stack");
            
            app.Synth();
        }
    }
}
