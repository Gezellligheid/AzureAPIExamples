# Azure Functions Examples
Created by Debaveye Elias

1. **GET** `GET/GET.cs`

	* `FunctionName("GET_NoParams")`

		Normal GET without parameters<br />
		Route can be changed however you want


	* `FunctionName("GET_Params")`

		Normal GET with parameters<br />
		Route can be changed however you want
		
		**Note:**	
		Don't forget to add your parameters to your function


	* `FunctionName("GET_noParams_withDB")`

		GET without parameters with database<br />
		Route can be changed however you want
		
		**Note:**
		Don't forget to add your ConnectionString to local.settings.json and include it in the code. (see comments)


	* `FunctionName("GET_Params_withDB")`

		GET with parameters with database<br />
		Route can be changed however you want

		
		**Note:**
		Don't forget to add your ConnectionString to local.settings.json and include it in the code. (see comments)<br />
		Don't forget to add your parameters to your function
		

2. **POST** `POST/POST.cs`

	* `FunctionName("POST_noDB")`

		Normal POSt Without database connection<br />
		Can be used for a simple calculation F.E.


	* `FunctionName("POSTWithDB")`

		Normal POST with database connection<br />
		Route can be changed however you want
		
		**Note:**
		Don't forget to add your ConnectionString to your local.settings.json and include it in the code. (see comments)


3. **MQTT**

	3.1 **MQTT Without Azure Functions** `MQTT/MQTTNoFunctions.cs`
		
	* `Example on adding MQTT`

		Example on how you would implement MQTT in a C# project<br />
		Covers **sending** and **receiving** messages

		**Note:** Don't forget to add the M2MQTT NuGet package<br />
		[Package](https://www.nuget.org/packages/M2Mqtt/4.3.0?_src=template)


	3.2 **MQTT With Azure Functions** `MQTT/MQTTFunctions.cs`

	* `FunctionName("MQTTFunctionsWithResend")`

		MQTT function that will execute included code when message is received at broker to specific topic<br />
		This function will send his own message to the same or different topic. This can be changed in code

		The data can be used for all sorts of end results (Database, ...)(see variablenames)

		**Note:** Don't forget to add the CaseOnline.Azure.WebJobs.Extensions.Mqtt package<br />
		[package](https://www.nuget.org/packages/CaseOnline.Azure.WebJobs.Extensions.Mqtt/2.1.0?_src=template)


	* `FunctionName("MQTTFunctionsWithoutResend")`

		MQTT function that will execute included code when message is received at broker to specific topic<br/>
		This function will **not** send his own message towards the broker

		The data can be used for all sorts of end results (Database, ...)(see variablenames)

		**Note:** Don't forget to add the CaseOnline.Azure.WebJobs.Extensions.Mqtt package<br />
		[package](https://www.nuget.org/packages/CaseOnline.Azure.WebJobs.Extensions.Mqtt/2.1.0?_src=template)


4. **Azure Storage**
	
	4.1 **Table Storage** `Storage/CloudTables.cs`

	Both Functions need a TableEntity class to send data to the cloud storage

	* `FunctionName("POSTWithAzureStorage")`

		Azure POST function that will create an instance of ExampleRequest and put this in a Table storage.<br />
		This can be a replacement to a SQL database.

		**Note:** Don't forget to add your ConnectionString for you Storage Acoount in local.settings.json<br />
		Don't forget to write your Tablename correctly<br />
		Don't forget to add the Microsoft.Azure.Cosmos.Table package<br />
		[package](https://www.nuget.org/packages/Microsoft.Azure.Cosmos.Table/1.0.8?_src=template)


	* `FunctionName("GETWithAzureStorage")`

		Azure GET function that will get all rows or certain rows with a specific value in a specific column. <br />
		Be mindfull of parameters and change them accordingly!

		**Note:** Don't forget yo add your ConnectionString for your Storage Account in local.settings.json<br />
		Don't forget to write your Tablename correctly<br />
		Don't forget to add the Microsoft.Azure.Cosmos.Table package<br />
		[package](https://www.nuget.org/packages/Microsoft.Azure.Cosmos.Table/1.0.8?_src=template)


	4.2 **Queues** `Queues/Queues.cs`

	* `FunctionName("Queues")`

		Function will be triggered when data enters a queue<br/>
		Includes function to add a certain instance to the queue

		**Note:** Don't forget to add the ConnectionString to local.settings.json<br/>
		Don't forget to add the Azure.Storage.Queues package<br/>
		[package](https://www.nuget.org/packages/Azure.Storage.Queues/12.4.2?_src=template)


5. **IoTHub**

	5.1 **Listen to messages** `IoTHub/IoTHubListener.cs`

	* `FunctionName("IoTHubListener")`

		Listens to all messages to the IoTHub

		**Note:** Don't forget to add you ConnectionString from **YourIoTHub > Built-in endpoints > Event Hub compatible endpoint**


	5.2 **Twins** `IoTHub/IoTHubTwinEdits.cs`

	* `FunctionName("GetDevices")`

		Will get all Twins from devices registered in the IoTHub

		**Note:** Requires the AdminConnectionString **YourIoTHub > Shared access policies > iothubowner**<br/>
		Don't  forget to install the Microsoft.Azure.Devices package<br/>
		[package](https://www.nuget.org/packages/Microsoft.Azure.Devices/1.28.0-preview-001)

	* `FunctionName("changeValue")`

		Will change a value you specified in a twin of a device you specified via a GET trigger

		**Note:** Requires the AdminConnectionString **YourIoTHub > Shared access policies > iothubowner**<br/>
		Don't  forget to install the Microsoft.Azure.Devices package<br/>
		[package](https://www.nuget.org/packages/Microsoft.Azure.Devices/1.28.0-preview-001)


	5.3 **Messages** `IoTHub/IoTSending.cs`

	* `FunctionName("IoTSending")`

		Will send messages OR send direct method to the device in the query params

		**Note:** Requires the AdminConnectionString **YourIoTHub > Shared access policies > iothubowner**<br/>
		Don't  forget to install the Microsoft.Azure.Devices package<br/>
		[package](https://www.nuget.org/packages/Microsoft.Azure.Devices/1.28.0-preview-001)


	5.4 **Device** `IoTHub/IoTHubPython.py`

	* `FunctionName("GetDevices")`

		Will simulate a Device connected to the IoTHub<br/>
		- Sends data to the IotHub<br/>
		- Gets data from Device Twin (threshold)<br/>
		- Triggers event when new Twin is available

		**Note:** Requires the DeviceConnectionString **YourIoTHub > Iot Devices > YourDevice > Primary Connection String**<br/>
		Don't forget to install `pip install azure-iot-device`<br/>
	

6. **CosmosDb**

	6.1 **Add item to the database** `COSMOS/CosmosFunctions.cs`

	* `FunctionName("AddItemToCosmos")`

		Adds new item to a specific comsos database in a specific container

		**Note:** Don't forget to add you ConnectionString from **YourCosmosDatabase > Keys > Primary ConnectionString**<br/>
		Don't forget to install the Microsoft.Azure.Cosmos package<br/>
		[package](https://www.nuget.org/packages/Microsoft.Azure.Cosmos/3.15.0?_src=template)

