using System;
using System.Threading.Tasks;
using PaySlips.Core.Abstraction.Services;
using PaySlips.Core.Model.Lesson;
using PaySlips.Infrastructure.Service;

namespace TestConsoleApp
{
    class Program
    {
        // Точка входа – асинхронный Main
        static async Task Main(string[] args)
        {
            try
            {
                // Создаем реализацию IDocumentService, которая читает файлы из папки docs
                IDocumentService documentService = new LocalDocumentService();

                // Создаем сервис для работы с направлениями и группами
                IDirectionsService directionsService = new DirectionsServices(documentService);

                // 1. Проверяем получение направлений
                var directions = await directionsService.GetAllDirections();
                Console.WriteLine("Полученные направления:");
                foreach (var direction in directions)
                {
                    Console.WriteLine($"Name: {direction.Name}, AbbreviatedName: {direction.AbbreviatedName}, Code: {direction.Code}");
                }

                // 2. Проверяем получение всех групп
                var groups = await directionsService.GetAllGroups();
                Console.WriteLine("\nПолученные группы:");
                foreach (var group in groups)
                {
                    Console.WriteLine($"Group: {group.Name}, Students: {group.CountStudents}, LearningType: {group.LearningType}");
                }

                // 3. Демонстрация группировки по направлению
                Console.WriteLine("\nГруппы, сгруппированные по направлению:");
                foreach (var direction in directions)
                {
                    var groupsByDir = groups.FindAll(g => g.Direction.AbbreviatedName == direction.AbbreviatedName);
                    Console.WriteLine($"\nНаправление: {direction.AbbreviatedName}");
                    foreach (var group in groupsByDir)
                    {
                        Console.WriteLine($"\tGroup: {group.Name}, " +
                            $"Students: {group.CountStudents}, LearningType: {group.LearningType}, " +
                            $"Code: {group.Code}, AbbreviatedName: {group.Direction.AbbreviatedName}, Code: {group.Direction.Code}, Name: {group.Direction.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
