# REQUISITOS PARA EXECUÇÃO DO PROJETO
### ANGULAR

  - Instalar o node.js (versão utilizada pelo desenvolvedor v 18.17.1)
  - versão do npm do desenvolvedor (9.8.1), instalação global. (nmp install -g)
  - Instalar o Angular (versão do CLI do desenvolvedor 16.2.0)
  - Para criação dos testes foram adicionados componentes jasmine karma através do comando:
**npm install --save-dev @angular/cli @types/jasmine jasmine-core karma karma-chrome-launcher karma-coverage-istanbul-reporter karma-jasmine karma-jasmine-html-reporter]**

- Lembrando que a pasta raiz do projeto é a WebPage.
  - Estando com os requistos prontos e na pasta WebPage, para rodar a aplicação Web utiliza-se o comando **ng serve -o**.
  - E para a execução dos testes o comando **ng test**.
### .NETCORE
- Aplicação WebApi em .netcore 6.0, garantir esta versão instalada na máquina.
- Para execução dos testes unitários, pelo terminal, basta estar na pasta WebPage da solução e executar o comando "dotnet test". 
 
#### Importante
>Ao executar esta aplicação, lembre-se de deixar as portas **7217 e 4200** livres para que o site e a api as utilizem.
```sh
Porta 7217 livre para aplicação .netcore
Porta 4200 livre para aplicação Angular
```

#### Tabela Auxiliar
| Requisitos | Versão |
| ------ | ------ |
| Node.js | 18.17.1 |
| Angular CLI | 16.2.0 |
| NPM | 9.8.1 |
| .Netcore | 6.0 |
| SDKs | 5.0.412 - 7.0.100 instaladas |

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
