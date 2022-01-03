docker run -d --rm -P --name bootstrap-webapi netcore-bootstrap-webapi:local
sleep 3

PUBLIC_PORT=$(docker port bootstrap-webapi 80 | cut -d: -f2)
open "http://localhost:$PUBLIC_PORT/api/posts"

read -p "Press [Enter] key to stop container..."
docker stop bootstrap-webapi