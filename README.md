# **PlataformaEducacao - Plataforma de gestão de cursos com DDD, EventSourcing, TDD e API REST**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **Plataforma de Educação Online**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Arquitetura, Modelagem e Qualidade de Software**.
O objetivo principal foi desenvolver uma plataforma educacional online com múltiplos bounded contexts (BC), aplicando DDD, TDD, CQRS e padrões arquiteturais para gestão eficiente de conteúdos educacionais, alunos e processos financeiros .


### **Autor(es)**
- **Ozias Manoel costa Neto**
 
## **2. Proposta do Projeto**

O projeto consiste em:
- **Bounded Contexts:** Gestão de cursos para gerir cursos e aulas; Gestão de Alunos, para gerir alunos, aulas assistidas, certificados e matriculas; Pagamentos, para realizar o pagamento das matrícculas.
- **API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**  
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:


- src/
  - PlataformaEducacao.Core/ - Contém todas as definições comuns aos deomínios compartilhados.
  - PlataformaEducacao.GestaoAlunos.Application/ - DTOs e ApplicationService do Bounded Context Gestaoalunos
  - PlataformaEducacao.GestaoAlunos.Data / Todo acesso a dados do Bounded Context Gestaoalunos
  - PlataformaEducacao.GestaoAlunos.Domain / Entidades de domínio do Bounded Context Gestaoalunos
  - PlataformaEducacao.GestaoCursos.Application/ - DTOs e ApplicationService do Bounded Context GestaoCursos
  - PlataformaEducacao.GestaoCursos.Data / Todo acesso a dados do Bounded Context GestaoCursos
  - PlataformaEducacao.GestaoCursos.Domain / Entidades de domínio do Bounded Context GestaoCursos
  - PlataformaEducacao.Pagamentos.AntiCorruption/ - Camada anti corrupção para simulação de pagamento
  - PlataformaEducacao.Pagamentos.Business / Entidades de domínio do Bounded Context GestaoCursos
  - PlataformaEducacao.Pagamentos.Data / Todo acesso a dados do Bounded Context Pagamentos
  - PlataformaEducacao.WebApps.WebApi / API para exposição de endpoints para acesso às funcionalidades implementadas
- tests/
  - PlataformaEducacao.GestaoCursos.Domain.Tests/ - Testes do domínio do contexto GestaoCursos
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **BC GestaoCursos:** Bounded context que permite cadastrar cursos e aulas tendo a entidade Curso como AggregateRoot.
- **BC GestaoAlunos:** Bounded context que permite matricular aluno em cursos, acompanhamento das aulas assistidas, finalização de cursos e emissão de certificados.
- **BC Pagamentos:** Bounded context que permite a realização da simulação do pagamento de matriculas.
- **Eventos:** Eventos para comunicação entre os bounded contexts de forma desacoplada.
- **Autenticação e Autorização:** Diferenciação entre usuários administradores e alunos.
- **API REST:** Exposição de endpoints para operações dos bounded contexts GestaoCursos e GestaoAlunos e Pagamento.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/Hellstricker/plataforma-educacao.git`
   - `cd plataforma-educacao`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.Development.json`, localizado na raiz do projeto **PlataformaEducacao.WebApps.WebApi**, configure a string de conexão do SQLite.
   - Ao rodar o projeto pela primeira vez será realizada a criação do banco inserção de dados necesários.
   - O seed configurará um usuário com perfil Admin. 
     - login: administrador@teste.com
     - senha: 12345678

3. **Executar a API:**
   - `cd src/PlataformaEducacao.WebApps.WebApi/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:7103/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Como descrito no item 2, os dados necessários para rodar o projeto são inseridos de forma automática.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:7103/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
