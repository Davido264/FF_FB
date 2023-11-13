.DEFAULT_GOAL := run

run:
	dotnet run

docker:
	docker run --name sql2 --hostname sql2 --rm -p 1433:1433 -v mssql_data:/var/opt/mssql/data localhost/database:latest

build:
	docker build . -t database

fill:
	sqlcmd -S 127.0.0.1,1433 -U sa -P Sql@root123 -i ./script.sql
