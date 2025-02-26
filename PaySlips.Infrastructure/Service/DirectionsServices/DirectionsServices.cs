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
        // Возвращаем два направления – для БПИ и БИСТ
        var directions = new List<Direction>
            {
                new Direction { Name = "Прикладная информатика", AbbreviatedName = "БПИ", Code = "09.03.03" },
                new Direction { Name = "Информационные системы и технологии", AbbreviatedName = "БИСТ", Code = "09.03.02" }
            };
        return directions;
    }


    public async Task<List<Group>> GetAllGroups()
    {
        byte[] teacherDoc = await _documentService.GetTeacherOverloadings();
        byte[] scheduleDoc = await _documentService.GetDepartmentsOverloadings();
        // Объединяем данные из почасовки и расписания
        var groups = DirectionsParser.MergeGroups(teacherDoc, scheduleDoc);
        return groups;
    }


    public Task<List<Group>> GetGroupsByDirection()
    {
        // Пример: можно вызвать GetAllGroups() и затем сгруппировать по направлению
        throw new NotImplementedException();
    }
}
