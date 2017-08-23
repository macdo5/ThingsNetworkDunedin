"""
Original Author: Pradeep Singh
Date: 20th January 2017
Version: 1.0
Python Ver: 2.7
Details At: https://iotbytes.wordpress.com/store-mqtt-data-from-sensors-into-sql-database/
This script subscribes to a LoRa MQTT broker, such as loraserver, Loriot or TTN.
It processes the received MQTT message into a matching mongo database format and stores it.
For mongo collection format and information, refer to documentation on GitHub.
"""

import datetime
import json
import paho.mqtt.client as mqtt
#from store_Sensor_Data_to_DB import sensor_Data_Handler
from pymongo import MongoClient

# MQTT Settings 
MQTT_Broker = "10.118.0.142"
MQTT_Port = 1883
Keep_Alive_Interval = 45
MQTT_Topic = "application/#" # subscribe to all incoming messages that begin with application/

# MongoDB settings
# from http://api.mongodb.com/python/current/tutorial.html
client = MongoClient()
db = client.duniot_database
mqtt_collection = db.duniot_collection

# Subscribe to the specified topic
def on_connect(mqttc, mosq, obj, rc):
    mqttc.subscribe(MQTT_Topic, 0)


# When the message is received, it is processed and stored in the database.
def on_message(mosq, obj, msg):
    # This is the Master Call for saving MQTT Data into DB
    print("MQTT Data Received...")
    print("MQTT Topic: " + msg.topic)
    print("MQTT Payload: " + msg.payload)
    # create a json object from the received mqtt data
    message_json = json.loads(msg.payload)
    # print the time that the gateway received the data.
    print("time:" + message_json['rxInfo'][0]['time'])
    # from https://stackoverflow.com/questions/127803
    # Take the string for 'time' and convert into ISO-datetime format 8601DZ
    message_json['rxInfo'][0]['time'] = datetime.datetime.strptime(message_json['rxInfo'][0]['time'], "%Y-%m-%dT%H:%M:%S.%fZ")

    # Add the MQTT data to MongoDB
    # First, check that the device is already in the database.
    # create a boolean object (1 is True, 0 is False) from the result of the database query
    # Check the database if at least a single item exists with the matching criteria
    # A combination of applicationID, devEUI and nodeName will ensure single unique nodes per application exist.
    found = mqtt_collection.find({
        "nodeName" : message_json['nodeName'],
        "applicationID" : message_json['applicationID'],
        "devEUI" : message_json['devEUI']
    }).limit(1)
    # returns a json object that is either populated or not populated
    # dynamic JSON building in python (https://stackoverflow.com/questions/23110383)
    if found["_id"] == "":  # id is null, therefore item does not exist in db
        print("node not found in database, creating new node...")
        node_entry = {}
        node_entry['applicationName'] = message_json['applicationName']
        node_entry['nodeName'] = message_json['nodeName']
        node_entry['dataEntries'] = [
            {
                "data": message_json['rxInfo'][0]['data'],
                "gwTime": message_json['rxInfo'][0]['time']
            }
        ]
        node_entry['applicationID'] = message_json['applicationID']
        print("inserting into duniot_database.mqtt_collection")
        new_entry_id = mqtt_collection.insert_one(node_entry).inserted_id
        print("Success. Entry ID is " + str(new_entry_id))
    else:   # node exists, append data to node entry in db
        print("extracting data")
        # build the new node json:
        data_entry = {}
        data_entry["data"] = message_json['rxInfo'][0]['data']
        data_entry["geTime"] = message_json['rxInfo'][0]['time']
        # push the data onto the end of the dataEntries
        print("pushing data to data entries in database")
        mqtt_collection.update(
            {"_id": found["_id"]}, {"$push": {"dataEntries": data_entry}}
        )
        print("node data entries updated")


def on_subscribe(mosq, obj, mid, granted_qos):
    pass

mqttc = mqtt.Client()
print("connecting...")
# Assign event callbacks
mqttc.on_message = on_message
mqttc.on_connect = on_connect
mqttc.subscribe("#")
print("subscribed")
# Connect
mqttc.connect(MQTT_Broker, int(MQTT_Port), int(Keep_Alive_Interval))
print("connected")
# Continue the network loop
mqttc.loop_forever()

