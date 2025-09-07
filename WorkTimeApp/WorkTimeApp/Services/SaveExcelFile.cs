using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.Abstractions;
using WorkTimeApp.Shared.Model;

namespace WorkTimeApp.Services
{
    internal class SaveExcelFile : IExportToExcel
    {
        public async Task ExportToExcel(List<WorkTime.Abstractions.TaskModel> AllTasks, List<WorkTime.Abstractions.TaskTimerModel> AllTimers, DateTime exportFrom, DateTime exportTo)
        {
            var filtered = AllTimers
                .Where(t => t.StartTime.Date >= exportFrom.Date && t.StartTime.Date <= exportTo.Date)
                .OrderBy(t => t.StartTime)
                .Select(timer => new
                {
                    Timer = timer,
                    Task = AllTasks.FirstOrDefault(task => task.id == timer.TaskID)
                })
                .ToList();

            using var ms = new MemoryStream();
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var ws = workbook.AddWorksheet("Timery");

            // Nagłówki
            ws.Cell(1, 1).Value = "Lp.";
            ws.Cell(1, 2).Value = "Tytuł zadania";
            ws.Cell(1, 3).Value = "Status";
            ws.Cell(1, 4).Value = "Start";
            ws.Cell(1, 5).Value = "Koniec";
            ws.Cell(1, 6).Value = "Czas trwania";

            // Wiersze
            for (int i = 0; i < filtered.Count; i++)
            {
                var row = filtered[i];
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = row.Task?.title ?? "Brak";
                ws.Cell(i + 2, 3).Value = row.Task?.status.ToString() ?? "Brak";
                ws.Cell(i + 2, 4).Value = row.Timer.StartTime.ToString("yyyy-MM-dd HH:mm");
                ws.Cell(i + 2, 5).Value = row.Timer.EndTime.ToString("yyyy-MM-dd HH:mm");
                ws.Cell(i + 2, 6).Value = (row.Timer.EndTime - row.Timer.StartTime).ToString(@"hh\:mm");
            }

            ws.Columns().AdjustToContents();
            workbook.SaveAs(ms);
            ms.Seek(0, SeekOrigin.Begin);

            var fileSaver = CommunityToolkit.Maui.Storage.FileSaver.Default;
            var fileName = $"TaskTimers_{exportFrom:yyyyMMdd}_{exportTo:yyyyMMdd}.xlsx";

            if (fileSaver != null)
            {
                var result = await fileSaver.SaveAsync(fileName, ms);
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (mainPage != null)
                {
                    if (result.IsSuccessful)
                    {
                        await mainPage.DisplayAlert("Sukces", $"Plik zapisano: {result.FilePath}", "OK");
                    }
                    else
                    {
                        await mainPage.DisplayAlert("Błąd", result.Exception?.Message ?? "Nie udało się zapisać pliku", "OK");
                    }
                }

            }
            else
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("Błąd", "FileSaver niedostępny", "OK");
                }
            }
        }
    }
}
