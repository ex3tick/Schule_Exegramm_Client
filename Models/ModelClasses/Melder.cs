using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ModelClasses;

public class Melder
{
    public int MId { get; set; }
    [Display (Name = "Full Name")]
    [Required(ErrorMessage = "Name is required")]
    [ MinLength(3, ErrorMessage = "Name muss mindestens 3 Zeichen lang sein")]
    [ MaxLength(50, ErrorMessage = "Name darf maximal 50 Zeichen lang sein")]
    [ DataType(DataType.Text)]
    [ RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name darf nur Buchstaben enthalten")]
    public string Name { get; set; } = "";
    [Display (Name ="Kennwort")]
    [Required(ErrorMessage = "Kennwort is required")]
    [ MinLength(8, ErrorMessage = "Kennwort muss mindestens 8 Zeichen lang sein")]
    [ MaxLength(50, ErrorMessage = "Kennwort darf maximal 50 Zeichen lang sein")]
    [ DataType(DataType.Password)]
    public string KennwortHash { get; set; } = "";
    public string? Salt { get; set; } 
    public bool IsAktiv { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    public DateTime RegDatum { get; set; } = DateTime.Now;
    
    [ Display(Name = "Email")]
    [ Required(ErrorMessage = "Email is required")]
    [ MinLength(5, ErrorMessage = "Email muss mindestens 5 Zeichen lang sein")]
    [ MaxLength(50, ErrorMessage = "Email darf maximal 50 Zeichen lang sein")]
    [ DataType(DataType.EmailAddress)]
    [ RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email ist nicht gültig")]
    public string Email { get; set; } = "";
        
        [ Display(Name = "Benutzername")]
        [ Required(ErrorMessage = "Benutzername is required")]
        [ MinLength(3, ErrorMessage = "Benutzername muss mindestens 3 Zeichen lang sein")]
        [ MaxLength(50, ErrorMessage = "Benutzername darf maximal 50 Zeichen lang sein")]
        [ DataType(DataType.Text)]
        [ RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Benutzername darf nur Buchstaben enthalten")]
    public string Benutzername { get; set; } =  "";
}