namespace PaySlips.Core.Abstraction.Services
{
    public interface IDocumentService
    {
        Task<byte[]> GetTeacherOverloadings();
        Task<byte[]> GetDepartmentsOverloadings();
    }
}
