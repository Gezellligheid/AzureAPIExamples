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
		