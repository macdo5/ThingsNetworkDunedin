import subprocess
import binascii
import time
import random
import json

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

# Call the functions
for i in range(10):
    ts = int(time.time())
    temp = random.randrange(0,25)
    # Message to be Sent
    data = {}
    data["temp"] = temp
    data["timestamp"] = ts
    message = json.dumps(data)
    JoinOTAA()
    SendMessage(message)
    print("Message Sent, Waiting 1 Minute...")
    time.sleep(60)
