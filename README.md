# Specifics of the Application

measurements.txt file was not commited due to the size of the file. It should be placed inside the Data folder.

## Use of interfaces
`FileWatcherService` and `CityTemperatureService` are implemented using an interface, allowing for better abstraction and testability.

## Data Storage and Automatic Recalculation
Storage and potential recalculation are encapsulated in the `FileWatcherService`. City data is stored in a dictionary. The class is implemented as a Singleton, which ensures that the `measurements.txt` file is processed only once (at the start of the application). The file path is stored in `appsettings.Development.json`, enabling easy modification of the data file path.

To calculate all temperatures, only one loop through the `measurements.txt` file is required. A city is added to the dictionary if it is not already present. At the end, the average temperature for each city is calculated based on the sum of all temperatures for that city and the count.

## Data Retrieval
The `CityTemperatureService` is used for data retrieval. It provides all the necessary functions for reading and filtering data.

## Controller
The controller implements the required REST endpoints. The filter method is represented as a POST call, where the body of the call contains the filter criteria. This design allows for easy modification and the addition of new filter values.

## Formating
On double attributes a custom `DoubleFormatConverter` is used. It rounds all the temperatures to 2 decimals (as average temperatures are all close to 0 the original value would probably be better, this is only used to provide consistency in number formatting).

## Swagger
Swagger is available at http://localhost:5211/swagger/index.html.
