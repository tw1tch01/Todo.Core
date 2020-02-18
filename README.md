# Todo.Core

Simple backend libraries for a Todo system. This project is based on the Onion architecture, and can be used to illustrate how to structure different 
layers of your applications and isolate any changes to their relevant layer.

## Setup

These libraries are implemented as .NET Standard 2.1 class libraries. 

Ensure that you have the [latest version](https://dotnet.microsoft.com/download/dotnet-core) of ASP.NET Core installed, as the unit and integration test
projects are .NET Core 3.1 applications.

###### NuGet
Setup a local NuGet source on your PC/Workstation. To do so, perform the following steps:

In Visual Studios 2019,
1. Open `Tools > NuGet Package Manager > Package Manager Settings`
2. From the side menu, select `NuGet Package Manager > Package Sources`
3. Click add (the big green `+` symbol, top right)
4. Enter a name for the source (preferably your computer name)
5. For the source, point to a local folder (e.g. `D:\NuGet`)

###### Dependencies

This system is dependent on the [Data](https://github.com/tw1tch01/Data) application. Please ensure that you have the corresponding packages.

## Commands

To use these libraries in any of your applications, you need to build and publish a nuget package file.
Once you are ready to export the source, run the following command,

> dotnet pack -c Release -o [your local nuget source folder]

This command will build a nuget package built in Release configuration and output it directly to your local nuget source. For example:
> dotnet pack -c Release -o D:\NuGet

## Guides

Below are small guidelines as to how to use the library and some of its implementations.

### Domain

Sacred inner-most layer of the Onion. All domain relevant classes are defined here. This class _should not_ be used directly in any application other
than this one, unless you are extending/overriding the implementations in the `Services` layer.

### DomainModels

This library mainly consists of Data Transfer Objects (`Dto`). It uses `Automapper` to simplify the casting operations between an entity and its DTO.
These models are returned by the `Services` layer.

### Services

Library that is responsible for all interaction between the application, `Domain` and the database.

### Application

The services in the `Application` project should be the classes/implementations that are injected into your Web/UI/Presentation layer.
This way you can extend any extra processes that need to happen after the data has been accessed/changed, for example caching query results.

### DependencyInjectopm

In your application's `Startup.cs`, all you need to call is `services.AddApplication();` from the `Application` layer, which will wire up all 
dependencies in each respective layer. Each layer does have its own `DependencyInjection.cs` class that wires up any required dependencies for that
layer, if you wish to explicitly call those dependencies yourself.

### Workflows

Workflows in this system are implemented as `IMediator` notifications, using a custom `Mediator` implementation. Basically, the `IWorkflowService`
will `Process` a workflow by raising an event saying that it's about to do some kind of action to the entity. All `INotificationHandler`s that are
subscribed to a particular `IWorkflowProcess` notification, will be triggered. The operation will not complete until all workflows have been processed.

In order to make use of a workflow, simply create a `INotificationHandler` of the prospective workflow, and in the `Handle` method do any work that 
you need to do. 

Please see below list of all possible workflows in the system:

###### `TodoItem` workflows 
| Workflow | Description |
| - | - |
| `BeforeItemCancelledProcess` | Process any necessary logic, _before_ an item is cancelled and saved to the database. |


### Notifications
