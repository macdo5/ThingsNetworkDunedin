#This code reads from an Arduino#
#from datetime import datetime
import serial
import time
import datetime
import json

#Create seral connection
ser = serial.Serial('/dev/ttyACM0',9600)

#Loop forever
while True:
        #Read temp details from serial port
        read_serial = ""
        #Check that a temperture reading has come from the arduino        
        while (read_serial[:4] != "Cels"):
                read_serial = ser.readline()

        #Strip off the newline character from the arduino
        temp = read_serial[:-2]
        #Get the currentDate
        todayDate = time.strftime('%x')
        #Get the currentTime
        timeATM = time.strftime('%I:%M:%S')
        
        #Print out in json format
        print json.dumps({'Date': todayDate,
                          'Time': timeATM,
                          'Temperature': temp},
                         sort_keys=True, indent=4, separators=(',', ': '))
