# Desafio TCP Chat

![Compatibility](https://img.shields.io/badge/compatibility-.Net%20Core%205.0%20-blue.svg)

## Atendimento aos Requisitos

Para atender aos requisitos do desafio o objetivo era construir um servidor e cliente de bate papo que implemente as seguintes funcionalidades básicas:

* Registro de apelido
* Envio de mensagem pública para a sala
* Envio de mensagem pública para um usuário
* Sair do bate-papo

**OBS**: Os requisitos opcionais não foram implementados.

Foram criados três projetos para as aplicações, são eles:

* ChatClient = Cliente de Bate Papo.
* ChatServer = Servidor de Bate Papo.
* ChatClientTest = Casos de testes unitários para verificar se os sistemas estão funcionando corretamente.

## O Projeto ChatClientTest

Este projeto cobre as funcionalidades de:

* Conexão de vários clientes de bate papo com o servidor.
* Envio de mensagem privada.
* Envio de mensagem pública.
* Fim de conexão para cada cliente.


