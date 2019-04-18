# Setup
To combine the nuxt-frontEnd build with the asp.net standard backend,
watch out, that they both are on the same port number and both the same protocol. Meaning, that they both have to be http OR https. 

## Run
For running the application, the newest version of the MS SQL Server should be installed. It can be downloaded from: https://www.microsoft.com/en-gb/sql-server/sql-server-downloads

The database must be created in the correct folder, other from that, the project works out of the box. (how to create the database is explained below)

## Update
If you want the newest version of the frontEnd in the asp.net project, you deploy the newest version of the frontEnd (explained in the frontEnd folder). In the now created "dist"-folder, you copy the whole content of the created "_nuxt"-folder and replace it with the already existing "_nuxt"-folder in the asp.net project.

After that, the content of the .html files has to be copied over into the asp.net cshtml-files, meaning:

- 'nuxt-finance-viewer\dist\index.html' into 'FinanceViewer.Net\FinanceViewer.Net\Views\Home\Index.cshtml'
- 'nuxt-finance-viewer\dist\admin\index.html' into 'FinanceViewer.Net\FinanceViewer.Net\Views\Admin\Admin.cshtml'
- 'nuxt-finance-viewer\dist\dashboard\index.html' into 'FinanceViewer.Net\FinanceViewer.Net\Views\Dashboard\Dashboard.cshtml'
## Set Port Number in nuxt-frondEnd
In the file 'nuxt-finance-viewer\nuxt.config.js' inside the 'axios'-configuration, the 'baseURL'-attribute contains the deployment-url with containing port number. Just change the port number and rebuild/deploy the application.

## Set Port Number in ASP.NET Project
You can change the port number of the project when you open the solution and right click on the project "FinanceViewer.Net" > "Properties". In the now open window you have to click on the section "Web" and there you can change the port number under "Project Url".  

## Set Up Database
When you open the solution with Visual Studio, in the "FinanceViewer.Net"-project there is the "App_Data"-folder. Inside this folder, there is the .sql-file that containts the scripts to build a finance-viewer MSSQL-database with already existing test data (users have the password "admin" in this test-db).

You have to right click on the "App_Data"-folder and add a new item. Now, from "Visual C#" > "Data" you select a "SQL Server Database" that you name "financeviewer.mdf". You open the Database, execute a SQL-Statement and execute the content from the .sql-file. Now everything should work.