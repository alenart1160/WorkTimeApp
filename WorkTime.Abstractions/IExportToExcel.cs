namespace WorkTime.Abstractions
{
    public interface IExportToExcel
    {
       public Task ExportToExcel(List<TaskModel> AllTasks,List<TaskTimerModel> AllTimers, DateTime exportFrom, DateTime exportTo);
    }
}
