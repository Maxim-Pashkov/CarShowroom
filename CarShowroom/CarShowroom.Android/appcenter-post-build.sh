#!/usr/bin/env bash

echo "Post Build Script Started"

#uitest run
set -e

##
# Environment variables required in App Center Build
# https://docs.microsoft.com/en-us/appcenter/build/custom/variables/
#
# APPCENTER_TOKEN
# https://docs.microsoft.com/en-us/appcenter/api-docs/
#
##


if find $APPCENTER_SOURCE_DIRECTORY -name '*.UITests.csproj';
then
	echo "Building UI test projects:"
	find $APPCENTER_SOURCE_DIRECTORY -name '*.UITests.csproj' -exec msbuild {} \;
else
	echo "Can't find UI test project"
	exit 9999
fi
echo "Compiled projects to run UI tests:"
find $APPCENTER_SOURCE_DIRECTORY -regex '*.bin.*UITests.*\.dll' -exec echo {} \;
echo "Running test in App Center Test"
APPPATH=$APPCENTER_OUTPUT_DIRECTORY/*.apk
BUILDDIR=$APPCENTER_SOURCE_DIRECTORY/*.UITests/bin/Debug/
UITESTTOOL=$APPCENTER_SOURCE_DIRECTORY/packages/Xamarin.UITest.*/tools
appcenter test run uitest --app "maximpashkov/CarShowroom" --devices "maximpashkov/base" --test-series --test-series "master" --locale "ru_RU" --app-path $APPPATH --build-dir $BUILDDIR --async --uitest-tools-dir $UITESTTOOL --token $APPCENTER_TOKEN

echo "End UI tests"

#unit tests run
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
find $APPCENTER_SOURCE_DIRECTORY -regex '.*bin.*UnitTest.*\.dll' -exec nunit3-console {} +
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