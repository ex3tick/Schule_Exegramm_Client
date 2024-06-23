namespace WebApp.Models.ModelClasses;

public class DTOLoginMelder
{
    public string Login { get; set; }
    public string Kennwort { get; set; }
    private Melder melder { get; set; } = new Melder();
}