# About This Project

Background tasks with hosted services in ASP.NET Core.

In ASP.NET Core, background tasks can be implemented as hosted services. A hosted service is a class with background task logic that implements the IHostedService interface. This article provides three hosted service examples:

Background task that runs on a timer.
Hosted service that activates a scoped service. The scoped service can use dependency injection (DI).

`IHostedService interface`

The IHostedService interface defines two methods for objects that are managed by the host:

- StartAsync(CancellationToken)
- StopAsync(CancellationToken)

## StartAsync

- `StartAsync(CancellationToken)` contains the logic to start the background task. StartAsync is called before:
- The app's request processing pipeline is configured.
- The server is started and IApplicationLifetime.ApplicationStarted is triggered.

StartAsync should be limited to short running tasks because hosted services are run sequentially, and no further services are started until StartAsync runs to completion.

## StopAsync

- `StopAsync(CancellationToken)` is triggered when the host is performing a graceful shutdown.
- StopAsync contains the logic to end the background task. Implement IDisposable and finalizers (destructors) to dispose of any unmanaged resources.

The cancellation token has a default five second timeout to indicate that the shutdown process should no longer be graceful. When cancellation is requested on the token:

- Any remaining background operations that the app is performing should be aborted.
- Any methods called in StopAsync should return promptly.

However, tasks aren't abandoned after cancellation is requested—the caller awaits all tasks to complete.

## BackgroundService base class

BackgroundService is a base class for implementing a long running IHostedService.

ExecuteAsync(CancellationToken) is called to run the background service. The implementation returns a Task that represents the entire lifetime of the background service. No further services are started until ExecuteAsync becomes asynchronous, such as by calling `await`. Avoid performing long, blocking initialization work in `ExecuteAsync`. The host blocks in StopAsync(CancellationToken) waiting for ExecuteAsync to complete.

The cancellation token is triggered when `IHostedService.StopAsync` is called. Your implementation of ExecuteAsync should finish promptly when the cancellation token is fired in order to gracefully shut down the service. Otherwise, the service ungracefully shuts down at the shutdown timeout.

## Consuming a scoped service in a background task

To use scoped services within a BackgroundService, create a scope. No scope is created for a hosted service by default.

The scoped background task service contains the background task's logic. In the following example:

- The service is asynchronous. The `DoWork` method returns a Task. For demonstration purposes, a delay of ten seconds is awaited in the DoWork method.
- An ILogger is injected into the service.

## Prerequisites

1. Install [.NET 5 SDK][dotnetdownload-url]
2. Install the latest DOTNET & EF CLI Tools by using this command:

```
 dotnet tool install --global dotnet-ef
```

3. Install the [Visual Studio][vsdownload-url] IDE 2022(v17.0.0 and above) OR [Visual Studio Code][vscodedownload-url].

## Getting Started

To get started, let's follow this short instruction routine:

1. Open up `BackgroundTasks.sln` in VS2022.
2. Run BackgroundTasks

## Evidence

<br />
<p align="center">
  <a href="https://github.com/vladperchi/BackgroundTasks">
    <img src="https://github.com/vladperchi/BackgroundTasks/blob/master/docs/Images/BackgroundTasks.jpg" alt="Background Tasks Result">
  </a>
</p>

## Core Developer Contact

- Twitter - [Vladimir][twitter-vlad-url]
- Twitter - [Code With Vladperchi][twitter-code-url]
- Linkedin - [Vladimir][linkedin-url]
- GitHub - [Vladperchi][github-url]

## License

This project is licensed with the [MIT License][license-url].

## Support

Has this Project helped you learn something New? or Helped you at work?
Here are a few ways by which you can support.

- Leave a star! :star:
- Recommend this awesome project to your colleagues
- Do consider endorsing me on LinkedIn for ASP.NET Core - [Connect via LinkedIn][linkedin-url]
- Or, If you want to support this project in the long run, [consider buying me a coffee][buymeacoffee-url]!

<br />

<a href="https://www.buymeacoffee.com/codewithvlad"><img width="250" alt="black-button" src="https://user-images.githubusercontent.com/31455818/138557309-27587d91-7b82-4cab-96bb-90f4f4e600f1.png" ></a>

[vsdownload-url]: https://visualstudio.microsoft.com/es/vs/community/
[vscodedownload-url]: https://code.visualstudio.com
[postmandownload-url]: https://www.postman.com/downloads/
[postmancollection-url]: https://github.com/vladperchi/HureIT/blob/master/postman/HureIT.Postman.json
[dockerwininstall-url]: https://docs.docker.com/docker-for-windows/install/
[coredownload-url]: https://docs.microsoft.com/en-us/ef/core/
[dotnetdownload-url]: https://dotnet.microsoft.com/download/dotnet/5.0
[structure-url]: https://github.com/vladperchi/HureIT/blob/master/docs/md/api-project-structure.md
[migrations-url]: https://github.com/vladperchi/HureIT/blob/master/docs/md/api-migrations-guide.md
[build-shield]: https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2Fvladperchi%2FHureIT%2Fbadge&style=flat-square
[build-url]: https://github.com/vladperchi/HureIT/actions
[contributors-shield]: https://img.shields.io/github/contributors/vladperchi/HureIT.svg?style=flat-square
[contributors-url]: https://github.com/vladperchi/HureIT/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/vladperchi/HureIT?style=flat-square
[forks-url]: https://github.com/vladperchi/HureIT/network/members
[stars-shield]: https://img.shields.io/github/stars/vladperchi/HureIT.svg?style=flat-square
[stars-url]: https://img.shields.io/github/stars/vladperchi/HureIT?style=flat-square
[issues-shield]: https://img.shields.io/github/issues/vladperchi/HureIT?style=flat-square
[issues-url]: https://github.com/vladperchi/HureIT/issues
[license-shield]: https://img.shields.io/github/license/vladperchi/HureIT?style=flat-square
[license-url]: https://github.com/vladperchi/HureIT/blob/master/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/vladperchi/
[github-url]: https://github.com/vladperchi/
[blog-url]: https://www.codewithvladperchi.com
[twitter-code-url]: https://www.twitter.com/codewithvlad
[twitter-vlad-url]: https://www.twitter.com/vladperchi
[buymeacoffee-url]: https://www.buymeacoffee.com/codewithvlad
[conventional-commits-shield]: https://img.shields.io/badge/Conventional%20Commits-1.0.0-%23FE5196?logo=conventionalcommits&logoColor=white
[conventional-commits-url]: https://conventionalcommits.org
