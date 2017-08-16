#------------------------------------------
#--- Original Author: Pradeep Singh
#--- Date: 20th January 2017
#--- Version: 1.0
#--- Python Ver: 2.7
#--- Details At: https://iotbytes.wordpress.com/store-mqtt-data-from-sensors-into-sql-database/
#------------------------------------------

import datetime
import json
import paho.mqtt.client as mqtt
#from store_Sensor_Data_to_DB import sensor_Data_Handler
from pymongo import MongoClient

# MQTT Settings 
MQTT_Broker = "10.118.0.142"
MQTT_Port = 1883
Keep_Alive_Interval = 45
MQTT_Topic = "application/+/node/+/rx" # display only the RX payloads for nodes

# MongoDB settings
# from http://api.mongodb.com/python/current/tutorial.html
client = MongoClient()
db = client.duniot_database
mqtt_collection = db.duniot_collection

# Subscribe to all Sensors at Base Topic
def on_connect(mqttc, mosq, obj, rc):
    mqttc.subscribe(MQTT_Topic, 0)


# Save Data into DB Table
def on_message(mosq, obj, msg):
    # This is the Master Call for saving MQTT Data into DB
    # For details of "sensor_Data_Handler" function please refer "sensor_data_to_db.py"
    print("MQTT Data Received...")
    print("MQTT Topic: " + msg.topic)
    print(msg.payload)
    new_mqtt_data = json.loads(msg.payload)
    print("time:")
    print(new_mqtt_data['rxInfo'][0]['time'])
    # from https://stackoverflow.com/questions/127803
    # Take the string for 'time' and convert into ISO-datetime format 8601DZ
    new_mqtt_data['rxInfo'][0]['time'] = datetime.datetime.strptime(new_mqtt_data['rxInfo'][0]['time'], "%Y-%m-%dT%H:%M:%S.%fZ")
    # Add the MQTT data to MongoDB
    print("inserting into duniot_database.mqtt_collection")
    new_entry_id = mqtt_collection.insert_one(new_mqtt_data).inserted_id
    print("Success. Entry ID is " + str(new_entry_id))


def on_subscribe(mosq, obj, mid, granted_qos):
    pass

mqttc = mqtt.Client()
print("print connecting")
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

