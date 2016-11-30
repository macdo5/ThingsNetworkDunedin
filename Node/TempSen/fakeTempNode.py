import subprocess
import binascii
import time
import random
import json
import serial
import datetime

# Python Fake Node
# Fakes being a node, by using ttncl to bypass the connection to the gateway and
# Elimating the need for the wireless chip
# Using ttnctl to authenticate and send messages to the broker/router/handler
# Lewis Gearing, September 2016

# Define App/Device Keys for OTAA Auth
devEUI = "0102030405060708"
appEUI = "70B3D57ED0000D2C"
appKey = "B549EB2D0CFF76F7069E5D7E0287ABD0"

# OTAA Auth Keys, Set when JoinOTAA() is run
devAddr = ""
nwkSKey = ""
appSKey = ""


# Gets the DevAddr, nwkSKey & appSKey required to send messages
def JoinOTAA():
	# Access the global variables
    global devAddr, appEUI, appKey, devAddr, nwkSKey, appSKey
    # Execute the ttnctl application and collect its stdout
    process = subprocess.Popen(['ttnctl', 'join', devEUI, appKey, '--app-eui', appEUI], stdout=subprocess.PIPE)
    output = process.communicate()[0]

    # Convert the stdout into a list
    output = output.split('\n')

    # Find the keys in the stdout & store them into the global variables
    for line in output:
        if "Device Address:" in line:
            num = line.index(':') + 2
            devAddr = line[num:].strip()
        if "Network Session Key:"  in line:
            num = line.index(':') + 2
            nwkSKey = line[num:].strip()
        if "Application Session Key:"  in line:
            num = line.index(':') + 2
            appSKey = line[num:].strip()

# Converts the given message into HEX then sends it to the MQTT queue for the given app
def SendMessage(message):
	# Access the global variables
    global devAddr, appEUI, appKey, devAddr, nwkSKey, appSKey

    # Convert the string message into hex
    payload = binascii.hexlify(message)

    # Execute the ttnctl application to send the message to the MQTT queue
    process = subprocess.Popen(['ttnctl', 'uplink', 'false', devAddr, nwkSKey, appSKey, payload, '5'], stdout=subprocess.PIPE)
    output = process.communicate()[0]

# Connects to the serial output from the arduino, passing it to ttnctl to send

#Create seral connection
ser = serial.Serial('/dev/ttyACM0',9600)

#Loop forever
while True:
        #Read temp details from serial port
        read_serial = ""
        #Check that a temperture reading has come from the arduino
        while (read_serial[:4] != "Cels"):
                read_serial = ser.readline()

        data = {}
	ts = int(time.time())
	data["Timestamp"] = ts
        #Strip off the newline character from the arduino
        data["Temperature"] = float(read_serial[9:-2])
        #Get the currentDate
        data["Date"] = time.strftime('%x')
        #Get the currentTime
        data["Time"] = time.strftime('%I:%M:%S')

        message = json.dumps(data, sort_keys=True)
        print(message)
        JoinOTAA()
        SendMessage(message)
