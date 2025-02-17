using PaySlips.Core.Abstraction.Services;
using PaySlips.Core.Model.Lesson;
using PaySlips.Infrastructure.Service.DirectionsServices;

public class DirectionsServices : IDirectionsService
{
    private IDocumentService _documentService;

    public DirectionsServices(IDocumentService documentService)
	{
        _documentService = documentService;
    }

    public async Task<List<Direction>> GetAllDirections()
    {
        var docTeacher = await _documentService.GetTeacherOverloadings();
        var docDepartments = await _documentService.GetDepartmentsOverloadings();

        var result = new List<Direction>();

        for (int i = 0; i <= 10; i++)
        {
            var value = DirectionsParser.GetTest(docTeacher, i);
            // тут получаешь данные из парсера
            result.Add(new Direction
            {
                //тут нужно заполнить свойства модели данными из парсера
            });
        }

        throw new NotImplementedException();
    }

    public Task<List<Group>> GetAllGroups()
    {
        //по примеру GetAllDirections()
        throw new NotImplementedException();
    }

    public Task<List<Group>> GetGroupsByDirection()
    {
        //по примеру GetAllDirections()
        throw new NotImplementedException();
    }
}
