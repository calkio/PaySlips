using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;   // для файлов .xlsx (почасовка)
using NPOI.HSSF.UserModel;   // для файлов .xls (расписание)
using PaySlips.Core.Model.Lesson;

namespace PaySlips.Infrastructure.Service.DirectionsServices
{
    internal static class DirectionsParser
    {
        // Вспомогательные модели для парсинга

        internal class HourRateGroupInfo
        {
            public string GroupName { get; set; } // "БПИ" или "БИСТ"
            public int Course { get; set; }       // 1..4
            public int CountStudents { get; set; }
            public string LearningType { get; set; } // "FullTime" или "Сorrespondence"
        }

        internal class ScheduleGroupInfo
        {
            public string FullGroupName { get; set; } // например, "БПИ-411"
            public int GroupCode { get; set; }        // например, 411
            public string GroupPrefix
            {
                get
                {
                    if (string.IsNullOrEmpty(FullGroupName))
                        return "";
                    var parts = FullGroupName.Split('-');
                    return parts.Length > 0 ? parts[0] : "";
                }
            }
            // Курс определяется как первая цифра из GroupCode (411 -> курс 4)
            public int Course { get { return GroupCode / 100; } }
        }

        /// <summary>
        /// Парсит данные из файла почасовки (формат .xlsx) и возвращает список HourRateGroupInfo.
        /// Используем колонки:
        /// A (индекс 0) – содержит строки с формой обучения ("Очная форма обучения" или "Заочная форма обучения") и заголовки.
        /// B (индекс 1) – название направления ("БПИ" или "БИСТ").
        /// C (индекс 2) – курс (число).
        /// F (индекс 5) – количество студентов.
        /// </summary>
        internal static List<HourRateGroupInfo> ParseHourRateData(byte[] docTeacher)
        {
            var result = new List<HourRateGroupInfo>();
            string currentLearningType = string.Empty;
            using (var ms = new MemoryStream(docTeacher))
            {
                IWorkbook workbook = new XSSFWorkbook(ms);
                ISheet sheet = workbook.GetSheetAt(0);
                for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null)
                        continue;

                    string cellA = GetCellValue(sheet, row, 0);
                    if (cellA.IndexOf("Очная форма обучения", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        currentLearningType = LearningTypes.FullTime.Name;
                        continue;
                    }
                    else if (cellA.IndexOf("Заочная форма обучения", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        currentLearningType = LearningTypes.Сorrespondence.Name;
                        continue;
                    }
                    // Пропускаем заголовки
                    if (cellA.IndexOf("Бакалавриат", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        cellA.IndexOf("Магистратура", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        cellA.IndexOf("Итого", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        cellA.IndexOf("Всего", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        continue;
                    }
                    // Из колонки B (индекс 1) – название группы ("БПИ"/"БИСТ")
                    string groupType = GetCellValue(sheet, row, 1);
                    if (groupType != "БПИ" && groupType != "БИСТ")
                        continue;
                    // Колонка C (индекс 2) – курс
                    string courseStr = GetCellValue(sheet, row, 2);
                    if (!int.TryParse(courseStr, out int course))
                        continue;
                    // Колонка F (индекс 5) – количество студентов
                    string countStr = GetCellValue(sheet, row, 5);
                    if (!int.TryParse(countStr, out int countStudents))
                        continue;
                    result.Add(new HourRateGroupInfo
                    {
                        GroupName = groupType,
                        Course = course,
                        CountStudents = countStudents,
                        LearningType = currentLearningType
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Парсит данные из файла расписания (формат .xls) и возвращает список ScheduleGroupInfo.
        /// Обрабатываются все листы и строки. Рассматриваются колонки C (индекс 2) и D (индекс 3).
        /// Ищется вхождение шаблона группы вида "БПИ-XXX" или "БИСТ-XXX" (все символы после трех цифр отбрасываются).
        /// </summary>
        internal static List<ScheduleGroupInfo> ParseScheduleData(byte[] docSchedule)
        {
            var result = new List<ScheduleGroupInfo>();
            // Регулярное выражение: ищем "БПИ" или "БИСТ", дефис, три цифры, далее любые непробельные символы
            Regex groupRegex = new Regex(@"(БПИ|БИСТ)-\d{3}\S*");
            using (var ms = new MemoryStream(docSchedule))
            {
                IWorkbook workbook = new HSSFWorkbook(ms);
                int sheetCount = workbook.NumberOfSheets;
                for (int i = 0; i < sheetCount; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    if (sheet == null)
                        continue;
                    for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row == null)
                            continue;
                        string cellC = GetCellValue(sheet, row, 2);
                        string cellD = GetCellValue(sheet, row, 3);
                        FindGroupsInText(cellC, groupRegex, result);
                        FindGroupsInText(cellD, groupRegex, result);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Объединяет данные из почасовки и расписания, создавая итоговый список объектов Group.
        /// Логика: для каждой уникальной записи почасовки (определяемой по группе, курсу, числу студентов и форме обучения)
        /// ищем в расписании все вхождения, где префикс и курс совпадают. Для каждого найденного кода создаём объект Group.
        /// </summary>
        internal static List<PaySlips.Core.Model.Lesson.Group> MergeGroups(byte[] docTeacher, byte[] docSchedule)
        {
            var hrGroups = ParseHourRateData(docTeacher);
            var schGroups = ParseScheduleData(docSchedule);

            // Получаем уникальные записи почасовки
            var uniqueHrGroups = hrGroups
                .GroupBy(x => new { x.GroupName, x.Course, x.CountStudents, x.LearningType })
                .Select(g => g.First())
                .ToList();

            // Формируем словарь расписания: ключ = (GroupName, Course), значение = список числовых кодов (например, 411, 412)
            var scheduleDict = new Dictionary<(string, int), List<int>>();
            foreach (var sg in schGroups)
            {
                var key = (sg.GroupPrefix, sg.Course);
                if (!scheduleDict.ContainsKey(key))
                    scheduleDict[key] = new List<int>();
                scheduleDict[key].Add(sg.GroupCode);
            }

            var result = new List<PaySlips.Core.Model.Lesson.Group>();

            // Определяем Direction для БПИ и БИСТ
            var bpiDirection = new Direction
            {
                Name = "Прикладная информатика",
                AbbreviatedName = "БПИ",
                Code = "09.03.03"
            };
            var bistDirection = new Direction
            {
                Name = "Информационные системы и технологии",
                AbbreviatedName = "БИСТ",
                Code = "09.03.02"
            };

            foreach (var hr in uniqueHrGroups)
            {
                var key = (hr.GroupName, hr.Course);
                if (!scheduleDict.ContainsKey(key))
                    continue; // если для данной группы в расписании нет конкретики, пропускаем

                var codes = scheduleDict[key];
                foreach (var code in codes)
                {
                    var direction = hr.GroupName == "БПИ" ? bpiDirection : bistDirection;
                    var group = new PaySlips.Core.Model.Lesson.Group
                    {
                        Direction = direction,
                        Code = code,
                        CountStudents = hr.CountStudents,
                        LearningType = hr.LearningType
                    };
                    result.Add(group);
                }
            }

            // Убираем дубликаты и сортируем итоговый список по коду и по префиксу (БПИ раньше БИСТ)
            var final = result
                .GroupBy(g => (g.Direction.AbbreviatedName, g.Code, g.CountStudents, g.LearningType))
                .Select(grp => grp.First())
                .OrderBy(g => g.Code)
                .ThenBy(g => (g.Direction.AbbreviatedName == "БПИ" ? 0 : 1))
                .ToList();

            return final;
        }

        // Вспомогательный метод для корректного получения значения ячейки с учетом объединённых ячеек
        private static string GetCellValue(ISheet sheet, IRow row, int colIndex)
        {
            ICell cell = row.GetCell(colIndex);
            string value = cell?.ToString().Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(value))
            {
                for (int i = 0; i < sheet.NumMergedRegions; i++)
                {
                    var merged = sheet.GetMergedRegion(i);
                    if (merged.IsInRange(row.RowNum, colIndex))
                    {
                        IRow firstRow = sheet.GetRow(merged.FirstRow);
                        ICell firstCell = firstRow.GetCell(merged.FirstColumn);
                        value = firstCell?.ToString().Trim() ?? string.Empty;
                        break;
                    }
                }
            }
            return value;
        }

        // Вспомогательный метод для поиска групп в тексте ячейки по регулярному выражению.
        private static void FindGroupsInText(string text, Regex regex, List<ScheduleGroupInfo> list)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
            var matches = regex.Matches(text);
            foreach (Match match in matches)
            {
                string raw = match.Value; // например "БПИ-411_2024" или "БИСТ-211-что-то"
                // Находим базовую часть "БПИ-411" или "БИСТ-211" – берем первые 3 цифры после дефиса.
                var subMatch = Regex.Match(raw, @"^(БПИ|БИСТ)-(\d{3})");
                if (!subMatch.Success)
                    continue;
                string prefix = subMatch.Groups[1].Value;
                string digits = subMatch.Groups[2].Value;
                string fullGroupName = $"{prefix}-{digits}";
                if (!int.TryParse(digits, out int code))
                    code = 0;
                list.Add(new ScheduleGroupInfo
                {
                    FullGroupName = fullGroupName,
                    GroupCode = code
                });
            }
        }
    }
}
