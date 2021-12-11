#!/bin/bash

# On Windows, pleas install git-bash for running this script.
# https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows
# https://tonyranieri.com/blog/measuring-net-core-test-coverage-with-coverlet

# Export env variable to enable db related integration tests.
export LOCAL_TEST=1

# Remove the last generated report.
rm -rf ./test_results

# Make sure that mysql server is running.
mysqld.exe &

# Run the unit tests.
dotnet test -r "./test_results" --collect "xplat code coverage"
latest_path=`ls -td ./test_results/* | head -1`
echo $latest_path
# dotnet reportgenerator "-reports:${latest_path}/coverage.cobertura.xml" "-targetdir:test_results/html" "-reporttypes:HTML"
reportgenerator -reports:${latest_path}/coverage.cobertura.xml -targetdir:test_results/html -reporttypes:HTML