# AWS CDK .NET Samples

This repository contains CDK samples for commonly used architecture patterns.

## Pre-requisite (Bootstrap AWS environment)
You need to provision a few AWS resources in advance to deploy a CDK app in an environment. Those resources include:
- **Amazon S3** bucket for storing CloudFormation files
- **IAM roles** that grant permissions needed to perform deployments
The process of provisioning these initial resources is called bootstrapping.

Bootstrapping is required only once per environment, where the *environment* is a combination of the target AWS account & region into which the stack is intended to be deployed.

To bootstrap, your AWS environment for CDK deployment, run the below command.
```
cdk bootstrap
```

## How to deploy CDK stack - Step by Step
1. Clone this repo `git clone https://github.com/ankushjain358/aws-cdk-dotnet-samples.git`
2. Open solution in Visual Studio or Visual Studio Code.
3. Go to `Program.cs` file, and uncomment the stack that you want to deploy.
4. The code should look like below:
    ```
    public static void Main(string[] args)
    {
        var app = new App();
            
        // Uncomment this to deploy static site resources including S3 & CloudFront
        new StaticSiteWithS3AndCloudFrontStack(app, "StaticSite_Stack");
            
        app.Synth();
    }
    ```
5. Build the solution
    ```
    cd src
    dotnet build
    ```
4. Run the following command to deploy the CDK stack.
    ```
    cdk deploy
    ```

## Available CDK Stacks
### 1. Static Site With S3 and CloudFront
This stack creates an S3 bucket and CloudFront distribution. S3 bucket is privately accessed by CloudFront using Origin Access Idenity.

- Stack location: `/static-site-s3-cloudfront/StaticSiteWithS3AndCloudFrontStack.cs`
- Primary Resources: 
    - S3 bucket
    - CloudFront distribution

## References
- [Getting Started with AWS CDK using .NET](https://coderjony.com/blogs/getting-started-with-aws-cdk-using-net)
- [What does CDK Diff do in AWS CDK](https://bobbyhadz.com/blog/what-does-cdk-diff-do)
