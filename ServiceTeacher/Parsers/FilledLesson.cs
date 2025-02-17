using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Model.Lesson;
using PaySlips.Core.Model.Parents;
using PaySlips.Core.Model.Teacher;

namespace ServiceTeacher.Parsers
{
    internal class FilledLesson
    {
        private readonly string _pathFile;
        private readonly IEnumerable<Group> _groups;

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
            _groups = request.Groups;
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
                    (Lesson evenSubject, Lesson oddSubject) = ParserSubject(row, startTime, endTime);

                    if (evenSubject == null && oddSubject == null)
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

        private (Lesson evenSubject, Lesson oddSubject) ParserSubject(IRow row, TimeSpan start, TimeSpan end)
        {
            string? evenSubjectStr = row.GetCell(CELL_INDEX_EVEN_SUBJECT)?.ToString()?.Trim();
            Lesson evenSubject = GetLesson(evenSubjectStr, start, end);

            string? oddSubjectStr = row.GetCell(CELL_INDEX_ODD_SUBJECT)?.ToString()?.Trim();
            Lesson oddSubject = GetLesson(oddSubjectStr, start, end);

            return (evenSubject, oddSubject);
        }

        private Lesson GetLesson(string? subjectStr, TimeSpan start, TimeSpan end)
        {
            if (string.IsNullOrEmpty(subjectStr))
            {
                return null;
            }

            (string onliSubjectStr, string groupStr) = SplitStr(subjectStr);

            Group? group = GetGroup(groupStr);
            TimeSpan duration = end - start;
            Discipline discipline = new Discipline() { Name = onliSubjectStr };

            Lesson lesson = new Lesson()
            {
                Group = group,
                Duration = duration,
                Discipline = discipline
            };
            return lesson;
        }

        private (string onliSubjectStr, string groupStr) SplitStr(string subjectStr)
        {
            string[] lines = subjectStr.Split('\n');

            string onliSubjectStr = lines[0];

            string groupStr = lines[1];
            // Если в строке есть "_" (подгруппа), то берём только часть до него
            groupStr = groupStr.Split('_')[0];

            return (onliSubjectStr, groupStr);
        }

        private Group? GetGroup(string groupStr)
        {
            if (string.IsNullOrEmpty(groupStr))
            {
                return _groups.FirstOrDefault(g => g.Name == groupStr);
            }

            throw new ArgumentNullException(groupStr);
        }



        #endregion

    }
}
