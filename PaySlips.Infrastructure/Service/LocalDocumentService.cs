using System;
using System.IO;
using System.Threading.Tasks;
using PaySlips.Core.Abstraction.Services;

namespace PaySlips.Infrastructure.Service
{
    public class LocalDocumentService : IDocumentService
    {
        private static readonly string _basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs"));
        private static readonly string _nameTeacherOverloadingsFile = "teacherOverloadings21.xls.xlsx";
        private static readonly string _nameDepartmentsOverloadingsFile = "Расписание_кафедры_1_семестр_2023_2024_от_01_09_2024_2.xls";

        public async Task<byte[]> GetTeacherOverloadings()
        {
            string path = GetFullPath(_nameTeacherOverloadingsFile);
            return await File.ReadAllBytesAsync(path);
        }

        public async Task<byte[]> GetDepartmentsOverloadings()
        {
            string path = GetFullPath(_nameDepartmentsOverloadingsFile);
            return await File.ReadAllBytesAsync(path);
        }

        private string GetFullPath(string fileName)
        {
            string result = Path.Combine(_basePath, fileName);
            if (!File.Exists(result))
                throw new FileNotFoundException($"File by path: {result} not found");
            return result;
        }
    }
}
