# Live Debugger 

This document aims to provide instructions to setting up a live debugger from scratch. It also briefly expands on why this software is being developed and how it would benefit developers, with full information available in the dissertation.

### Table of contents

- [Live Debugger](#live-debugger)
    + [Table of contents](#table-of-contents)
  * [Debugging](#debugging)
  * [Pipeline Debugging](#pipeline-debugging)
    + [What is a pipeline?](#what-is-a-pipeline-)
    + [How does a pipeline work?](#how-does-a-pipeline-work-)
    + [The problem](#the-problem)
    + [Project goals](#project-goals)
  * [Set-up Instructions](#set-up-instructions)
    + [Amazon Web Services](#amazon-web-services)
    + [GitHub Webhook](#github-webhook)
    + [Jenkins Set-Up](#jenkins-set-up)

<small><i><a href='http://ecotrust-canada.github.io/markdown-toc/'>Table of contents generated with markdown-toc</a></i></small>


## Debugging

A debugger is a program that can examine the state of your program while your program is running. A traditional debugger can halt execution when it reaches a particular place in the code, set by a breakpoint, and examine the values of the variables in the program at that time. It can also execute code step-by-step, one line at a time.

The way a debugger is implemented will differ based on what OS is in use and what the user is debugging. Generally, a user can attach a debugger to a process either by its PID or name (a lookup is performed to find the PID) and initiate the debugging session via a system call. If that is successful, the debugger will enter an event loop, similar to a UI system, but instead of events coming from the lower-level system, the OS will generate events based on triggers in the process being debugged – eg breakpoint, exception.

## Pipeline Debugging
### What is a pipeline?
A CI/CD pipeline is a series of steps that must be performed in order to deliver a new version of software. A CI/CD pipeline introduces monitoring and automation to improve the process of application development, particularly at the integration and testing phases, as well as during delivery and deployment. Although it is possible to manually execute each of the steps of a CI/CD pipeline, the true value of CI/CD pipelines is realized through automation. This project focuses on the CI aspect of a pipeline.

### How does a pipeline work?
Assuming a pipeline is integrated with a Git repository, the pipeline will be triggered when some change is made to that repo, e.g., code is pushed. The definitions for the steps the pipeline has to achieve are housed in a .yaml file, which usually are the container definition and build, lint, test, deploy stages. Within the specified container, the pipeline retrieves all files from the Git repository, including the .yaml file. A build is created according to the given dependencies, so that the test and deploy stages will work the same, regardless of where the command is executed from.

### The problem
Problems arise when changes are needed to the pipeline definitions – the .yaml file. In order for them to be tested, the pipeline needs to run every time, and more importantly, all steps need to run. To amend the yaml file based on feedback from previous runs, it has to be changed locally and pushed, triggering the pipeline and executing all the steps. This will result in a lot of waiting time and an inefficient process, as the developer is idle and has to wait for the pipeline to finish. This is particularly problematic when the scenarios the pipeline is expected to cover are complex.

### Project goals

Live debugging is a solution that hasn’t been explored in depth by the current providers. Much to a traditional code debugger, it would allow the developer to set breakpoints after certain stages of the pipeline, which would start a debugging session – a shell within the container the pipeline is running from. Codefresh has created such debugger in 2019, however it is exclusive to their pipelines. This would provide a faster and more convenient approach to detecting problems with pipelines than current debugging methods, such as local debugging and debugging with SSH.

## Set-up Instructions
This section expands on the live debugger set up with GitHub. I explain the steps that I have taken to set it up, explaining the most important options, but users who opt for different platforms will need to change various settings, as per required in their documentation. In order to set up the live debugger, a user must set up a Jenkins platform that is accessible through the Internet. That is required for proper GitHub integration, as Github webhooks require a URL reachable through the Internet. 

### Amazon Web Services
The first step is to set up a Jenkins platform accessible through the Internet. I used AWS for this step, but different platforms can be used to achieve the same result, such as Azure, Google Cloud, Digital Ocean, as long as the equivalent settings are correct.

The user needs to set up a virtual machine with a public IPv4 address that runs Jenkins on a desired port. The steps to achieve that can be found in Jenkins on AWS documentation:

https://www.jenkins.io/doc/tutorials/tutorial-for-installing-jenkins-on-AWS/

Care must be taken to ensure the port on which Jenkins is running is not blocked by the firewall. Otherwise, it can result in errors such as the website refuses the connection or it takes too long to respond. This is done by setting up inbound rules in the AWS instance Security Group that allow TCP connections on the desired port. The other settings are firewall on the virtual machine itself, which can be accessed with the command sudo firewalld command. SSH and Jenkins ports need to be allowed.

### GitHub Webhook

GitHub webhooks are necessary in order to automatically trigger the pipeline when a certain action occurs, such as a push event. Once a user has access to a working Jenkins URL over the Internet, they need to go the the repository settings -> Webhooks -> Add webhook. In the Payload URL field type http://(jenkins-url):(jenkins-port)/github-webhook/ and change the content type to application/json, then select the appropriate events that the application requires, and finally ensure Active is ticked.

### Jenkins Set-Up
The configuration I employed uses a Jenkinsfile in the local repository that defines the instructions to be executed by the pipeline. 

Navigate to the Jenkins URL and create a new item. Name it accordingly, then select Pipeline Project.

Under General Settings, tick Github Project and write the project repository's URL.  Under Build Triggers, tick Github hook trigger for GITScm polling. 

Under Advanced Project Options -> Pipeline, for Definition select Pipeline Script from SCM. That is the option to allow Jenkins to retrieve the custom Jenkinsfile from the repository. For SCM select Git, write the repository URL as before in the Repository URL form. Under Branches to build, ensure you have the correct named branch, e.g. main instead of master. For Git executable, select jgit instead of the Default option. This is necessary because, under the Jenkins version that I used, there is a bug in the Git plugin that causes the pipeline to fail when it tries to retrieve the repository, however jgit does not have that problem. Leave the Repository browser to (Auto) and finally in Script Path, type Jenkinsfile but consider any parent directories as well. 

With these options set up correctly, Jenkins will automatically trigger a pipeline when a desired event, such as push, is encountered, and it will use the custom Jenkinsfile script. 
