using WebApp.Models.ModelClasses;

namespace WebApp.Models.Interfaces;

public interface IAccessible
{
    #region Melder
    Task<Melder> GetMelderByIdAsync(int id);
    Task<Melder> GetMelderByBenutzernameAsync(string benutzername);
    Task<Melder> GetMelderByEmailAsync(string email);
    Task<List<Melder>> GetAllMelderAsync();
    Task<int> AddMelderAsync(Melder melder);
    Task<bool> UpdateMelderAsync(Melder melder);
    Task<bool> DeleteMelderAsync(int id);
    #endregion

    #region Kategorie
    Task<Kategorie> GetKategorieByIdAsync(int id);
    Task<Kategorie> GetKategorieByBezeichnungAsync(string bezeichnung);
    Task<List<Kategorie>> GetAllKategorieAsync();
    Task<int> AddKategorieAsync(Kategorie kategorie);
    Task<bool> UpdateKategorieAsync(Kategorie kategorie);
    Task<bool> DeleteKategorieAsync(int id);
    #endregion

    #region Sichtung
    Task<Sichtung> GetSichtungByIdAsync(int id);
    Task<List<Sichtung>> GetAllSichtungAsync();
    Task<List<Sichtung>> GetSichtungByKategorieIdAsync(int kategorieId);
    Task<List<Sichtung>> GetSichtungByMelderIdAsync(int melderId);
    Task<int> AddSichtungAsync(Sichtung sichtung);
    Task<bool> UpdateSichtungAsync(Sichtung sichtung);
    Task<bool> DeleteSichtungAsync(int id);
    #endregion

    #region Bild
    Task<Bild> GetBildByIdAsync(int id);
    Task<List<Bild>> GetBildBySichtungIdAsync(int sichtungId);
    Task<List<Bild>> GetAllBildAsync();
    Task<int> AddBildAsync(Bild bild);
    Task<bool> UpdateBildAsync(Bild bild);
    Task<bool> DeleteBildAsync(int id);
    #endregion
}