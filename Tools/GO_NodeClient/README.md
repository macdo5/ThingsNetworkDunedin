# ttn-tools
## Go Node MQTT Client
Subscribes to the MQTT channel based on the given DeviceEUI, AppEUI & AppAccessKey and outputs received messages from the node to the log box.

### Requirments
* Go
* Docker (Optional)

### Quick Start
* `go get github.com/eclipse/paho.mqtt.golang`
* `go get golang.org/x/net/websocket`
* `go build .`
* `./mqtt -server tcp:/<hander_ip>:1883 -username <AppEUI> -password <AppAccessKey> -topic <AppEUI>/devices/<DeviceEUI>/up`

### Docker Quick Start
For ease of use/setup a dockerfile has been included
1. Edit the last line of the Dockerfile and set the ip, username, password, AppEUI & DeviceEUI
2. `docker build -t <username>/gomqtt .`
3. `docker run -i -t <username>/gomqtt`
