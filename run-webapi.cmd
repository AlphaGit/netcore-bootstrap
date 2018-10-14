@ECHO OFF
docker run -d --rm -P --name bootstrap-webapi netcore-bootstrap-webapi:local
SLEEP 3
FOR /F "tokens=* delims=0.:" %%i IN ('docker port bootstrap-webapi 80') DO SET PUBLIC_PORT=%%i
START "" "http://localhost:%PUBLIC_PORT%/api/posts"
ECHO Press any key to stop container.
PAUSE>NUL
docker stop bootstrap-webapi