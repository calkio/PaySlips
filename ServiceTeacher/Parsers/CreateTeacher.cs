using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Model.Parents;

namespace ServiceTeacher.Parsers
{
    internal class CreateTeacher
    {
        private readonly string _pathFile;
        private readonly int ROW_FULL_NAME_TEACHER = 4;
        private readonly int CELL_FULL_NAME_TEACHER = 1;
        private readonly int START_SHEET_INDEX = 1;

        public CreateTeacher(NoLessonTeacherRequest request)
        {
            _pathFile = request.PathFileScheduler ?? throw new ArgumentNullException(nameof(request.PathFileScheduler));
        }

        public async Task<IEnumerable<Teacher>> CreateAllTeacher()
        {
            var task = Task.Run(() =>
            {
                var workbook = CreateFileConnection();

                return ParserTeacher(workbook);
            });

            var teachers = await task;
            return teachers;
        }

        private IWorkbook CreateFileConnection()
        {
            IWorkbook workbook;

            using (var fs = new FileStream(_pathFile, FileMode.Open, FileAccess.Read))
            {
                workbook = _pathFile.EndsWith(".xlsx")
                    ? new XSSFWorkbook(fs) // Для .xlsx
                    : new HSSFWorkbook(fs); // Для .xls
            }

            return workbook;
        }

        private IEnumerable<Teacher> ParserTeacher(IWorkbook workbook)
        {
            List<Teacher> teachers = new List<Teacher>();
            for (int sheetIndex = START_SHEET_INDEX; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                string teacherName = sheet.GetRow(ROW_FULL_NAME_TEACHER).GetCell(CELL_FULL_NAME_TEACHER).StringCellValue;
                string[] teacherAllName = teacherName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                teachers.Add(new Teacher()
                {
                    Secondname = teacherAllName[0],
                    Firstname = teacherAllName[1],
                    Surname = teacherAllName[2],
                });
            }
            return teachers;
        }
    }
}
