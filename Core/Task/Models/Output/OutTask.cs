namespace TaskManager.Core.Models.Output;
public class OutTask
{
    public int ID_Task { get; set; }
    public DateTime DT_Created { get; set; }
    public DateTime? DT_Change { get; set; }
    public string? Description_Task { get; set; }
}