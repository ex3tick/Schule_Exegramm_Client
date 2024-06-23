namespace WebApp.Models.ModelClasses;

public class Melder
{
    public int MId { get; set; }
    public string Name { get; set; } = "";
    public string KennwortHash { get; set; } = "";
    public string? Salt { get; set; } 
    public bool IsAktiv { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    public DateTime RegDatum { get; set; } = DateTime.Now;
    public string Email { get; set; } = "";
    public string Benutzername { get; set; } =  "";
}