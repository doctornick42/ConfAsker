# ConfAsker

***Please note that the current version of ConfAsker is very early and it's not ready for production.***

ConfAsker is a simple solution to check your config files after the deployment.

After publishing a Web site or an application it seems to be a good decision to check published config files to be sure that you have right connection strings or some application settings there.

ConfAsker allows you to create a query to config files. For example, if you want to make sure that your Web.config file (let's imagine that this file is in the same folder as the ConfAsker application) contains app setting called "TestKey" and it's value should be "TestValue", you need to run ConfAsker.ConsoleApp.exe with these arguments

    ConfAsker.ConsoleApp.exe check paths:"Web.config" keyValue:"TestKey" expected:"TestValue"
   
So, if this assumption is right - ConfAsker will return "True" in the console. If not - it will return "False" to the console and will write the description in a log file (application folder\App_Data\logs\Info). Log will tell you what's wrong with your file.

You can also check a connection string:

    ConfAsker.ConsoleApp.exe check paths:"Web.config" connectionString:"connectionStringName" expected:"some_connection"

"Paths" argument could be also a full path to a file and could contains multiple paths. 

    ConfAsker.ConsoleApp.exe check paths:"D:\temp\Web.config,D:\temp\app\App.config" keyValue:"TestKey" expected:"TestValue"

In the last example, if Web.config contains expected value but App.config doesn't, application will return "False" and in log file there will be this line:

    2015-11-19 23:41:07.4359 - Info 
    "ConfAsker.ConsoleApp.exe check paths:"D:\temp\Web.config,D:\temp\app\App.config" keyValue:"TestKey" expected:"TestValue". 
    Mismatch in file 'D:\temp\app\App.config' on 'TestKey'. 
