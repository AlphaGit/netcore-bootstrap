docker build -f Database.Dockerfile -t netcore-bootstrap-psql .

docker run --rm ^
    --name netcore-bootstrap-psql ^
    -e POSTGRES_USER=appUser ^
    -e POSTGRES_DB=appDb ^
    -e POSTGRES_PASSWORD=appPassword ^
    -p 5432:5432 ^
    netcore-bootstrap-psql
