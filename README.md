# RPM Freight Systems Fuel Tracking
Weekly U.S. fuel pricing downloads

## Overview
The App is written in .NET 6, it uses [hangfire](https://www.hangfire.io/) for job scheduling. <br>
Hangfire has its own mechanism to retries failed jobs, and it can be configured how many retries before it gives up. <br>

## Prerequisites
-  [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configurable settings
- `HangfireConnection` and `FuelTrackingConnection` connection strings.
- `EiaEndpoint`, kept empty, should be filled with correct endpoint.
- `FuelTrackingCronExpression` is a CRON expression consumed by Hangfire. (i.e. it can be weekly, daily, etc..)
- `MaxDay` The N max days to save data.

## Installation
1. Run script `database.sql` located in `RpmFsRepositories` project. 
2. Launch `RpmFsJobs` app.
3. The app will automatically launch [hangfire](https://www.hangfire.io/) dashboard.

## Notes
If a record is already existing, the app will perform an `UPSERT`/`Merge` operation. The purpose is to keep the prices up-to-date. If needed, functionality can be modified to simply ignore new data and keep older price.
