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

## O Projeto ChatServer

Este sistema aceita conexões TCP por padrão em localhost (127.0.0.1), porta 8081, máximo de usuários igual a 10 (dez) e máximo de conexões igual a 10 (dez). Estas configurações podem ser alteradas através da linha de comando de inicialização ajustando seus valores respectivamente na mesma sequência.

Com ele podemos:

* Enviar mensagens exclusivas do administrador.
* Enviar mensagens para todos os usuários conectados.
* Enviar mensagens para um usuário específico.

Ele controla e informa sobre o início e fim de conexões de usuários em geral.

## O Projeto ChatClient

Este sistema inicia conexões fixas para localhost (127.0.0.1) e porta 8081. Estas informações atualmente não podem ser alteradas. Inicialmente o sistema solicita um nome de usuário para criar uma conexão TCP com o servidor ChatServer.

Caso a conexão seja aceita então podemos utilizar os seguintes comando:

* /exit = sair do sistema.
* /logout = trocar de usuário.
* /pub = enviar uma mensagem pública em broadcast.
* /priv = enviar uma mensagem privada para um usuário específico.

Quando utilizado o comando */priv* então será solicitado um nome de usuário conectado no servidor ChatServer. Caso este nome não seja válido a mensagem não será encaminhada para ninguém.


