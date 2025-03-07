# LocalStackTestAPI

Projeto desenvolvido para testar o funcionamento do LocalStack.

## Descrição

Este projeto tem como objetivo validar a integração e o funcionamento de serviços AWS simulados pelo LocalStack em um ambiente local de desenvolvimento.

## Tecnologias Utilizadas

- **C#**
- **Docker**
- **LocalStack**

## Estrutura do Projeto

A estrutura principal do projeto é composta pelos seguintes arquivos e diretórios:

- `LocalStackTestAPI.sln`: Solução do Visual Studio.
- `docker-compose.yml`: Configuração dos serviços Docker, incluindo o LocalStack.
- `launchSettings.json`: Configurações de lançamento da aplicação.
- `.gitignore`: Arquivos e diretórios ignorados pelo Git.
- `.dockerignore`: Arquivos e diretórios ignorados pelo Docker.

## Pré-requisitos

- [Docker](https://www.docker.com/) instalado.
- [Docker Compose](https://docs.docker.com/compose/) instalado.
- [Visual Studio](https://visualstudio.microsoft.com/) ou outro ambiente de desenvolvimento C# configurado.

## Como Executar

1. Clone este repositório:

   ```bash
   git clone https://github.com/rafahbrito/LocalStackTestAPI.git
   ```

2. Navegue até o diretório do projeto:

   ```bash
   cd LocalStackTestAPI
   ```

3. Inicie os serviços Docker:

   ```bash
   docker-compose up
   ```

4. Abra a solução `LocalStackTestAPI.sln` no Visual Studio e execute a aplicação.

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

## Licença

Este projeto está licenciado sob a licença MIT. Consulte o arquivo `LICENSE` para mais informações.
