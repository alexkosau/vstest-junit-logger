This is JUnit-style **logger** for **vstest.console** unit test runner from MS Visual Studio.
===================

# Usage
Put the .dll file into *"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\Extensions"*

## Command Line: 
    vstest.console.exe <testdll> /logger:JUnitLogger;TestResultsFile=.\TestResults1.xml

You can publish the result file with "Publish JUnit report" post-build action in Jenkins.
