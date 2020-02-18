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

Workflows are implemented as `IMediator` notifications, using a custom `Mediator` implementation. Basically, the `IWorkflowService` will `Process` 
a workflow by raising an event stating that it will or has done some kind of action.

In order to handle the raised event (i.e. a workflow process) simply create a `INotificationHandler` of the prospective process and in the `Handle` 
method, do any work that you need to do. All `INotificationHandler`s that are subscribed to a particular `IWorkflowProcess` notification will be 
triggered and handled one after another, waiting until the previous one has completed before going onto the next. The operation will not continue until 
all workflows have been processed.

Below table lists all workflows supported in the system:

###### Todo Item workflows

| Workflow | Description |
| - | - |
| `BeforeItemCancelledProcess` | Process any necessary logic, _before_ an item is cancelled. |
| `ItemCancelledProcess` | Process any necessary logic, _after_ an item has been cancelled. |
| `BeforeItemCompletedProcess` | Process any necessary logic, _before_ an item is completed. |
| `ItemCompletedProcess` | Process any necessary logic, _after_ an item has been completed. |
| `ItemCreatedProcess` | Process any necessary logic, _after_ an item has been created. |
| `BeforeChildItemCreatedProcess` | Process any necessary logic, _before_ a child item is created. |
| `ChildItemCreatedProcess` | Process any necessary logic, _after_ an item has been created. |
| `BeforeItemDeletedProcess` | Process any necessary logic, _before_ a child item is deleted. |
| `ItemDeletedProcess` | Process any necessary logic, _after_ an item has been deleted. |
| `BeforeItemResetProcess` | Process any necessary logic, _before_ a child item is reset. |
| `ItemResetProcess` | Process any necessary logic, _after_ an item has been reset. |
| `BeforeItemStartedProcess` | Process any necessary logic, _before_ a child item is started. |
| `ItemStartedProcess` | Process any necessary logic, _after_ an item has been started. |
| `BeforeItemUpdatedProcess` | Process any necessary logic, _before_ a child item is updated. |
| `ItemUpdatedProcess` | Process any necessary logic, _after_ an item has been updated. |

###### Note workflows

| Workflow | Description |
| - | - |
| `BeforeNoteCreatedProcess` | Process any necessary logic, _before_ a note is created. |
| `NoteCreatedProcess` | Process any necessary logic, _after_ an note has been created. |
| `BeforeReplyCreatedProcess` | Process any necessary logic, _before_ a reply is created. |
| `ReplyCreatedProcess` | Process any necessary logic, _after_ a reply has been created. |
| `BeforeNoteDeletedProcess` | Process any necessary logic, _before_ a note is deleted. |
| `NoteDeletedProcess` | Process any necessary logic, _after_ an note has been deleted. |
| `BeforeReplyUpdatedProcess` | Process any necessary logic, _before_ a reply is updated. |
| `ReplyUpdatedProcess` | Process any necessary logic, _after_ a reply has been updated. |

### Notifications

Notifications are implemented as `IMediator` notifications, using a custom `Mediator` implementation. The `INotificationService` will `Queue` a 
notification by raising an event stating that some action has occurred.

In order to handle the raised event (i.e. an event notification) simply create a `INotificationHandler` of the prospective event and in the `Handle`
method, execute whatever external logic needs to happen. All `INotificationHandler`s that are subscribed to a particular `INotificationProcess` 
notification will be triggered without being handled by the process. A new thread will be started and _not_ awaited, meaning the current process will
continue, while the other thread handles the notification(s).

Below table lists all notifications supported in the system:

###### Todo Item notifications

| Notification | Description |
| - | - |
| `ItemCancelledNotification` | Event notification that an item was cancelled. |
| `ItemCompletedNotification` | Event notification that an item was completed. |
| `ItemCreatedNotification` | Event notification that an item was created. |
| `ChildItemCreatedNotification` | Event notification that a child item was created. |
| `ItemDeletedNotification` | Event notification that an item was deleted. |
| `ItemResetNotification` | Event notification that an item was reset. |
| `ItemStartedNotification` | Event notification that an item was started. |
| `ItemUpdatedNotification` | Event notification that an item was updated. |

###### Note notifications

| Notification | Description |
| - | - |
| `NotedCreatedNotification` | Event notification that a note was created. |
| `ReplyCreatedNotification` | Event notification that a reply was created. |
| `NoteDeletedNotification` | Event notification that a note was deleted. |
| `NoteUpdatedNotification` | Event notification that a note was updated. |
