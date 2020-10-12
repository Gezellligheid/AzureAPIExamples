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

3.1 **MQTT Without Azure Functions**

3.2 **MQTT With Azure Functions**