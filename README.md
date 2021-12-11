# Safe-Travel Recommender Service
As COVID-19 gets more and more contained, travel demand has correspondingly risen. The service we created is a safe-travel recommender service for traveling purposes in the United States. Our service suggests safe-travel locations based on the state's current COVID-19 cases, weather conditions, and air pollution levels. 

# Getting started
Note: these are the instructions to run our service on Windows operating systems, since our team is all running Windows. Instructions may differ if you run a different operating system (MacOS, Linux).

#### Setting up the local database for the first time

The service requires a local instance of a MySQL database server running. Please install MySQL Workbench (most of the times: x86, 32-bit, 470.2M version) here: https://dev.mysql.com/downloads/installer/  

To get the `mysql` server to run on Windows:
- command-line version
  - First make sure the `<path to mysql installation>/MySQL Server 8.0/bin/` is added to system path.
  - To start mysql server:
    - `mysqld.exe &`
    - If this is your first time starting the server:
      - `mysqld.exe --initialize --console`, the temporarily-generated temporary password is needed when connecting to the database for the first time.
  - Then, please update your password to `asesharp` for the root user. To connect to mysql server after password is updated to `asesharp`:
    - `mysql.exe -uroot -pasesharp` (the username and password needed by the service to connect to the database can also be found in `Service/appsettings.Development.json`.)
- GUI version
  - After installation of the MySQL Workbench, find it in your apps and open it.
  - Click on the 'root' user and set your password to `asesharp`
    - if you have set your password to something else, you may run this query in order to change it: `ALTER USER 'root'@'localhost' IDENTIFIED BY 'asesharp';` 

In order to run the service, you must create the schema `asesharpdb`, and then the following tables (the queries to run are listed below):
- `comment`: This table holds user's comments for a specific trip.
  - `CREATE TABLE IF NOT EXISTS comment (uniqueId VARCHAR(100) PRIMARY KEY, userId VARCHAR(100), country VARCHAR(50), state VARCHAR(50), createdAt VARCHAR(50), commentStr VARCHAR(500)) `
- `user`: This table holds user information.
  - `CREATE TABLE IF NOT EXISTS user (UserName VARCHAR(500), UserId VARCHAR(100) PRIMARY KEY, UserRole VARCHAR(50), CreatedAt VARCHAR(50), CountryCode VARCHAR(50))`

In order to run the unit and integration tests, you must create the schema `asesharptestdb`, and then the following tables (the queries to run are listed below):
- `comment`: This table holds user's comments data for integration test.
  - `CREATE TABLE IF NOT EXISTS comment (uniqueId VARCHAR(100) PRIMARY KEY, userId VARCHAR(100), country VARCHAR(50), state VARCHAR(50), createdAt VARCHAR(50), commentStr VARCHAR(500)) `
- `user`: This table holds user data for integration test.
  - `CREATE TABLE IF NOT EXISTS user (UserName VARCHAR(500), UserId VARCHAR(100) PRIMARY KEY, UserRole VARCHAR(50), CreatedAt VARCHAR(50), CountryCode VARCHAR(50))`

#### Running the service
- Running from IDE (`Visual Studio`):
  1. Install and setup `MySQL` as described in `Setup database for the first time`.
  
  2. Install `Visual Studio 2019`.
  
  3. Install `.Net Core 3.1`.
  
  4. Open `Service.sln` with `Visual Studio`. (You can just type `Service.sln` in the root directory on Windows command prompt.)
  
  5. Locate the green play button (green arrow) at the top, and then select `Service` from the green arrow dropdown in order to run the service.

- Running from command line:
  - Instruction on how to run from command line will be provided later.

# Testing
#### Running the tests and generating the test report
1. First, make sure that `dotnet-reportgenerator-globaltool` is installed. If it is not installed, run `dotnet tool install -g dotnet-reportgenerator-globaltool` in the Windows command prompt. (More details: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows)
    - Note that this might require installation for `ASP.NET Core Runtime 6.0.0` to run: https://dotnet.microsoft.com/download/dotnet/6.0
    - Now, if you run `reportgenerator` in the command line, it should run with no errors but with `No report files specified` and `No target directory specified` warnings.
2. From the project root directory, run command `./gen_test_report.sh` __from bash__. This command will run all the unit tests, and generate the html coverage report.
    - On Windows, this can be run from git shell.
3. The generated reports are stored in `test_results/`. There is an index.html that shows the coverage of the project. Here is the link to the index.html: https://htmlpreview.github.io/?https://github.com/Alying/team_ase_sharp/blob/main/test_results/html/index.html
 

# Swagger Documentation
Please copy and paste `swagger.yaml`(under team_ase_sharp/) to `Swagger Editor` (https://editor.swagger.io/) and the documentation will be automatically rendered. It provides more detail on example requests and responses for endpoints under different situations. 

# Postman Documentation
The Postman responses from our service test runs are included in the `FirstIterationDemo.postman_test_run.json` file. To log on and see the Postman request Collection `FirstIterationDemo`, please use the following link (ASE# Team Workspace):
https://columbia-university-student-plan-team-187884.postman.co/workspace/ASE%2523~2a87fd55-26fd-42f0-9442-97ff3f88363b/collection/18347595-9703f8bc-acad-4ab9-9c83-777c2f66dbb9?ctx=documentation

# API documentation
We are using the following 3 APIs in order to make decisions for safe-travel locations in the Decision Engine (Not fully implemented yet).
  - COVID-19 API:  https://covidtracking.com/data/api/version-2
  - Weather API: https://openweathermap.org/current#call
  - Air Quality API: https://api-docs.iqair.com/?version=latest

# Client Documentation
To run the test client included in this codebase, check that there are multiple startup projects. Right-click the solution, click 'Properties', navigate to 'Startup Project' and make sure the 'Multiple startup projects' is selected. The projects that should have a 'Start' Action are WebClient and Service (i.e. on running 'Start', which is the green run arrow, both the Service and the test client WebClient will be run together.) 

# Style documentation
We use StyleCop, which gives a StyleCop.Cache for every directory. This is our style report.

# Technology
#### StyleCop
Style checker that is used to check C# code to conform StyleCop's recommended coding styles and Microsoft's .NET Framework Design Guidelines.
#### XUnit + Moq 
Test runner for .NET Framework projects
#### Coverlet + ReportGenerator
Generates coverage html report for the project
#### NCrunch 
Coverage tracker that runs automated tests and provides code coverage
Please install NCrunch, in accordance with your Visual Studio version, here: https://www.ncrunch.net/download
#### ASP.NET 
Framework for server-side web-application
#### .Net Core 3.1
Runtime that enables you to run existing server applications.
#### NewtonSoft.Json, Dapper, Optional, etc.
Libraries (NuGet packages)

# Authors (Team ASE#)
Muzhi Yang 

Alina Ying 

Tian Liu 

Mengwen Li 

# License
### GNU General Public License 

Version 3, June 2007

Copyright © 2007 Free Software Foundation, Inc. https://fsf.org/

Everyone is permitted to copy and distribute verbatim copies of this license document, but changing it is not allowed.

# Acknowledgments
We sincerely appreciate Prof. Gail Kaiser and Head TA Claire's helps on this project. They have been very helpful whenever we needed help.

# Useful links:
- [ReportGenerator](https://github.com/danielpalme/ReportGenerator)
- [Code coverage tutorial from Microsoft](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows)
- [Another code coverage tutorial that might be helpful](https://tonyranieri.com/blog/measuring-net-core-test-coverage-with-coverlet)
