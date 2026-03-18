# ExternalDataSolution

## Visão geral da solução

Esta solução foi desenvolvida em **.NET 8** e é composta por dois serviços principais:

- **ExternalData.Worker**: serviço em background responsável por acessar periodicamente uma fonte externa pública e persistir os dados coletados.
- **ExternalData.Api**: API REST responsável por disponibilizar os dados coletados através de endpoints HTTP.

Para este desafio, foi utilizada a API pública **AwesomeAPI** para consulta de cotação **USD/BRL**.

Os dados são armazenados em **SQLite**, permitindo uma persistência simples, leve e adequada ao escopo do desafio.

---

## Arquitetura adotada

A solução foi estruturada em camadas para garantir melhor separação de responsabilidades, legibilidade e manutenção:

- **ExternalData.Domain**
  - Contém as entidades e contratos da aplicação.
  - Exemplo: `ExchangeRateRecord`, `IExchangeRateRepository`.

- **ExternalData.Infrastructure**
  - Responsável pela persistência com Entity Framework Core.
  - Contém `AppDbContext`, repositórios e configuração de injeção de dependência.

- **ExternalData.Worker**
  - Serviço em background que executa periodicamente a coleta dos dados externos.
  - Utiliza `HttpClientFactory` para integração com a fonte externa.
  - Persiste os dados no banco SQLite.

- **ExternalData.Api**
  - API REST responsável por expor os dados persistidos.
  - Disponibiliza endpoints para consulta do último registro e do histórico completo.
  - Inclui Swagger para facilitar testes e documentação.

---

## Decisões arquiteturais tomadas

Algumas decisões foram tomadas para equilibrar simplicidade, clareza e boas práticas:

- Separação entre **API**, **Worker**, **Domain** e **Infrastructure** para manter responsabilidades bem definidas.
- Utilização de **Worker Service** para representar o robô/RPA executando em background.
- Utilização de **Entity Framework Core** com **SQLite** por ser uma solução simples, embutida e suficiente para o desafio.
- Utilização de **migrations** para versionar a estrutura do banco.
- Utilização de **HttpClientFactory** para integração com a API externa, evitando criação manual inadequada de conexões HTTP.
- Utilização de **Docker multi-stage build** para gerar imagens mais enxutas.
- Utilização de **Docker Compose** para orquestrar API e Worker em containers separados, compartilhando o mesmo volume de banco de dados.

---

## Fonte externa utilizada

Foi utilizada a seguinte fonte pública de dados:

- **AwesomeAPI**
- Endpoint utilizado para coleta:
  - `https://economia.awesomeapi.com.br/json/last/USD-BRL`

---

## Tecnologias utilizadas

- **C#**
- **.NET 8**
- **ASP.NET Core Web API**
- **Worker Service**
- **Entity Framework Core**
- **SQLite**
- **Docker**
- **Docker Compose**
- **Swagger / OpenAPI**

---

## Como rodar o projeto localmente

### Pré-requisitos

- .NET 8 SDK
- Docker Desktop

---

## Execução sem Docker

Na raiz da solução:

```bash
dotnet build