# ItSoftware.ExceptionHandler  
License: **GPL-3.0-or-later**  
.NET Framework exception handling framework.  

Example configuration in App.Config:
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="exceptionHandler" type="ItSoftware.ExceptionHandler.ExceptionHandlerSectionHandler,ItSoftware.ExceptionHandler"/>
  </configSections>
  <exceptionHandler>
    <policy name="Information">
      <publisher name="EventLog" source="Test" logName="ItSoftware" enabled="true" eventLogEntryType="Information">
        <formatter type="ItSoftware.ExceptionHandler.Formatters.DefaultFormatter"/>
      </publisher>
      <publisher name="File" filename="c:\temp\testlog.txt" overwrite="true" enabled="false">
        <formatter type="ItSoftware.ExceptionHandler.Formatters.DefaultFormatter"/>
      </publisher>
      <publisher name="EMail"
                 host=""
                 port="25"
                 fromAddress=""
                 toAddress=""
                 ccAddress=""
                 bccAddress=""
                 subject="An error occurred in Xxx"
                 enabled="false">
        <formatter type="ItSoftware.ExceptionHandler.Formatters.DefaultFormatter"/>
      </publisher>
    </policy>
    <policy name="Warning">
      <publisher name="EventLog" source="Test" logName="ItSoftware" enabled="true" eventLogEntryType="Warning">
        <formatter type="ItSoftware.ExceptionHandler.Formatters.DefaultFormatter"/>
      </publisher>
    </policy>
    <policy name="Error">
      <publisher name="EventLog" source="Test" logName="ItSoftware" enabled="true" eventLogEntryType="Error">
        <formatter type="ItSoftware.ExceptionHandler.Formatters.DefaultFormatter"/>
      </publisher>
    </policy>
  </exceptionHandler>
</configuration>
```

Example usage:
```
ApplicationException ae = new ApplicationException("hello, world!", new Exception("This is an inner exception"));
try
{
    throw ae;
}
catch (Exception y)
{
    ExceptionManager.PublishException(y, "Error");
}
```
