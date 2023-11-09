# Key Generation Commands

openssl ecparam -genkey -name secp384r1 | openssl pkcs8 -topk8 -v2 aes-256-cbc -out ssl.key

openssl req -key ssl.key -new -x509 -days 1000 -out ssl.crt

openssl pkcs12 -export -in ssl.crt -inkey ssl.key -out ssl.pfx

