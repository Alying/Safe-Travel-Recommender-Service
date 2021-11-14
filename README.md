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
- ...

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

# Documentation
https://app.swaggerhub.com/apis/test_org_comsw/comsw_4156_team_project/1.0.0

# Technology
#### StyleCop
Style checker that is used to check C# code to conform StyleCop's recommended coding styles and Microsoft's .NET Framework Design Guidelines.
#### Coverity
Static analysis bug finder that enables software engineers and security teams to find and fix software defects.
#### XUnit + Moq 
Test runner for .NET Framework projects
#### NCrunch 
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


