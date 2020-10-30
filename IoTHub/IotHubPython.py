import os
import json
import time
import random
import asyncio
import random
from azure.iot.device import MethodResponse
from azure.iot.device import IoTHubDeviceClient

#Connection String from device.
# IoT devices > YourDevice > Primary Connection String
conn_str = ""

#Create Device CLient
device_client = IoTHubDeviceClient.create_from_connection_string(conn_str)

## GETTING TWIN VARIABLE
def get_threshold():
    twin =  device_client.get_twin()
    threshold = twin["desired"]["threshold"] #Can be whatever you want
    return threshold

 ## MAIN CODE
def main():
   while True:
        #Start connection
        device_client.connect()
        temp = random.randint(0,50)
        if(temp>= get_threshold()):
            print(temp)
            print("Sending message...")
            #Send message to IoTHub as the device
            device_client.send_message(temp)
            time.sleep(3)
            print("Message successfully sent!")
        #Disconnect
        device_client.disconnect()

## RECEIVING COMMANDS
def method_request_handler(method_request):
    if method_request.name == "reboot":
        payload = {"result": True, "data": "some data"} 
        status = 200 

        ## RESENDING ANSWER
        method_response = MethodResponse.create_from_method_request(method_request, status, payload)
        device_client.send_method_response(method_response)
        print("executed reboot")

        ## SEND REPORTED PROPERTY TO TWIN
        reported_properties = {"bootstatus": "ok"}
        device_client.patch_twin_reported_properties(reported_properties)
    else:
        payload = {"result": False, "data": "unknown method"}
        status = 400 
        
        ## RESENDING ANSWER
        method_response = MethodResponse.create_from_method_request(method_request, status, payload)
        device_client.send_method_response(method_response)
        print("executed unknown method: " + method_request.name)

## TWIN DATA CHECKER
def twin_patch_handler(patch):
    print("the data in the desired properties patch was: {}".format(patch))

if __name__ == "__main__":
    ## CHECKEN OF TWIN DATA IS AANGEPAST
    device_client.on_twin_desired_properties_patch_received = twin_patch_handler

    ## CHECKEN OF EEN COMMAND IS GESTUURD
    device_client.on_method_request_received = method_request_handler

    main()  