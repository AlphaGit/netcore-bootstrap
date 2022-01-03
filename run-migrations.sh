docker run \
    -v ./Alpha.Bootstrap.Database/migrations:/migrations \
    --network host \
    --rm \
    migrate/migrate \
    -path=/migrations -verbose -database postgres://appUser:appPassword@localhost:5432/appDb?sslmode=require up
