using Model;

namespace IntuitChallengeAPI
{
    public interface ICsvReaderService
    {
        List<Socio> ReadCsvFromFile();
        IEnumerable<Socio> ReadCsv(Stream fileStream);
    }


    public class CsvReaderService : ICsvReaderService
    {
        private const string CsvFileName = "socios.csv"; // Nombre del archivo CSV esperado en la raíz del proyecto

        public List<Socio> ReadCsvFromFile()
        {
            // Obtener la ruta del archivo CSV en la carpeta raíz
            var csvFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, CsvFileName);
            if (!System.IO.File.Exists(csvFilePath))
            {
                throw new FileNotFoundException($"El archivo {CsvFileName} no se encontró en la carpeta raíz del proyecto.", csvFilePath);
            }

            using (var fileStream = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read))
            {
                return ReadCsv(fileStream).ToList();
            }
        }

        public IEnumerable<Socio> ReadCsv(Stream fileStream)
        {
            var socios = new List<Socio>();

            using (var reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = line.Split(';');

                    if (values.Length != 5)
                        throw new FormatException($"Línea incorrecta en el archivo CSV: {line}");

                    socios.Add(new Socio
                    {
                        Nombre = values[0],
                        Edad = int.Parse(values[1]),
                        Equipo = values[2],
                        EstadoCivil = values[3],
                        NivelDeEstudios = values[4]
                    });
                }
            }

            return socios;
        }
    }
}
