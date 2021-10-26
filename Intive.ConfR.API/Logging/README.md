# Logging

## Initialization 
Remember to __always__ initialize logger in directory you want to use it.

Like this:
```csharp
using Intive.ConfR.Logging;
// ...
class Example
{
    private readonly ILoggerManager _logger;
    public ClassConstructor(ILoggerManager logger)
    {
        _logger = logger;
    }
// ...
}
```

## How-to-use
There are three methods you can use.

### LogInfo
Used to log detailed messages.
```csharp
 _logger.LogInfo(string message)
```
### LogTrace
Used to log much more detailed messages.

```csharp
 _logger.LogTrace(string message)
```

### LogError
Used to log error messages.

```csharp
 _logger.LogError(string message)
```
## Debug Console

Logs are visible in Visual Studio Output window if __Show output from:__ is set to __Debug__.

## Files

There are two files where logs are stored.
Both can be found in folder `C:\NLogs`

For a date 2019.02.29 files look as follows:
+ Trace, Info & Debug: `nlog-info-2019-02-29.log`
+ Errors: `nlog-error-2019-02-29.log`

## Azure Storage

Additionally logs can be stored locally in Azure Tables.
To use it install:
+ [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator#get-the-storage-emulator)
+ [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)

Run both programs in same order.

## License

We use [Nlog](https://nlog-project.org/), the flex & free open-source logging for .NET .
