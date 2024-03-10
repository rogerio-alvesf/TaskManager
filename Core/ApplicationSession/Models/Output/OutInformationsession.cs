namespace TaskManager.Core.Models;

public class OutInformationsession
{
    public int ID_User { get; set; }
    public string Email_User { get; set; }
    public OutInformationsession(int id_user, string email_user)
    {
        ID_User = id_user;
        Email_User = email_user;
    }
}