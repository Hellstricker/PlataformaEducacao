# **PlataformaEducacao - Plataforma de gestão de cursos com DDD, TDD, CQRS, Identity, JWT, Event Sourcing e API REST**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **Plataforma de Educação Online**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Arquitetura, Modelagem e Qualidade de Software**.
O objetivo principal foi desenvolver uma plataforma educacional online com múltiplos bounded contexts (BC), aplicando DDD, TDD, CQRS e padrões arquiteturais para gestão eficiente de conteúdos educacionais, alunos e processos financeiros.

O objetiovo ainda não fon concluído, mas pretendo fazê-lo.

### **Autor(es)**
- **Ozias Manoel costa Neto**
 
## **2. Proposta do Projeto**

O projeto consiste em:
- **Bounded Contexts:** Cadastros para, gerir cursos e aulas; Gestao, para gerir alunos, aulas assistidas, certificados e matriculas; Pagamentos, para realizar o pagamento das matrículas.
- **API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.
- **Event Sourcing:** Armazenamento de eventos de mudança de estado.

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
  - EventSourcing/ - Meios para utilizar o EventStore para armazenamento dos eventos 
  - PlataformaEducacao.Core/ - Contém todas as definições comuns aos deomínios compartilhados
  - PlataformaEducacao.Gestao.Application/ - Commands, CommandHandler do Bounded Context Gestao
  - PlataformaEducacao.Gestao.Data / Todo acesso a dados do Bounded Context Gestao
  - PlataformaEducacao.Gestao.Domain / Entidades de domínio do Bounded Context Gestao
  - PlataformaEducacao.Cadastros.Application/ - DTOs e ApplicationService do Bounded Context Cadastros
  - PlataformaEducacao.Cadastros.Data / Todo acesso a dados do Bounded Context Cadastros
  - PlataformaEducacao.Cadastros.Domain / Entidades de domínio do Bounded Context Cadastros    
  - PlataformaEducacao.WebApps.WebApi / API para exposição de endpoints para acesso às funcionalidades implementadas
- tests/
  - PlataformaEducacao.Cadastros.Application.Tests/ - Testes de unidade do projeto PlataformaEducacao.Cadastros.Application
  - PlataformaEducacao.Cadastros.Domain.Tests/ - Testes de unidade do projeto PlataformaEducacao.Cadastros.Domain
  - PlataformaEducacao.Gestao.Application.Tests/ - Testes de unidade do projeto PlataformaEducacao.Gestao.Application
  - PlataformaEducacao.Gestao.Domain.Tests/ - Testes de unidade do projeto PlataformaEducacao.Gestao.Domain.Tests
  - PlataformaEducacao.WebApps.Tests/ - Testes de integração
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **BC Cadastro:** Bounded context que permite cadastrar cursos e aulas tendo a entidade Curso como AggregateRoot.
- **BC Gestao:** Bounded context que permite matricular aluno em cursos, acompanhamento das aulas assistidas, finalização de cursos e emissão de certificados.
- **Eventos:** Eventos para comunicação entre os bounded contexts de forma desacoplada.
- **Autenticação e Autorização:** Diferenciação entre usuários administradores e alunos.
- **API REST:** Exposição de endpoints para operações dos bounded contexts GestaoCursos e GestaoAlunos e Pagamento.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.
- **Event Sourcing:** Armazenamento dos eventos de mudança de estado

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git
- Docker

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/Hellstricker/plataforma-educacao.git`
   - `cd plataforma-educacao`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.Development.json`, localizado na raiz do projeto **PlataformaEducacao.WebApps.WebApi**, configure a string de conexão do SQLite.
   - Ao rodar o projeto pela primeira vez será realizada a criação do banco inserção de dados necesários.
   - O seed configurará:
     - um usuário com perfil ADMIN. 
       - login: administrador@teste.com
       - senha: 12345678
     - um usuário com perfil ALUNO. 
       - login: alunointeligente@teste.com
       - senha: 12345678

3. **Configuração do Event Store**
   - Com o Docker instaládo, acesse a pasta raiz do projeto, abra o prompt de comando de sua preferência e execute o comando **docker-compose up**.       

4. **Executar a API:**
   - `cd src/PlataformaEducacao.WebApps.WebApi/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:7103/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Como descrito no item 2, os dados necessários para rodar o projeto são inseridos de forma automática.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:7106/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
