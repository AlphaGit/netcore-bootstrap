SET PWD=%~dp0
SET PWD=%PWD%\certificates

DEL %PWD%\* /Q
MKDIR certificates

docker run --rm -e COUNTY="EC" ^
    -e STATE="Example State" ^
    -e LOCATION="Example Location" ^
    -e ORGANISATION="Example Organisation" ^
    -e PUBLIC_CN="issuer.example.com" ^
    -e ISSUER_CN="Example Issuer" ^
    -e ISSUER_NAME="issuer" ^
    -e KEYSTORE_PASS="example keystore password" ^
    -v %PWD%:/etc/ssl/certs ^
    pgarrett/openssl-alpine