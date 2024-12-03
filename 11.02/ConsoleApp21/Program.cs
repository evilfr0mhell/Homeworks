using System;
using System.IO;

class FileOperations
{
    public static void CopyFile(string sourceFileName, string targetFileName)
    {
        try
        {

            if (!File.Exists(sourceFileName))
            {
                Console.WriteLine($"Файл \"{sourceFileName}\" не найден.");
                return;
            }


            File.Copy(sourceFileName, targetFileName, overwrite: true);

            Console.WriteLine($"Файл \"{sourceFileName}\" успешно скопирован в \"{targetFileName}\".");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
class Program
{
    static void Main()
    {
        string sourceFile = "source.txt";
        string targetFile = "target.txt";


        FileOperations.CopyFile(sourceFile, targetFile);
    }
}
