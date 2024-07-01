## Projeto Infrastructure

No Clean Architecture, as preocupações de infraestrutura são mantidas separadas das principais regras de negócios (ou modelo de domínio no DDD).

O único projeto que deve ter código relacionado a EF, Arquivos, E-mail, Web Services, Azure/AWS/GCP, etc é Infraestrutura.

A infraestrutura deve depender do Core (e, opcionalmente, dos Casos de Uso) onde existem abstrações (interfaces).

As classes de infraestrutura implementam interfaces encontradas no(s) projeto(s) Core (Use Cases).