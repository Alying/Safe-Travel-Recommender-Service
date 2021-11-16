# Safe-Travel Recommender Service
As COVID-19 is getting more and more contained, travel demand has correspondingly risen. The service we created is a safe-travel recommender service for traveling purposes in the United States. Our service suggests travel spots based on the state’s current COVID-19 cases, weather conditions, and air pollution levels. 

# Getting started
#### Setup database for the first time

The service requires a local instance of MySQL database server running. 

The following instructions might be useful for getting `mysql` server to run on Windows:
- Fist make sure the `<path to mysql installation>/MySQL Server 8.0/bin/` is added to system path.
- To start mysql server:
  - `mysqld.exe &`
  - If this is the first time starting the server:
    - `mysqld.exe --initialize --console`, the temporary generated temporary password is needed when connecting to the database for the first time.
- To connect to mysql server after password is updated to `asesharp`:
  - `mysql.exe -uroot -pasesharp` (the username and password needed by the service to connect to the database can also be found in `Service/appsettings.Development.json`.)

Before running the service, the schema `asesharpdb`, and the following tables needs to be created:
- `comment`: This table holds user's comments for a specific trip.
  - `CREATE TABLE IF NOT EXISTS comment (uniqueId VARCHAR(100) PRIMARY KEY, userId VARCHAR(100), country VARCHAR(50), state VARCHAR(50), createdAt VARCHAR(50), commentStr VARCHAR(500)) `
- `user`: This table holds user information.
  - `CREATE TABLE IF NOT EXISTS user (UserName VARCHAR(500), UserId VARCHAR(100) PRIMARY KEY, UserRole VARCHAR(50), CreatedAt VARCHAR(50), CountryCode VARCHAR(50))`

Before running the unit tests, the schema `asesharptestdb`, and the following tables needs to be created:
- `comment`: This table holds user's comments data for integration test.
  - `CREATE TABLE IF NOT EXISTS comment (uniqueId VARCHAR(100) PRIMARY KEY, userId VARCHAR(100), country VARCHAR(50), state VARCHAR(50), createdAt VARCHAR(50), commentStr VARCHAR(500)) `
- `user`: This table holds user data for integration test.
  - `CREATE TABLE IF NOT EXISTS user (UserName VARCHAR(500), UserId VARCHAR(100) PRIMARY KEY, UserRole VARCHAR(50), CreatedAt VARCHAR(50), CountryCode VARCHAR(50))`

#### Running the service
- Running from IDE (`Visual Studio`):
  1. Install and setup `MySQL` as described in `Setup database for the first time`.
  
  2. Install `Visual Studio`.
  
  3. Install `.Net Core 3.1`.
  
  4. Open `Service.sln` with `Visual Studio`.
  
  5. Select `Service` from the green arrow dropdown, and click the green arrow.

- Running from command line:
  - Instruction on how to run from command line will be provided latter.

# Testing
#### Running the test and generate report
1. First make sure that `dotnet-reportgenerator-globaltool` is installed. (https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows)
    - Note that this might require installation for `ASP.NET Core 6.0` to run: https://dotnet.microsoft.com/download/dotnet/6.0
2. From the project root directory, run command `./gen_test_report.sh` __from bash__. This command will run all the unit tests, and generate the html coverage report.
    - On Windows, this can be run from git shell.
3. The generated reports are stored in `test_results/`.

# Swagger Documentation
Please copy and paste `swagger.yaml`(under team_ase_sharp/) to `Swagger Editor` (https://editor.swagger.io/) and the documentation will be automatically rendered. It provides more detail on example requests and responses for endpoints under different situations. 

# Technology
#### StyleCop
Style checker that is used to check C# code to conform StyleCop's recommended coding styles and Microsoft's .NET Framework Design Guidelines.
#### XUnit + Moq 
Test runner for .NET Framework projects
#### Coverlet + ReportGenerator
Generate coverage html report for the project
#### NCrunch 
Please install NCrunch, in accordance with your Visual Studio version, here: https://www.ncrunch.net/download
Coverage tracker that runs automated tests and provides code coverage
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
