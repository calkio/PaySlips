using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Model.Parents;
using PaySlips.Core.Model.Teacher;

namespace ServiceTeacher.Parsers
{
    internal class FilledLesson
    {
        private readonly string _pathFile;

        private readonly int START_ROW = 8;
        private readonly int START_SHEET_INDEX = 1;

        private readonly int ROW_FULL_NAME_TEACHER = 4;
        private readonly int CELL_FULL_NAME_TEACHER = 1;

        private readonly int CELL_INDEX_DAY = 0;
        private readonly int CELL_INDEX_TIME = 1;
        private readonly int CELL_INDEX_EVEN_SUBJECT = 2;
        private readonly int CELL_INDEX_ODD_SUBJECT = 3;

        public FilledLesson(FilledTeacherRequest request)
        {
            _pathFile = request.PathFileScheduler ?? throw new ArgumentNullException(nameof(request.PathFileScheduler));
        }

        public async Task<IEnumerable<Teacher>> FilledAllTeacher()
        {
            var task = Task.Run(() =>
            {
                var workbook = CreateFileConnection();

                return ParserLesson(workbook);
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

        private IEnumerable<Teacher> ParserLesson(IWorkbook workbook)
        {
            List<Teacher> teachers = new List<Teacher>();
            string dayStr = "";
            for (int sheetIndex = START_SHEET_INDEX; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                Teacher teacher = ParserTeacher(sheet);
                for (int i = START_ROW; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    DayOfWeek day = ParserDay(ref dayStr, row);

                    (TimeSpan startTime, TimeSpan endTime) = ParserTime(row);
                    if (startTime == TimeSpan.Zero || endTime == TimeSpan.Zero)
                    {
                        continue;
                    }
                    (string? evenSubject, string? oddSubject) = ParserSubject(row);

                    if (string.IsNullOrEmpty(evenSubject) && string.IsNullOrEmpty(oddSubject))
                    {
                        continue;
                    }

                    var slot = new TimeSlot
                    {
                        DayOfWeek = day,
                        StartTime = startTime,
                        EvenWeekSubject = evenSubject,
                        OddWeekSubject = oddSubject
                    };
                    teacher.TimeSlot.Add(slot);
                } 
                teachers.Add(teacher);
            }
            return teachers;
        }

        #region Teacher

        private Teacher ParserTeacher(ISheet sheet)
        {
            string teacherName = sheet.GetRow(ROW_FULL_NAME_TEACHER).GetCell(CELL_FULL_NAME_TEACHER).StringCellValue;
            string[] teacherAllName = teacherName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Teacher teacher = new Teacher()
            {
                Secondname = teacherAllName[0],
                Firstname = teacherAllName[1],
                Surname = teacherAllName[2],
                TimeSlot = new List<TimeSlot>()
            };
            return teacher;
        }

        #endregion

        #region Day

        private DayOfWeek ParserDay(ref string dayStr, IRow row)
        {
            string? currentDay = row.GetCell(CELL_INDEX_DAY)?.ToString()?.Trim();
            if (!string.IsNullOrEmpty(currentDay))
            {
                dayStr = currentDay;
            }

            DayOfWeek day = ConvertDayStringToDayOfWeek(dayStr);
            return day;
        }
        private DayOfWeek ConvertDayStringToDayOfWeek(string dayStr)
        {
            if (string.IsNullOrWhiteSpace(dayStr))
            {
                throw new ArgumentException("Строка не должна быть пустой", nameof(dayStr));
            }

            switch (dayStr.Trim().ToLower())
            {
                case "понедельник":
                    return DayOfWeek.Monday;
                case "вторник":
                    return DayOfWeek.Tuesday;
                case "среда":
                    return DayOfWeek.Wednesday;
                case "четверг":
                    return DayOfWeek.Thursday;
                case "пятница":
                    return DayOfWeek.Friday;
                case "суббота":
                    return DayOfWeek.Saturday;
                case "воскресенье":
                    return DayOfWeek.Sunday;
                default:
                    throw new ArgumentException($"Неизвестный день недели: {dayStr}", nameof(dayStr));
            }
        }

        #endregion

        #region Time

        private (TimeSpan startTime, TimeSpan endTime) ParserTime(IRow row)
        {
            string? timeStr = row.GetCell(CELL_INDEX_TIME)?.ToString()?.Trim() ?? throw new ArgumentNullException(nameof(timeStr));
            TryParseTimeRange(timeStr, out TimeSpan startTime, out TimeSpan endTime);
            return (startTime, endTime);
        }
        private bool TryParseTimeRange(string? timeRangeStr, out TimeSpan startTime, out TimeSpan endTime)
        {
            startTime = default;
            endTime = default;

            if (string.IsNullOrWhiteSpace(timeRangeStr))
            {
                return false;
            }

            var parts = timeRangeStr.Split('-');
            if (parts.Length != 2)
            {
                return false;
            }

            if (!TimeSpan.TryParse(parts[0].Trim(), out startTime))
            {
                return false;
            }

            if (!TimeSpan.TryParse(parts[1].Trim(), out endTime))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Subject

        private (string? evenSubject, string? oddSubject) ParserSubject(IRow row)
        {
            string? evenSubject = row.GetCell(CELL_INDEX_EVEN_SUBJECT)?.ToString()?.Trim();
            string? oddSubject = row.GetCell(CELL_INDEX_ODD_SUBJECT)?.ToString()?.Trim();

            return (evenSubject, oddSubject);
        }

        #endregion

    }
}
