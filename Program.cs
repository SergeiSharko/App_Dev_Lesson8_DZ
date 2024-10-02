namespace Lesson8_DZ
{
    //ДЗ: Объедините две предыдущих работы(практические работы 2 и 3): поиск файла и поиск текста в файле написав утилиту которая ищет файлы определенного расширения с указанным текстом.Рекурсивно.
    //Пример вызова утилиты: utility.exe txt текст.


    internal class Program
    {
        const string filePath = "D:\\Death\\_myStudy\\GeekBrains\\Application development in C#";
        //const string searchExtension = ".txt";
        //const string searchWord = "text";

        static void Main(string[] args)
        {            
            if (args.Length == 2)
            {
                var extList = SearchFiles(filePath, args[0]);
                Console.WriteLine($"--- Список файлов с искомым {args[0]} расширением ---");
                Console.WriteLine(string.Join("\n", extList));
                Console.WriteLine("--------------------------------------------------");
                FindWordInFile(extList, args[1]);
            }
            else
            {
                Console.WriteLine("Запуск программы с параметрами: <Расширение файлов, в которых искать> <Искомых текст>");
            }

        }

        public static void FindWordInFile(List<string> filePath, string searchWord)
        {
            try
            {
                foreach (string fp in filePath)
                {
                    using (StreamReader sr = new StreamReader(fp))
                    {
                        bool isNextFile = false;
                        string line;
                        long countLine = 0;

                        while ((line = sr.ReadLine()!) != null)
                        {
                            countLine++;
                            if (line.Contains(searchWord))
                            {
                                string lineRes = line.Replace(searchWord.ToLower(), searchWord.ToUpper());
                                Console.WriteLine($"Номер строки, содержащей искомое {searchWord} => {countLine} => {lineRes}");
                                isNextFile = true;
                            }
                        }
                        if (isNextFile)
                        {
                            Console.WriteLine($"Путь искомого файла: {fp}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static List<string> SearchFiles(string path, string nameFile)
        {
            var listResult = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories();
            var files = dirInfo.GetFiles();

            foreach (var file in files)
            {
                if (file.Extension.Contains(nameFile.ToLower()))
                {
                    listResult.Add((file.FullName));
                }
            }

            foreach (var dir in dirs)
            {
                listResult.AddRange(SearchFiles(dir.FullName, nameFile));
            }
            return listResult;
        }
    }
}
