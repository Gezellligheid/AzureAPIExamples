# Azure Functions Examples
Created by Debaveye Elias

1. **GET** `GET.cs`

	* `FunctionName("GET_NoParams")`

		Normal GET without parameters
		Route can be changed however you want


	* `FunctionName("GET_Params")`

		Normal GET with parameters
		Route can be changed however you want
		
		**Note:**
		Don't forget to add your parameters to your function


	* `FunctionName("GET_noParams_withDB")`

		GET without parameters with database
		Route can be changed however you want
		
		**Note:**
		Don't forget to add your ConnectionString to local.settings.json and include it in the code. (see comments)


	* `FunctionName("GET_Params_withDB")`

		GET with parameters with database
		Route can be changed however you want
		
		**Note:**
		Don't forget to add your ConnectionString to local.settings.json and include it in the code. (see comments)
		Don't forget to add your parameters to your function
		

2. **POST** `POST.cs`

3. **MQTT**

3.1 **MQTT Without Azure Functions**

3.2 **MQTT With Azure Functions**