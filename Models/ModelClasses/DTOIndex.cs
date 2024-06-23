using WebApp.Models.ModelClasses;

public class DTOIndex
{
    public Melder Melder { get; set; } = new Melder();
    public List<SichtungMitBilder> Sichtungen { get; set; } = new List<SichtungMitBilder>();
}

public class SichtungMitBilder
{
    public Sichtung Sichtung { get; set; } = new Sichtung();
    public List<Bild> Bilder { get; set; } = new List<Bild>();
}