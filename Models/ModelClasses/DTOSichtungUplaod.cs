namespace WebApp.Models.ModelClasses;

public class DTOSichtungUplaod
{
    
   public  Bild Bild { get; set; } = new Bild();
   public  Sichtung Sichtung { get; set; } = new Sichtung();
   
   public  List<IFormFile>? File { get; set; }
 public   List<Kategorie>? Kategorie { get; set; } = new List<Kategorie>();
}