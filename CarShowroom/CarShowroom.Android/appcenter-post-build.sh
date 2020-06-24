﻿#!/usr/bin/env bash
echo "Post build file init"

if [ "$APPCENTER_XAMARIN_CONFIGURATION" == "Debug" ];then

    echo "Post Build Script Started"

    SolutionFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name CarShowroom.sln`
    SolutionFileFolder=`dirname $SolutionFile`

    UITestProject=`find "$APPCENTER_SOURCE_DIRECTORY" -name CarShowroom.UITests.csproj`

    echo SolutionFile: $SolutionFile
    echo SolutionFileFolder: $SolutionFileFolder
    echo UITestProject: $UITestProject

    chmod -R 777 $SolutionFileFolder

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

    npm install -g appcenter-cli@2.6.0

    appcenter login --token token

    appcenter test run uitest --app "maximpashkov/CarShowroom" --devices "maximpashkov/base" --app-path $APKFile --test-series "master" --locale "en_US" --build-dir $UITestBuildDir --uitest-tools-dir $TestCloudExeDirectory --async
fi