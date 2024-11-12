# Projeto de cadastro de Brands utilizando microserviços com .NET 8, Entity Framework, JWT, MySQL e Angular 15

Este repositório contém dois microserviços: **auth-service** para login e **brand-service** para o cadastro de marcas. O front-end foi desenvolvido em **Angular 15**.

A seguir, você encontrará as instruções para configurar o ambiente de desenvolvimento e rodar o sistema com um banco de dados MySQL no Docker.

## Pré-requisitos

- **Docker**: Certifique-se de que o Docker Desktop esteja instalado em seu sistema. Você pode baixar a versão apropriada para o seu sistema operacional a partir do [site oficial do Docker](https://www.docker.com/get-started).
- **.NET 8 SDK**: Para rodar os microserviços no backend, é necessário ter o **.NET 8 SDK** instalado. Você pode fazer o download [aqui](https://dotnet.microsoft.com/download/dotnet/8.0).
- **Node.js e npm**: Para rodar o front-end, você precisará do **Node.js** e do **npm** instalados. Baixe a versão mais recente em [nodejs.org](https://nodejs.org/).

---

## Passo 1: Verifique se o Docker está em execução

Antes de começar, verifique se o Docker Desktop está em execução. Você deve ver o ícone do Docker na bandeja do sistema (canto inferior direito da tela). Se não estiver em execução, inicie o Docker Desktop.

## Passo 2: Criar um contêiner MySQL

Para criar um contêiner do MySQL, execute o seguinte comando no terminal:

docker run --name mysql-container -e MYSQL_ROOT_PASSWORD=Teste@2024 -e MYSQL_DATABASE=dev-brands -p 33068:3306 -d mysql:latest

Este comando cria um contêiner MySQL com o nome `mysql-container`, define a senha do usuário root como `Teste@2024`, e cria o banco de dados `dev-brands`.

---

## Passo 3: Configurar o Backend

### 3.1: Clone o repositório

Clone o repositório para sua máquina local:

git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio

### 3.2: Configurar a conexão com o banco de dados

No backend, os microserviços estão configurados para usar o MySQL. Acesse a pasta de cada microserviço e configure a string de conexão no arquivo `appsettings.json` ou no arquivo de variáveis de ambiente, conforme o caso:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=33068;Database=dev-brands;User=root;Password=Teste@2024;"
}

### 3.3: Restaurar pacotes NuGet

Execute o comando abaixo para restaurar os pacotes NuGet necessários para o projeto:

dotnet restore

### 3.4: Executar os microserviços

Para rodar os microserviços no .NET 8, use os seguintes comandos:

1. Para o `auth-service`:

    cd auth-service
    dotnet run

2. Para o `brand-service`:

    cd brand-service
    dotnet run

### 3.5: Testar a API

Após rodar os microserviços, você pode testar as APIs de login e cadastro de marcas.

---

## Passo 4: Configurar o Frontend

### 4.1: Instalar dependências do Angular

No diretório raiz do frontend, execute o comando abaixo para instalar as dependências:

cd frontend
npm install

### 4.2: Configurar o ambiente

O frontend está configurado para fazer chamadas para os microserviços, então verifique o arquivo `src/environments/environment.ts` e certifique-se de que as URLs estão corretas:

export const environment = {
  production: false,
    api: 'http://localhost:8000',
};

### 4.3: Executar o frontend

Para rodar o frontend, execute o comando:

ng serve

Isso iniciará o servidor de desenvolvimento Angular na porta 4200. Você pode acessar a aplicação no navegador em [http://localhost:4200](http://localhost:4200).

---

## Passo 5: Testar o sistema

- **Autenticação**: Acesse o frontend e faça o login utilizando a API do `auth-service`.
  - **Para acessar o sistema utilize as credenciais  Email: admin@brand.com Senha: Admin@2024**


- **Cadastro de Marcas**: No painel, utilize o `brand-service` para cadastrar novas marcas.

---

## Passo 6: Dockerize (Opcional)

Caso deseje rodar o sistema em containers Docker, você pode criar imagens Docker para os microserviços e o frontend. Para isso, consulte os arquivos `Dockerfile` e `docker-compose.yml` presentes no repositório para instruções detalhadas.

---

## Tecnologias Utilizadas

- **Backend**: .NET 8, Entity Framework, MySQL
- **Frontend**: Angular 15
- **Banco de Dados**: MySQL
- **Docker**: Para criar ambientes isolados para os serviços

---

## Mensagem de encerramento!

Este é meu projeto, espero que gostem. Qualquer dúvida entrem contato comigo. Email: brenomarques19176@gmail.com
