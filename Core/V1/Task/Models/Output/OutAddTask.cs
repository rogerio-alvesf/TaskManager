namespace TaskManager.Core.V1.Models.Output
{
    public class OutAddTask
    {
        public int ID_Task { get; set; }
        public DateTime DT_Created { get; set; }
        public string NM_User_Inclusion { get; set; }
        public string Description { get; set; }
    }
}