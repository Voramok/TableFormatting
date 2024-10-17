using TableFormatting.Functions;
using TableFormatting.Models;

namespace TableFormatting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employees employees = new Employees();
            bool endApp = false;

            while (!endApp)
            {
                Console.WriteLine("Вставьте или введите путь до файла: ");

                //Get entered file path
                string? filePath = Console.ReadLine();

                //Check file path
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"Файл по пути: {filePath} найден");

                    //Read file
                    List<Employee> employeesList = employees.ReadTXT(filePath);

                    //Sort data
                    List<string> sortedCodes = employees.SortCodes(employeesList);

                    //Write sorted data to new file
                    employees.WriteTXT(sortedCodes, filePath);

                }
                else
                {
                    Console.WriteLine($"Файл по пути: {filePath} не найден");
                }

                Console.WriteLine($"Нажмите q + Enter для выхода из программы, " +
                    $"или нажмите любую другую клавишу + Enter для продолжения работы: ");
                if (Console.ReadLine() == "q") endApp = true;

                Console.WriteLine("\n");
            }
        }
    }
}
