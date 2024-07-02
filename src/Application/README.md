## Projeto Core (Domain Model)

No Clean Architecture, o foco central deve ser em Entidades e regras de negócios.

No Domain-Driven Design, esse é o Modelo de Domínio.

Este projeto deve conter todas as suas Entidades, Value Objects e lógica de negócios.

As entidades que estão relacionadas e devem mudar juntas devem ser agrupadas em um ggregate.

As entidades devem aproveitar o encapsulamento e minimizar os setters públicos.

As entidades podem aproveitar os Eventos de Domínio para comunicar alterações a outras partes do sistema.

As entidades podem definir especificações que podem ser usadas para consultá-las.

Para acesso mutável, as Entidades devem ser acessadas por meio de uma interface do Repositório.

As consultas read-only ad hoc podem Query Services separados que não usam o Modelo de Domínio.

Referência:
https://github.com/ardalis/CleanArchitecture/tree/main/sample



## Projeto Use Cases

No Clean Architecture, o projeto Use Cases (ou Service Application) é uma camada relativamente fina que encapsula o modelo de domínio.

Normalmente, os Casos de Uso são organizados por feature. Podem ser operações CRUD simples ou atividades muito mais complexas.

Os casos de uso não devem depender diretamente de preocupações de infraestrutura, tornando-os simples para realizar testes de unidade na maioria dos casos.

Os Use Cases geralmente são agrupados em Comandos e Consultas, seguindo o CQRS.

Ter Use Cases como um projeto separado pode reduzir a quantidade de lógica nos projetos WebApi e Infraestrutura.

Para projetos mais simples, o projeto de Use Cases pode ser omitido e seu comportamento movido para o projeto WebApi, serviços separados ou handlers do MediatR, ou simplesmente colocando a lógica nos pontos de extremidade da API.

Para obter mais ideias sobre como organizar a estrutura de pastas do seu projeto de Use Cases:
https://twitter.com/ardalis/status/1686406393018945536