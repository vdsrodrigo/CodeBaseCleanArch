## Projeto Use Cases

No Clean Architecture, o projeto Use Cases (ou Service Application) é uma camada relativamente fina que encapsula o modelo de domínio.

Normalmente, os Casos de Uso são organizados por feature. Podem ser operações CRUD simples ou atividades muito mais complexas.

Os casos de uso não devem depender diretamente de preocupações de infraestrutura, tornando-os simples para realizar testes de unidade na maioria dos casos.

Os Use Cases geralmente são agrupados em Comandos e Consultas, seguindo o CQRS.

Ter Use Cases como um projeto separado pode reduzir a quantidade de lógica nos projetos WebApi e Infraestrutura.

Para projetos mais simples, o projeto de Use Cases pode ser omitido e seu comportamento movido para o projeto WebApi, como serviços separados ou handlers do MediatR, ou simplesmente colocando a lógica nos pontos de extremidade da API.

Para obter mais ideias sobre como organizar a estrutura de pastas do seu projeto de Use Cases:
https://twitter.com/ardalis/status/1686406393018945536