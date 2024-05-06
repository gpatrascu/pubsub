# pubsub

## small pubsub broker - 
* For the beginning 
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

  an API that will use the pubsub framework to publish a message after a transfomation

ex: Submit an order
messages to be published: OrderSubmitted

 ## CFR:

* extensibility 
  * should support different pub sub brokers.

## A subscriber on the previous topic

* a simple console that will print the message
  * I think a subscriber should be enough. I can start a few instances in parallel for a demo.
  Subscribers : 
  * NotificationService,
  * WarehouseService,
  * ReportingService

  