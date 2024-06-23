namespace WebApp.Models.ModelClasses;

public class Sichtung
{
    public int SId { get; set; }
    public int MId { get; set; }
    public string? Titel { get; set; }
   
    public string? Anmerkung { get; set; }
    public DateTime Datum { get; set; } = DateTime.Now;
    public DateTime Eintragsdatum { get; set; } = DateTime.Now;
    public string? Laengengrad { get; set; }
    public string? Breitengrad { get; set; }
    public int KId { get; set; }
}