namespace TaskManager.Core.Models.Input;
public class InRegisterUser
{
    public string NM_User { get; set; }
    public string Email_User { get; set; }
    public DateTime DT_Birth { get; set; }
    public string User_Gender { get; set; }
    public string Password_User { get; set;}
}