using CsvHelper;
using System.Globalization;

namespace ServicesLayer.Common
{

    public static class CsvReaderService
    {
        public static List<T> ReadCsv<T>(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<T>(csv.GetRecords<T>());
            }
            catch (Exception E)
            {
                Console.WriteLine($"Error :{E.Message}");
                return new List<T>();
            }

        }
    }

    public static class CsvWriterService
    {
        public static void WriteCsv<T>(string filePath, IEnumerable<T> records)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(records);
        }
    }
}
