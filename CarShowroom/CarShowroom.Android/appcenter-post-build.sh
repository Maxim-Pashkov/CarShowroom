#!/usr/bin/env bash

echo "Post Build Script Started"

#---------------------------------------
#UI tests run
#---------------------------------------
##
# Environment variables required in App Center Build
# https://docs.microsoft.com/en-us/appcenter/build/custom/variables/
#
# APPCENTER_TOKEN
# https://docs.microsoft.com/en-us/appcenter/api-docs/
#
##

SolutionFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name CarShowroom.sln`
SolutionFileFolder=`dirname $SolutionFile`

UITestProject=`find "$APPCENTER_SOURCE_DIRECTORY" -name CarShowroom.UITests.csproj`

echo SolutionFile: $SolutionFile
echo SolutionFileFolder: $SolutionFileFolder
echo UITestProject: $UITestProject

msbuild "$UITestProject" /property:Configuration=$APPCENTER_XAMARIN_CONFIGURATION

UITestDLL=`find "$APPCENTER_SOURCE_DIRECTORY" -name "CarShowroom.UITests.dll" | grep bin | head -1` 
echo UITestDLL: $UITestDLL

UITestBuildDir=`dirname $UITestDLL`
echo UITestBuildDir: $UITestBuildDir

UITestVersionNumber=`grep '[0-9]' $UITestProject | grep Xamarin.UITest|grep -o '[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}'`
echo UITestVersionNumber: $UITestVersionNumber

TestCloudExe=`find ~/.nuget | grep test-cloud.exe | grep $UITestVersionNumber | head -1`
echo TestCloudExe: $TestCloudExe

TestCloudExeDirectory=`dirname $TestCloudExe`
echo TestCloudExeDirectory: $TestCloudExeDirectory

APKFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.apk | head -1`

appcenter test run uitest --app "maximpashkov/CarShowroom" --devices "maximpashkov/base" --test-series "master" --locale "ru_RU" --app-path $APKFile --build-dir $UITestBuildDir --async --uitest-tools-dir $TestCloudExeDirectory --token $APPCENTER_TOKEN

echo "End UI tests"

#---------------------------------------
#unit tests run
#---------------------------------------

echo "Start Unit tests"
# For Xamarin, run all NUnit test projects that have "Test" in the name.
# The script will build, run and display the results in the build logs.

echo "Found NUnit test projects:"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*UnitTest.*\.csproj' -exec echo {} \;
echo
echo "Building NUnit test projects:"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*UnitTest.*\.csproj' -exec msbuild {} \;
echo
echo "Compiled projects to run NUnit tests:"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*bin.*UnitTest.*\.dll' -exec echo {} \;
echo
echo "Running NUnit tests:"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*bin.*UnitTest.*\.dll' -exec nunit-console --list-extensions {} +
echo
echo "NUnit tests result:"
pathOfTestResults=$(find $APPCENTER_SOURCE_DIRECTORY -name 'TestResult.xml')
cat $pathOfTestResults
echo

#look for a failing test
grep -q 'result="Failed"' $pathOfTestResults

if [[ $? -eq 0 ]]
then 
echo "A test Failed" 
exit 1
else 
echo "All tests passed" 
fi

echo "End Unit tests"