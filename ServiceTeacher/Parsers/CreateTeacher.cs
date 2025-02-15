using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PaySlips.Core.Model.Parents;

namespace ServiceTeacher.Parsers
{
    internal class CreateTeacher
    {
        private readonly string _pathFile;
        private readonly int ROW_FULL_NAME_TEACHER = 4;
        private readonly int CELL_FULL_NAME_TEACHER = 1;

        public CreateTeacher(string pathFile)
        {
            _pathFile = pathFile ?? throw new ArgumentNullException(nameof(pathFile));
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
                if (_pathFile.EndsWith(".xlsx"))
                    workbook = new XSSFWorkbook(fs); // Для .xlsx
                else
                    workbook = new HSSFWorkbook(fs); // Для .xls
            }

            return workbook;
        }

        private IEnumerable<Teacher> ParserTeacher(IWorkbook workbook)
        {
            List<Teacher> teachers = new List<Teacher>();
            for (int sheetIndex = 1; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
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
