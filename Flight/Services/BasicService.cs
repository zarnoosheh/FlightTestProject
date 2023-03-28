using DataAccessLayer.ViewModel;
using Microsoft.Extensions.Logging;
using ServicesLayer.Common;
using ServicesLayer.Contract;
using System.Diagnostics;
using System.Globalization;

namespace Flight.Services
{
    public class BasicService
    {
        private readonly IFlightChangeDetectorService _flightChangeDetector;
        private readonly ILogger<BasicService> _logger;
        public BasicService(ILogger<BasicService> logger, IFlightChangeDetectorService flightChangeDetector)
        {
            _logger = logger;
            _flightChangeDetector = flightChangeDetector;
        }

        public async Task MainMethod(string[] args)
        {
            if (args.Length != 3)
            {
                _logger.LogInformation("Usage: FlightFilterApp.exe <start-date> <end-date> <agency-id>");
                return;
            }
            if (!DateTime.TryParseExact(args[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                _logger.LogInformation("Invalid start date format. Please use yyyy-MM-dd format.");
                return;
            }
            if (!DateTime.TryParseExact(args[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                _logger.LogInformation("Invalid end date format. Please use yyyy-MM-dd format.");
                return;
            }
            if (!int.TryParse(args[2], out int agencyId))
            {
                _logger.LogInformation("Invalid agency id format. Please enter a valid integer.");
                return;
            }
            _logger.LogInformation($"Filtering flights for agency {agencyId} between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}.");
            var stopwatch = Stopwatch.StartNew();
            var (Flights, Routes) = await _flightChangeDetector.Filter(agencyId, startDate, endDate);
            _logger.LogInformation($"Time taken to execute the FilterFlightsFromDb: {stopwatch.Elapsed.TotalMilliseconds} ms");
            stopwatch.Stop();
            stopwatch = Stopwatch.StartNew();
            var (newFlights, discontinuedFlights) = await _flightChangeDetector.DetectionAlgorithm(Flights, Routes);
            _logger.LogInformation($"Time taken to execute the change detection algorithm: {stopwatch.Elapsed.TotalMilliseconds} ms");
            stopwatch.Stop();
            var resultFlights = new List<ResultFlightViewModel>();
            foreach (var flight in newFlights)
            {
                resultFlights.Add(new ResultFlightViewModel(flight, "New"));
            }
            foreach (var flight in discontinuedFlights)
            {
                resultFlights.Add(new ResultFlightViewModel(flight, "Discontinued"));
            }
            CsvWriterService.WriteCsv("result/results.csv", resultFlights);
            _logger.LogInformation($"results.csv File Created Successfully");
        }

    }
}
