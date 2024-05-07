# pubsub

## small pubsub broker - 
* For the beginning 
  * naive implementation. For the moment is using a simple List. Might be changed to use channels. 
    * no authentication - anybody can write and read.
    * the endpoints can be http. 
    * nice to have - subscribers receive never ending streams. but not a prio.
    * the storage can be a simple list in memory (or a dictionary if we use topics)

      * This solution should be extensible to support
          * different storage options.
          * different way to expose functionality (TCP sockets, grpc, etc..)
          * DLQ per subscriber
          * topic management - 

## API to receive an input and send a message to a topic

A submit order is sent. 
This order is enhanced with some product catalog information.
An integration event is sent to the broker.

I used clean architecture to separate the concerns. 
I know this might be a bit overkill, but I did this in order to keep the design extensible.
* we can use different types of API in the future (grpc, azure functions, etc...). 
  * This is the reason the logic is delegated to the handlers (application services layer) 
* we can use a different broker in the future (kafka, amqp, redis, etc..)
* we can use different storage options

I added a few integration tests and a few unit tests.
Integration tests mock the broker and the external services. 
Unit tests use Mocks to test one handler

Another overkill thing might be that I used the domain event.
I am raising a domain event. I have a handle for it that makes a call to the infrastructure to send the integration event to the broker.

 ## CFR:

* extensibility `
  * should support different pub sub brokers.`

## A subscriber on the previous topic

* a simple console that will print the message
  * I think a subscriber should be enough. I can start a few instances in parallel for a demo.
  Subscribers : 
  * NotificationService,
  * WarehouseService,
  * ReportingService

  