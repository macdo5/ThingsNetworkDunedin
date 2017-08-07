 //Using the HiveMQ public Broker, with a random client Id
 var client = new Messaging.Client("localhost", 1884, "WebAppTester" );

 //Gets  called if the websocket/mqtt connection gets disconnected  for any reason
 client.onConnectionLost = function (responseObject) {
     //Depending on your scenario you could implement a reconnect logic here
     alert("connection lost: " + responseObject.errorMessage);
 };

 //Gets called whenever you receive a message for your subscriptions
 client.onMessageArrived = function (message) {
     //Do something with the push message you received
     console.log("Got a message, it arrived!");
     $('#messages').append('<span>Topic: ' + message.destinationName + '  | ' + message.payloadString + '</span><br/>');
 };

 //Connect Options
 var options = {
     timeout: 3,
     //Gets Called if the connection has sucessfully been established
     onSuccess: function () {
         alert("Connected");
     },
     //Gets Called if the connection could not be established
     onFailure: function (message) {
         alert("Connection failed: " + message.errorMessage);
     }
 };
