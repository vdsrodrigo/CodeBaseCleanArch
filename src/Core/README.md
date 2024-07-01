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