using PaySlips.Core.Abstraction.Services;

namespace PaySlips.Infrastructure.Service
{
    internal class LocalDocumentService : IDocumentService
    {
        private static readonly string _basePath = $"{Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs"))}";

        private static readonly string _nameTeacherOverloadingsFile = "TeacherOverloadings.xls.xlsx";
        private static readonly string _nameDepartmentsOverloadingsFile = "..."; // добавь нужные документы в папку docs и навпиши сдесь его название

        public async Task<byte[]> GetDepartmentsOverloadings()
        {
            var departmentsOverloadingsFilePath = GetFullPath(_nameDepartmentsOverloadingsFile);
            throw new NotImplementedException();
        }

        public async Task<byte[]> GetTeacherOverloadings()
        {
            var teacherOverloadingsFilePath = GetFullPath(_nameTeacherOverloadingsFile);
            throw new NotImplementedException();
        }

        private string GetFullPath(string nameFile)
        {
            var result = Path.Combine(_basePath, nameFile);
            return Path.Exists(result) ? result : throw new FileNotFoundException($"file by path: {result} not found");
        }
            
    }
}
