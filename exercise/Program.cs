using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

/*
 // 1. Создать консольный файловый менеджер
// У которого будет меню позволяющее
// - Просматривать каталоги, их содержимое
// - Выводить информацию о файлах
// - Копировать каталоги
// - Перемещать каталоги
// - Удалять каталоги
// - Создавать файлы в каталогах
// - Удалять в каталогах
// - Перемещать файлы
// - Копировать файлы

 */
namespace exercise
{
    internal class Program
    {
        public static void GetDrivesInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives) {

                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
        }

        static void GetDirectoryInfo(string dir)
        {
            


            string dirName = @dir;
           
            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");

                
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }

                Console.WriteLine();
                Console.WriteLine("Файлы:");                
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }

            }
        }

        static void CreateCatalog()
        {
            {
                Console.WriteLine("Введите адрес каталога, где хотите создать папку");
                string path = Console.ReadLine();
                Console.WriteLine("Введите адрес для новой папки"); 
                string subpath = Console.ReadLine();
                DirectoryInfo dirInfo = new DirectoryInfo(@path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo.CreateSubdirectory(@subpath);
            }
        }

        static void DeleteDirectory()
        {
            Console.WriteLine("Введите адрес для удаления директории");
            string dName = Console.ReadLine();

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(@dName);
                dirInfo.Delete(true);
                Console.WriteLine("Каталог удален");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void MoveDirectory()
        {
            Console.WriteLine("Укажите действующий адрес каталога, который желаете переместить");
            string oldPath = Console.ReadLine();
            Console.WriteLine("Укажите новый адрес для перемещения каталога");
            string newPath = Console.ReadLine();
            DirectoryInfo dirInfo = new DirectoryInfo(@oldPath);
            if (dirInfo.Exists && Directory.Exists(@newPath) == false)
            {
                dirInfo.MoveTo(@newPath);
            }
        }

        static void ViewFileInfo(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла:                    {0}", fileInf.Name);
                Console.WriteLine("Время создания:               {0}", fileInf.CreationTime);
                Console.WriteLine("Размер:                       {0}", fileInf.Length);
                Console.WriteLine("Имя дирректории:              {0}", fileInf.DirectoryName);
                Console.WriteLine("Только для чтения:            {0}", fileInf.IsReadOnly);
                Console.WriteLine("Время последней записи в файл:{0}", fileInf.LastWriteTime);
            }
        }

        static void DeleteFile(string path)
        {
            FileInfo fileInfDel = new FileInfo(path);
            if (fileInfDel.Exists)
            {
                fileInfDel.Delete();
                Console.WriteLine("Файл удален!");
            }
        }


        static void MoveFile(string path, string newPath)
        {
            FileInfo fileInfMove = new FileInfo(path);
            if (fileInfMove.Exists)
            {
                fileInfMove.MoveTo(newPath);
                Console.WriteLine("Файл перемещен!");                
            }
        }


        public static void CopyFile(string path, string newPath)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(newPath, true);
                Console.WriteLine("Файл скопирован.");
            }
        }



        public static void ProcessDirectory(string targetDirectory)
        {            
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }        
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Файл'{0}'.", path);
        }


        static void Main(string[] args)
        {
           

            int userInput;
            string path;
            string newPath;

            do
            {
                Console.WriteLine("\nВыберите действие:\n1 - узнать информацию о дисках\n" +
                    "2 - Просмотр файлов и каталогов\n" +
                    "3 - Создать новый каталог\n" +
                    "4 - Удалить существующий каталог\n" +
                    "5 - Переместить каталог\n" +
                    "6 - Посмотреть информацию о файле\n" +
                    "7 - Создать файл\n" +
                    "8 - Удалить файл\n" +
                    "9 - Переместить файл\n" +
                    "10 - Копировать файл\n" +
                    "11 - Вывод всех найденых файлов в каталоге\n");
                userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 1:                    
                        Console.WriteLine("Информация о жестких дисках");
                        GetDrivesInfo();
                        break;
                    case 2:
                        Console.WriteLine("Посмотреть папки и файлы в указанной директории");
                        Console.WriteLine("Введите путь директории");
                        string dir = Console.ReadLine();
                        GetDirectoryInfo(dir);
                        break;
                    case 3:
                        Console.WriteLine("Создание новой папки");
                        CreateCatalog();
                        break;
                    case 4:
                        Console.WriteLine("Удаление папки");
                        DeleteDirectory();
                        break;
                    case 5:
                        Console.WriteLine("Перемещение каталога");
                        Console.WriteLine("При перемещении надо учитывать, что новый каталог, " +
                            "в который мы хотим перемесить все содержимое старого каталога, не должен существовать.\n" +
                            "Старый каталог будет переименнован в новый, если укажите разные имена");
                        MoveDirectory();
                        break;
                    case 6:
                        Console.WriteLine("Показать информацию о файле");
                        Console.WriteLine("Введите директорию файла");
                        path = Console.ReadLine();
                        ViewFileInfo(path);
                        break;
                    case 7:
                        Console.WriteLine("Создание файла");
                        Console.WriteLine("Укажите путь для создания файла");
                        path = Console.ReadLine();
                        File.Create(path);
                        Console.WriteLine("Файл создан");
                        break;
                    case 8:
                        Console.WriteLine("Удаление файла");
                        Console.WriteLine("Укажите директорию удаляемого файла");
                        path = Console.ReadLine();
                        DeleteFile(path);
                        break;
                    case 9:
                        Console.WriteLine("Перемещение файла");
                        Console.WriteLine("Укажите путь к файлу");
                        path= Console.ReadLine();
                        Console.WriteLine("Укажите новое расположение файла");
                        newPath = Console.ReadLine();
                        MoveFile(path, newPath);
                        break;
                    case 10:
                        Console.WriteLine("Копировать файл с заменой, если таковой есть");
                        Console.WriteLine("Укажите путь к файлу");
                        path = Console.ReadLine();
                        Console.WriteLine("Укажите путь для копирования файла файла");
                        newPath = Console.ReadLine();
                        CopyFile(path, newPath);
                        break;
                    case 11:
                        Console.WriteLine("Вывод всех файлов найденых в каталоге");
                        Console.WriteLine("Укажите директорию");
                        path = Console.ReadLine();
                        ProcessDirectory(path);
                        break;
                    default:
                        if (userInput == 0)
                        { Console.WriteLine("Завершение работы программы. Всего доброго!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Введена неверная команда!");
                        }
                        break;
                }
            } while (userInput != 0);
        }
    }
}
