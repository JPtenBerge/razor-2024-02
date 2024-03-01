# Gebruikte Docker-commando's

```sh
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=IdonotETEUIOJHFOIDS9898798asD-8&" -e "MSSQL_PID=Evaluation" -p 1533:1433  --name sqlpreview --hostname sqlpreview -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

docker run -p 5500:8080 -p 5501:8081 -e "connstring=Server=host.docker.internal,1533; Database=avatardb; User Id=sa; Password=IdonotETEUIOJHFOIDS9898798asD-8&"
```
