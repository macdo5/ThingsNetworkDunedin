## Python Fake Gateway/Node - Temp Sensor
Arduino Temperature sensor that outputs the current temperature every 1 minute to serial out.    
Consumed in the python code then sent through the network.    

Emulates being a gateway & node, allowing a configured message to be sent to the MQTT channel.


### Requirments
* [ttnctl](https://www.thethingsnetwork.org/wiki/Backend/ttnctl/QuickStart)    
 * Install Scripts:
  Downloads ttnctl and installs in /usr/bin, configures the correct address for the broker, router & hander for the test enviroment
   * [RaspberryPi](../ttnctl-pi-setup.sh)
   * [Linux x64](../ttnctl-linux-setup.sh)

* Requires a AppKey, DeviceEUI & AppEUI that have been pre registered with the hander.
