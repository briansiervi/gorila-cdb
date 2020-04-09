# gorila-cdb
# Realizando chamadas remotas ao servidor
- Use um editor de api's restful de sua preferência para os testes abaixo.

## Exemplos de requisições

#### /GET
- enpoint: https://gorila-cdb.azurewebsites.net/health
- obs: é apenas um healthcheck

#### /POST

- enpoint: https://gorila-cdb.azurewebsites.net/api/home
    1. request
        ```
        {
            "investmentDate":"2016-11-14",
            "cdbRate": 103.5,
            "currentDate":"2016-12-23"
        }
        ```
        - reponse (json) [200 - ok]
            ```
            [
                {
                    "date": "2016-11-14T00:00:00",
                    "unitPrice": 1.0005339668500000
                },
                {
                    "date": "2016-11-16T00:00:00",
                    "unitPrice": 1.0010682188206000
                },
                ...    
            ]
            ```

    1. request
        ```
        {
            "investmentDate":"2025-11-14",
            "cdbRate": 103.5,
            "currentDate":"2030-11-28"
        }
        ```
        - reponse (json) [400 - bad request]
            ```
            [
                {
                    "fields": [
                        "currentDate",
                        "investmentDate"
                    ],
                    "message": "Desculpe, mas as datas informadas não estão presentes em nossas bases de dados."
                }
            ]
            ```

    1. request
        ```
        {
            "investmentDate":"1990-01-01",
            "cdbRate": 103.5,
            "currentDate":"2019-01-01"
        }
        ```
        - reponse (json) [400 - bad request]
            ```
            [
                {
                    "fields": [
                        "investmentDate"
                    ],
                    "message": "Por favor, insira um valor maior do que este. A menor data de investimento presente em nossas bases é 04/01/2010."
                }
            ]
            ```

# Baixando o projeto para rodar localmente

### Passo 1 
- Instale o dotnet sdk (versão 3.1) como descrito [aqui](https://dotnet.microsoft.com/learn/aspnet/hello-world-tutorial/install).

### Passo 2
- Abra o terminal e digite os seguintes comandos
    ``` 
        git clone https://github.com/briansiervi/gorila-cdb.git
        cd gorila-cdb
        dotnet restore
        dotnet build
        dotnet run
    ``` 
### Passo 3
- Utilize os cenários acima para realizar os testes, trocando os endpoints de https://gorila-cdb.azurewebsites.net/ para https://localhost:5001/

# Docker
- Opcionalmente você poderá rodar este projeto com o docker
- Recomendo utilizar [esta extensão](https://github.com/Microsoft/vscode-docker) do Vscode