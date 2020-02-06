# IPStackSolution

This WebAPi is a simple startup project using ASP.NET Core 3.0 EntityFramework Core and .Net MemoryCache.
It consumes IPStack service(https://ipstack.com/) which provides information about IP addresses.
The final user will make a request to the api to get information about the requested IP (City, Country, Continent, Latitude, Longitude)
Below is the flowchart of a request for getting IP details: 
![GetIpDetails](https://github.com/afroditiTzifa/IPStackSolution/blob/master/GetIPDetailsFlowChart.PNG)


Also the api supports a batch operation for updating IP details. It expose a method where a post request can be made providing an array of items of IP details that should be updated
Bellow is the flowchart outlining what batch update method is expected to do: 
![UpdateIpDetails](https://github.com/afroditiTzifa/IPStackSolution/blob/master/PostIPDetailsFlowChart.PNG)


## Getting Started

The easiest way to use these samples without using Git is to download the zip file containing the current version. You can then unzip the entire archive and use the WebAPi in Visual Studio.

## Request & Response Examples

#### GET
http://localhost:5000/api/ipstack/[ip]/
  ```
  Response body:
  {
      "city": "Athens",
      "country": "Greece",
      "continent": "Europe",
      "latitude": 37.96998977661133,
      "longitude": 23.719989776611328
  }
  ```

#### POST
http://localhost:5000/api/ipstack/
```
RequestBody:
[
    {"IP": "ip1", 
    "City": "Athens", 
    "Country": "Greece",
    "Continent": "Europe", 
    "Latitude": 50.96998977661133,
    "Longitude": 50.719989776611328
},
   {"IP": "ip2", 
    "city": "Athens",
    "country": "Greece",
    "continent": "Europe",
    "latitude": 37.96998977661133,
    "longitude": 23.719989776611328
},
```
```
Response:"0a31ad59-2d26-47cf-b01b-e4b0ccf86eac"
```


## Authors

* **Afroditi Tzifa** - (https://github.com/afroditiTzifa)
