#!/bin/bash

# On Windows, pleas install git-bash for running this script.
# https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows
# https://tonyranieri.com/blog/measuring-net-core-test-coverage-with-coverlet

# Remove the last generated report.
rm -rf ./test_results

# Make sure that mysql server is running.
mysqld.exe &

# Run the unit tests.
dotnet test -r "./test_results" --collect "xplat code coverage"
latest_path=`ls -td ./test_results/* | head -1`
echo $latest_path
reportgenerator -reports:${latest_path}/coverage.cobertura.xml -targetdir:test_results/html -reporttypes:HTML
