using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Interfaces;
using WebApp.Models.ModelClasses;

namespace WebApp.RestDal;

public class RestDAL : IAccessible
{
    private readonly string melderUrl = "https://localhost:44343/api/Melder";
    private readonly string kategorieUrl = "https://localhost:44343/api/Kategorie";
    private readonly string sichtungUrl = "https://localhost:44343/api/Sichtung";
    private readonly string bildUrl = "https://localhost:44343/api/Bild";
    private readonly HttpClient client = new HttpClient();


    [HttpGet]
    public async Task<Melder> GetMelderByIdAsync(int id)
    {
        try
        {
            Melder melder = await client.GetFromJsonAsync<Melder>($"{melderUrl}/?id={id}");
            return melder;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public Task<Melder> GetMelderByBenutzernameAsync(string benutzername)
    {
        try
        {
            Melder melder = client.GetFromJsonAsync<Melder>($"{melderUrl}/Benutzername?benutzername={benutzername}").Result;
            return Task.FromResult(melder);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public Task<Melder> GetMelderByEmailAsync(string email)
    {
        try
        {
            Melder melder = client.GetFromJsonAsync<Melder>($"{melderUrl}/Email?email={email}").Result;
            return Task.FromResult(melder);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<List<Melder>> GetAllMelderAsync()
    {
        try
        {
            return await client.GetFromJsonAsync<List<Melder>>(melderUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<int> AddMelderAsync(Melder melder)
    {
        try
        {
            var response = await client.PostAsJsonAsync(melderUrl, melder);
            if (response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Fehler: {response.StatusCode}, Nachricht: {errorContent}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Fehler beim Hinzufügen des Melders");
        }
    }

    [HttpPut]
    public async Task<bool> UpdateMelderAsync(Melder melder)
    {
        try
        {
            var response = await client.PutAsJsonAsync(melderUrl, melder);
            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Fehler: {response.StatusCode}, Nachricht: {errorContent}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    public Task<bool> DeleteMelderAsync(int id)
    {
        try
        {
               var response = client.DeleteAsync($"{melderUrl}/?id={id}").Result;
               if (response.IsSuccessStatusCode)
               {
                     return Task.FromResult(true);
                }
                else
                {
                     var errorContent = response.Content.ReadAsStringAsync().Result;
                     throw new Exception($"Fehler: {response.StatusCode}, Nachricht: {errorContent}");
                
               }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }    
    }

    public Task<Kategorie> GetKategorieByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Kategorie> GetKategorieByBezeichnungAsync(string bezeichnung)
    {
        throw new NotImplementedException();
    }

    public Task<List<Kategorie>> GetAllKategorieAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddKategorieAsync(Kategorie kategorie)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateKategorieAsync(Kategorie kategorie)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteKategorieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Sichtung> GetSichtungByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Sichtung>> GetAllSichtungAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Sichtung>> GetSichtungByKategorieIdAsync(int kategorieId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Sichtung>> GetSichtungByMelderIdAsync(int melderId)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddSichtungAsync(Sichtung sichtung)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateSichtungAsync(Sichtung sichtung)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSichtungAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Bild> GetBildByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Bild>> GetBildBySichtungIdAsync(int sichtungId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Bild>> GetAllBildAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddBildAsync(Bild bild)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateBildAsync(Bild bild)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBildAsync(int id)
    {
        throw new NotImplementedException();
    }
}