using System.Globalization;
using indigolabs_demo.Models;

namespace indigolabs_demo.Services
{
    public class FileWatcherService : IFileWatcherService
    {
        private FileSystemWatcher _fileWatcher;
        private Dictionary<string, CityTemperatureModel> _cityDictionary;
        private readonly string _filePath;

        public FileWatcherService(IConfiguration configuration)
        {
            _cityDictionary = new Dictionary<string, CityTemperatureModel>();
            _filePath = configuration["FileWatcherConfig:CityFilePath"] ?? throw new ArgumentNullException(nameof(configuration), "FileWatcherConfig:CityFilePath is not configured."); ;
            CalculateTemperatures(_filePath);
            InitializeFileSystemWatcher();
        }

        public Dictionary<string, CityTemperatureModel> GetCityDictionary()
        {
            return _cityDictionary;
        }

        // Initialize fileWatcher for automatic recalculation of city temperatures
        private void InitializeFileSystemWatcher()
        {
            _fileWatcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(_filePath),
                Filter = Path.GetFileName(_filePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };

            _fileWatcher.Changed += OnFileChanged;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            CalculateTemperatures(_filePath);
        }

        // Generate the city dictionary based on input file
        private void CalculateTemperatures(string filePath)
        {
            Console.WriteLine("Starting sync");
            _cityDictionary.Clear();
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string[] data = line.Split(";");
                        if (data.Length != 2)
                        {
                            throw new Exception("Incorrect format");
                        }
                        string cityName = data[0];
                        double temperature;
                        if (data[1] != null && Double.TryParse(data[1], CultureInfo.InvariantCulture, out double temp))
                        {
                            temperature = temp;
                        }
                        else
                        {
                            throw new Exception("Incorect temperature format");
                        }

                        if (!_cityDictionary.TryGetValue(cityName, out var cityTemperature))
                        {
                            cityTemperature = new CityTemperatureModel();
                            cityTemperature.Name = cityName;
                            _cityDictionary[cityName] = cityTemperature;
                        }
                        UpdateCityTemperatureDTO(cityTemperature, temperature);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading row: {ex.Message}");
                        continue;
                    }
                }
            }

            foreach (var cityTemperature in _cityDictionary.Values)
            {
                cityTemperature.AvgTemperature = cityTemperature.SumTemperature / cityTemperature.Count;
            }
            Console.WriteLine("Ending sync");
        }

        // Recalculate the temperatures for a specific city
        private void UpdateCityTemperatureDTO(CityTemperatureModel cityTemperature, double temperature)
        {
            if (cityTemperature.MinTemperature == null || temperature < cityTemperature.MinTemperature)
            {
                cityTemperature.MinTemperature = temperature;
            }
            else if (cityTemperature.MaxTemperature == null || temperature > cityTemperature.MaxTemperature)
            {
                cityTemperature.MaxTemperature = temperature;
            }

            cityTemperature.SumTemperature += temperature;
            cityTemperature.Count += 1;
        }
    }
}
