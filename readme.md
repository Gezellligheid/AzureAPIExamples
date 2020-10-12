# Azure Functions Examples
Created by Debaveye Elias

1. **GET** `GET.cs`

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
		

2. **POST** `POST.cs`

	* `FunctionName("POST_noDB")`

		Normal POSt Without database connection<br />
		Can be used for a simple calculation F.E.


	* `FunctionName("POSTWithDB")`

		Normal POST with database connection<br />
		Route can be changed however you want
		
		**Note:**
		Don't forget to add your ConnectionString to your local.settings.json and include it in the code. (see comments)


3. **MQTT**

	3.1 **MQTT Without Azure Functions** `MQTTNoFunctions.cs`
		
	* `Example on adding MQTT

		Example on how you would implement MQTT in a C# project<br />
		Covers **sending** and **receiving** messages

		Note: Don't forget to add the M2MQTT NuGet package [Package](https://www.nuget.org/packages/M2Mqtt/4.3.0?_src=template)


	3.2 **MQTT With Azure Functions** `MQTTFunctions.cs`

	* `FunctionName("MQTTFunctionsWithResend")`

		MQTT function that will execute included code when message is received at broker to specific topic<br />
		This function will send his own message to the same or different topic. This can be changed in code

		The data can be used for all sorts of end results (Database, ...)(see variablenames)

		Note: Don't forget to add the CaseOnline.Azure.WebJobs.Extensions.Mqtt package [package](https://www.nuget.org/packages/CaseOnline.Azure.WebJobs.Extensions.Mqtt/2.1.0?_src=template)


	* `FunctionName("MQTTFunctionsWithoutResend")`

			MQTT function that will execute included code when message is received at broker to specific topic<br/>
			This function will **not** send his own message towards the broker

			The data can be used for all sorts of end results (Database, ...)(see variablenames)

			Note: Don't forget to add the CaseOnline.Azure.WebJobs.Extensions.Mqtt package [package](https://www.nuget.org/packages/CaseOnline.Azure.WebJobs.Extensions.Mqtt/2.1.0?_src=template)