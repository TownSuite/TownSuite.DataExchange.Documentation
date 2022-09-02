


# Auth

Initialize account

```bash
curl --location --request POST 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/auth' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Username": "{{email}}",
    "Password": "{{password}}",
    "TwoFactorCode": ""
}'
```


Auth twofactor

```bash
curl --location --request POST 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/auth/twofactor' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Username": "{{username}}",
    "Password": "{{password}}",
    "TwoFactorCode": "{{the 2fa code}}"
}'
```


Refresh 
```bash
curl --location --request GET 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/auth/refresh' \
--header 'Authorization: Bearer YourRefreshTokenHere'
```



# Property


Assessments

```bash
curl --location --request GET 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/property/assessment/TheDataOnwerCodeHere' \
--header 'Authorization: Bearer YourBearerTokenHere'
```


Annual bill

```bash
curl --location --request GET 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/property/annualbill/TheDataOnwerCodeHere' \
--header 'Authorization: Bearer YourBearerTokenHere'
```


Information

```bash
curl --location --request GET 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/property/information/TheDataOnwerCodeHere' \
--header 'Authorization: Bearer YourBearerTokenHere'
```


Local improvements

```bash
curl --location --request GET 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/property/localimprovement/TheDataOnwerCodeHere' \
--header 'Authorization: Bearer YourBearerTokenHere'
```


Other owner

```bash
curl --location --request GET 'https://tsx.yqx.auto.k8s.your-eservices.com/api/v1/property/otherowner/TheDataOnwerCodeHere' \
--header 'Authorization: Bearer YourBearerTokenHere'
```

