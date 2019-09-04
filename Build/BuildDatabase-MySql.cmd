@echo off

del /f %~dp0..\Bin\WeatherForecast.db

%~dp0sqlite3.exe %~dp0..\Bin\WeatherForecast.db < %~dp0..\Database\Sqlite\CreateTable-Cities.sql
%~dp0sqlite3.exe %~dp0..\Bin\WeatherForecast.db < %~dp0..\Database\Sqlite\CreateTable-Forecasts.sql
