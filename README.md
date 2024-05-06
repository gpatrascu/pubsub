# pubsub

* small pubsub broker - It is not clear for me if this is part of the requirement. (The fact that the solution has to be pure c# make me wonder) (libraries can be used, but can I use an existing broker?)
    * For the beginning 
        * no authentication - anybody can write and read.
        * the endpoints can be http. 
        * nice to have - subscribers receive never ending streams. but not a prio.
        * the storage can be a simple list in memory (or a dictionary if we use topics)

    This solution should be extensible to support
        * different storage options.
        * different way to expose functionality (TCP sockets, grpc, etc..)
        * DLQ per subscriber
        * topic management - 

* an API that will use the pubsub framework to publish a message after a transfomation
 CFR:
    * extensibility 
        * should support different pub sub brokers. 


* a subscriber - a simple console that will print the message
* I think a subscriber should be enough. I can start a few instances in paralel for a demo.