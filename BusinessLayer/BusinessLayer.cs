using WebApp.Models.Interfaces;
using WebApp.Models.ModelClasses;
using WebApp.RestDal;

namespace WebApp.BusinessLayer;

public class BusinessLayer
{
    private readonly IAccessible _dal;
    private readonly string Pepper = "AllesFuersPepper89";

    public BusinessLayer()
    {
        _dal = new RestDAL();
    }

    public async Task<bool> RegisterMelder(Melder melder)
    {
        try
        {
            if (await _dal.IsEmailInUseAsync( melder.Email))
            {
                throw new Exception("Email bereits vergeben!");
            }
          
            
            if (await _dal.IsBenutzernameInUseAsync(melder.Benutzername))
            {
                throw new Exception("Benutzername bereits vergeben!");
            }

            melder.Salt = HashHelper.HashHelper.SaltGenerate();
            melder.KennwortHash = HashHelper.HashHelper.HashGenerate(melder.KennwortHash, melder.Salt, Pepper);
            melder.RegDatum = DateTime.Now;

            await _dal.AddMelderAsync(melder); // await hier hinzufügen
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Es ist ein Fehler aufgetreten!", e); // Ursprung des Fehlers behalten
        }
    }

    // login geht ueber email oder benutzername
    public async Task<Melder> LoginMelder(string login, string kennwort)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(kennwort))
        {
            throw new ArgumentException("Bitte geben Sie einen Benutzernamen oder eine Email und ein Kennwort ein!");
        }

        Melder melder;
        try
        {
            if (login.Contains("@"))
            {
                melder = await _dal.GetMelderByEmailAsync(login);
            }
            else
            {
                melder = await _dal.GetMelderByBenutzernameAsync(login);
            }

            if (melder == null || !IsPasswordValid(kennwort, melder))
            {
                throw new UnauthorizedAccessException("Email oder Kennwort falsch!");
            }

            return melder;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Es ist ein Fehler aufgetreten!", ex); // Ursprung des Fehlers behalten
        }
    }

    private bool IsPasswordValid(string kennwort, Melder melder)
    {
        return HashHelper.HashHelper.HashGenerate(kennwort, melder.Salt, Pepper) == melder.KennwortHash;
    }
}
