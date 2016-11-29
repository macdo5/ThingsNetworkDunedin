/*
 * Copyright (c) 2013 IBM Corp.
 *
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.eclipse.org/legal/epl-v10.html
 *
 * Contributors:
 *    Seth Hoenig
 *    Allan Stockdill-Mander
 *    Mike Robertson
 */

package main

import (
	"crypto/tls"
	"flag"
	"fmt"
	//"log"
	"os"
	"os/signal"
	"strconv"
	"syscall"
	"time"
	"encoding/json"
	b64 "encoding/base64"

	MQTT "github.com/eclipse/paho.mqtt.golang"
)

func onMessageReceived(client MQTT.Client, message MQTT.Message) {
	//fmt.Printf("Received message on topic: %s\nMessage: %s\n", message.Topic(), message.Payload())
	//fmt.Println();

	//String array
	var parsed map[string]interface{}

	//Convert the message payload into bytes
	data := []byte(message.Payload())
	//decode the bytes into an array
	err := json.Unmarshal(data, &parsed)
	//Check for no errors
	if err != nil {
        panic(err)
    }

	//fmt.Println(parsed["data"])
	//Decode the base64 data string
	sDec, _  := b64.StdEncoding.DecodeString(parsed["payload"].(string))
	//Output to stdout
	fmt.Println(string(sDec))
	f, err := os.OpenFile("mqttOutput.txt", os.O_APPEND|os.O_WRONLY, 0600)
	if err != nil {
		panic(err)
	}

	defer f.Close()

	if _, err = f.WriteString(string(sDec) + "\n"); err != nil {
	    panic(err)
	}
}

var i int64

func main() {
	if _, err := os.Stat("mqttOutput.txt"); os.IsNotExist(err) {
		os.Create("mqttOutput.txt")
	}

	//MQTT.DEBUG = log.New(os.Stdout, "", 0)
	//MQTT.ERROR = log.New(os.Stdout, "", 0)
	c := make(chan os.Signal, 1)
	i = 0
	signal.Notify(c, os.Interrupt, syscall.SIGTERM)
	go func() {
		<-c
		fmt.Println("signal received, exiting")
		os.Exit(0)
	}()

	hostname, _ := os.Hostname()

	server := flag.String("server", "tcp://127.0.0.1:1883", "The full url of the MQTT server to connect to ex: tcp://127.0.0.1:1883")
	topic := flag.String("topic", "#", "Topic to subscribe to")
	qos := flag.Int("qos", 0, "The QoS to subscribe to messages at")
	clientid := flag.String("clientid", hostname+strconv.Itoa(time.Now().Second()), "A clientid for the connection")
	username := flag.String("username", "", "A username to authenticate to the MQTT server")
	password := flag.String("password", "", "Password to match username")
	flag.Parse()

	connOpts := &MQTT.ClientOptions{
		ClientID:             *clientid,
		CleanSession:         true,
		Username:             *username,
		Password:             *password,
		MaxReconnectInterval: 1 * time.Second,
		KeepAlive:            30 * time.Second,
		TLSConfig:            tls.Config{InsecureSkipVerify: true, ClientAuth: tls.NoClientCert},
		AutoReconnect: 				true,
	}
	connOpts.AddBroker(*server)
	connOpts.OnConnect = func(c MQTT.Client) {
		if token := c.Subscribe(*topic, byte(*qos), onMessageReceived); token.Wait() && token.Error() != nil {
			panic(token.Error())
		}
	}

	client := MQTT.NewClient(connOpts)
	if token := client.Connect(); token.Wait() && token.Error() != nil {
		panic(token.Error())
	} else {
		fmt.Printf("Connected to %s\n", *server)
	}

	for {
		time.Sleep(1 * time.Second)
	}
}
