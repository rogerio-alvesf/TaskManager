# TaskManager
## Executando o Projeto

Antes de executar o projeto, certifique-se de atender aos seguintes pré-requisitos:

### Pré-Requisitos

- **Banco de Dados:**
  - Se optar pela configuração manual, você precisará ter um servidor de banco de dados compatível como SQL Server instalado em sua máquina.
  - Caso opte por usar o Docker Compose, certifique-se de ter o Docker instalado em sua máquina.

- **Ferramentas de Desenvolvimento:**
  - Certifique-se de ter as ferramentas de desenvolvimento adequadas instaladas em sua máquina, como um IDE (Ambiente de Desenvolvimento Integrado), compilador (se aplicável).

- **Docker e Docker Compose (opcional):**
  - Se você deseja usar Docker Compose para executar o projeto, é necessário ter o Docker e o Docker Compose instalados em sua máquina.

Agora, você pode escolher entre as duas opções de execução do projeto, dependendo de suas necessidades e preferências.

### 1. Configuração Manual

Se você optar pela configuração manual, siga os seguintes passos:

1. **Banco de Dados:**
   - Execute o arquivo `setup.sql` que está em `Schemas/setup.sql` para criar o banco de dados e as tabelas necessárias.

2. **Configuração do `appsettings.json`:**
   - Crie o arquivo `appsettings.json` conforme o exemplo abaixo e ajuste as configurações conforme necessário, incluindo as informações de conexão do banco de dados.
   ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "Jwt": {
        "secretKey": "T@$kManagerSecretKeyJwt",
        "Issuer": "github.com/rogerio-alvesf",
        "Audience": "https://github.com/rogerio-alvesf"
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "MyDatabaseConnection": "Data Source=SERVERNAME;Initial Catalog=TaskManager;Integrated Security=True;TrustServerCertificate=True;"
      }
    }
  - OBSERVAÇÃO: SERVERNAME precisa ser substituído pelo nome do servidor da sua máquina.
3. **Execução do Projeto:**
   - Compile e execute o projeto conforme as instruções específicas para sua plataforma.

### 2. Utilizando Docker Compose

Se preferir uma configuração mais simplificada e rápida, você pode usar o Docker Compose. Basta seguir os passos abaixo:

1. **Docker Compose:**
   - Certifique-se de ter o Docker e o Docker Compose instalados em sua máquina.

2. **Execução:**
   - Na raiz do projeto, execute o seguinte comando no terminal:
     ```
     docker-compose up
     ```

   Isso criará e iniciará os contêineres necessários conforme definido no arquivo `docker-compose.yml`.
   - Observação: O script setup_env.sh será executado automaticamente durante a criação dos containers Docker. Este script configura o ambiente SMTP necessário para interagir com os endpoints do projeto que utilizam o   protocolo SMTP.

Escolha o método que melhor se adapta às suas necessidades e desfrute do projeto!
