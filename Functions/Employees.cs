using TableFormatting.Models;

namespace TableFormatting.Functions
{
    internal class Employees
    {
        //Global funcs
        #region Global funcs

        /// <summary>
        /// Function for reading .txt file
        /// </summary>
        /// <param name="filePath">File path to readable file</param>
        /// <returns>Employee list</returns>
        internal List<Employee> ReadTXT(string filePath)
        {
            var employees = new List<Employee>();
            var lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(new[] {'\t'}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    var name = string.Join("\t", parts.Take(parts.Length - 2));
                    var code = int.Parse(parts[parts.Length - 2]);
                    var position = parts.Last();

                    employees.Add(new Employee { Name = name.Trim(), Code = code, Position = position.Trim() });
                }
            }

            return employees;
        }

        /// <summary>
        /// Function for sort employees names by employees positions
        /// </summary>
        /// <param name="employees">Employees list</param>
        /// <returns>Sorted by positions string list</returns>
        internal List<string> SortCodes(List<Employee> employees)
        {
            var groupedCodes = employees
            .GroupBy(e => e.Position)
            .Select(g => new
            {
                Position = g.Key,
                Codes = g.Select(e => e.Code).OrderBy(c => c).ToList()
            });

            //Create new table
            var resultLines = new List<string> { "Коды\tДолжность" };
            foreach (var group in groupedCodes)
            {
                var codesList = group.Codes;
                var rangesList = GetRanges(codesList);
                Console.WriteLine($"{rangesList}");
                resultLines.Add($"{string.Join(",", rangesList)}\t{group.Position}");
            }

            return resultLines;
        }

        /// <summary>
        /// Function for write string list to .txt file format
        /// </summary>
        /// <param name="lines">List of lines</param>
        /// <param name="fileOutPath">File out path</param>
        internal void WriteTXT(List<string> lines, string fileOutPath) 
        {
            string newFilePath = fileOutPath;
            if (File.Exists(fileOutPath)) 
            {
                newFilePath = SetNewFilePath(fileOutPath);
            }

            File.WriteAllLines(newFilePath, lines);

            Console.WriteLine($"Новый файл сохранен в: {newFilePath}");
        }

        #endregion

        //Helpers funcs
        #region Helpers funcs

        /// <summary>
        /// Function for getting string list of codes ranges 
        /// </summary>
        /// <param name="codes">int list of codes</param>
        /// <returns></returns>
        private List<string> GetRanges(List<int> codes)
        {
            List<string> rangesList = new List<string>();
            int startRange = codes[0];
            int endRange = codes[0];

            for (int i = 1; i < codes.Count; i++)
            {
                if (codes[i] == endRange + 1)
                {
                    endRange++;
                }
                else
                {
                    rangesList.Add(startRange == endRange ? startRange.ToString() : $" {startRange}-{endRange}");
                    startRange = codes[i];
                    endRange = codes[i];
                }
            }

            //Add last range
            rangesList.Add(startRange == endRange ? startRange.ToString() : $" {startRange}-{endRange}");

            return rangesList;
        }

        /// <summary>
        /// Function for creating new file path if current file path is exists
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string new file path</returns>
        private string SetNewFilePath(string filePath)
        {
            string newFilePathWithoutTXT = filePath.Replace(".txt", "");
            string newFilePath = $"{newFilePathWithoutTXT}(1).txt";

            int i = 1;

            while (File.Exists(newFilePath) == true)
            {
                newFilePath = $"{newFilePathWithoutTXT}({i}).txt";
                i++;
            }
            
            return newFilePath;
        }

        #endregion
    }
}
