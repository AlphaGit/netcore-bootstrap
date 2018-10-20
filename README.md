# netcore-bootstrap

A bootstrap project to get quickly up and running with dotnet projects.

## Requirements

* Docker 17+
* Visual Studio 15.7+
* NetCore 2.1.300+ SDK

## How to setup your project from this one

- Clone this repository without git tracking
- Run a bulk file rename in the repository folder from `Alpha.Bootstrap` to `(YourCompany).(YourProject)`.
- Run a bulk replace for all the file contents from `Alpha.Bootstrap` to `(YourCompany).(YourProject)`.

## Commands

- `build-and-run-webapi`: compiles and runs the web API project (and dependencies)
- `build-docker-webapi`: compiles the web API project (and dependencies), but does not run it
- `run-migrations`: updates the running database server to its latest version of the migrations
- `run-webapi`: run a local version of the web API project, based on the latest compiled image
- `start-database`: start a database server