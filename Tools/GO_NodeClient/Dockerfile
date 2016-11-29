FROM golang:latest

RUN go get github.com/eclipse/paho.mqtt.golang
RUN go get golang.org/x/net/websocket

ADD mqtt.go /go/src/github.com/user/mqtt/

WORKDIR /go/src/github.com/user/mqtt

RUN go build .

CMD ./mqtt -server tcp://10.25.2.62:1883 -username 70B3D57ED0000D2C -password HYPinKzZYDpjHYoswBNrvuTmQ5WHD2Mq4yOfgTFyITI= -topic 70B3D57ED0000D2C/devices/0102030405060708/up
