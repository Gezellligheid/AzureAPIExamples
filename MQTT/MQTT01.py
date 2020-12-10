import paho.mqtt.client as mqtt

def on_connect(client,userdata,flags,rc):
    print('connected with result ' +str(rc))

def on_message(client, userdata,msg):
    print(msg.topic+' '+str(msg.payload))
    if "test" in str(msg.payload):
        print ('Hello World')
    else:
        print ('Goodbye World')


client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

client.connect("13.81.105.139", 1883,60)
client.subscribe("/vandevelde/demos")
client.loop_forever()