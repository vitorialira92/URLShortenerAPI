# URL Shortener API
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![SQLite](https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)

Este é um projeto desenvolvido como atividade do módulo de Testes Automatizados do curso de C# da AdaTech em parceria com o Mercado Eletrônico.

## Table of Contents

- [Uso](#uso)
- [Requisitos](#requisitos)
- [API Endpoints](#api-endpoints)
- [Funcionamento](#funcionamento)
- [Testes](#testes)
- [Database](#database)
  
## Uso

1. Clone o repositório
2. Rode o código na sua maáquina
3. A API estará acessível no link https://localhost:7106/
4.É possível acesso a documentação do Swagger em https://localhost:7106/swagger/index.html

## Requisitos

1. Receber uma url qualquer e retornar um objeto que contém:
   -  O id da requisição
   -  a url curta
   -  o tempo em segundos pelo qual a url curta será válida
3. Retornar a url original ao consultar uma url curta válida;
4. Retornar um error, caso não exista uma url original válida para a url curta consultada;
5. A última parte da url encurtada deve ter no máximo 7 caracteres.


## API Endpoints
A API tem os seguintes endpoints:

```markdown
---------URL-----------

GET /{shortUrl} - redirect to a website using the short version

POST /{originalUrl} - create a short link to a website


```

## Funcionamento

Para gerar a url curta os seguintes passos são seguidos:
1. As strings "https", "http" e "www" são removidas da string original
2. Todos os caracteres não alfanuméricos são removidos da string original
3. A string é deixada lower case
4. É feita a soma do valor na tabela ascii de todos os caracteres da string resultante
5. A soma dos dígitos da soma do passo anterior é feita
6. O resultado da soma do passo 4 é transformado em um binário de 6 dígitos
7. Caso o binário resultante já esteja em uso, ele é adicionado de uma letra, de "A" até "z", até que haja uma opção livre
8. Caso o binário não esteja em uso, ele é transformado em um binário de 7 dígitos

Ex: "https://www.netflix.com/browse"
1. ://.netflix.com/browse
2. netflixcombrowse
3. netflixcombrowse
4. 110 + 101 + 116 + 102 + 108 + 105 + 120 + 99 + 111 + 98 + 114 + 111 + 119 + 115 + 101 = 1739
5. 1+7+3+9 = 20
6. 20 = 010100
7. Caso esteja em uso -> 010100A
8. Caso não esteja em uso -> 0010100

## Testes

A api possui testes automatizados no projeto de teste chamado UrlShortenerTest.

## Database

A API tem integração com o banco de dados SQLite, com Entity Framework Core como ORM.
