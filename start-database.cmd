docker run --rm ^
    --name netcore-bootstrap-psql ^
    -e POSTGRES_USER=appUser ^
    -e POSTGRES_DB=appDb ^
    -e POSTGRES_PASSWORD=appPassword ^
    -p 5432:5432 ^
    postgres:11