using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Interfaces;
using WebApp.Models.ModelClasses;

namespace WebApp.RestDal;

public class RestDAL : IAccessible
{
    private readonly string melderUrl = "https://localhost:5277/api/Melder";
    private readonly string kategorieUrl = "https://localhost:5277/api/Kategorie";
    private readonly string sichtungUrl = "https://localhost:5277/api/Sichtung";
    private readonly string bildUrl = "https://localhost:5277/api/Bild";
    private readonly HttpClient _client  = new HttpClient();


        
    
    [HttpGet]
    public async Task<Melder> GetMelderByIdAsync(int id)
    {
        try
        {
            Melder melder = await _client.GetFromJsonAsync<Melder>($"{melderUrl}/?id={id}");
            return melder;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public  async Task<Melder> GetMelderByBenutzernameAsync(string benutzername)
    {
        try
        {
            Melder melder = await _client.GetFromJsonAsync<Melder>($"{melderUrl}/Benutzername?benutzername={benutzername}");
            return melder;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<Melder> GetMelderByEmailAsync(string email)
    {
        try
        {
            Melder? melder = await _client.GetFromJsonAsync<Melder>($"https://localhost:5277/api/Melder/Email?email={email}");
            return melder;
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
            return await _client.GetFromJsonAsync<List<Melder>>(melderUrl);
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
            var response = await _client.PostAsJsonAsync(melderUrl, melder);
            if (response.IsSuccessStatusCode)
            {
               var mId = await response.Content.ReadAsStringAsync();
                return int.Parse(mId);
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
            var response = await _client.PutAsJsonAsync(melderUrl, melder);
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
               var response = _client.DeleteAsync($"{melderUrl}/?id={id}").Result;
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

    public async Task<bool> IsEmailInUseAsync(string email)
    {
        try
        {
          var response = await _client.GetAsync($"{melderUrl}/Email?email={email}");
           if(response.IsSuccessStatusCode)
           {
               return true;
           }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return false;
    }

    public Task<bool> IsBenutzernameInUseAsync(string benutzername)
    {
        try
        {
             var response = _client.GetAsync($"{melderUrl}/Benutzername?benutzername={benutzername}").Result;
                if (response.IsSuccessStatusCode)
                {
                 return Task.FromResult(true);
                }
                else
                {
                 return Task.FromResult(false);
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
        try
        {
            Kategorie kategorie = _client.GetFromJsonAsync<Kategorie>($"{kategorieUrl}/?id={id}").Result;
            return Task.FromResult(kategorie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Kategorie> GetKategorieByBezeichnungAsync(string bezeichnung)
    {
        try
        {
            Kategorie kategorie = _client.GetFromJsonAsync<Kategorie>($"{kategorieUrl}/Bezeichnung?bezeichnung={bezeichnung}").Result;
            return Task.FromResult(kategorie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Kategorie>> GetAllKategorieAsync()
    {
        try
        {
            return _client.GetFromJsonAsync<List<Kategorie>>(kategorieUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<int> AddKategorieAsync(Kategorie kategorie)
    {
        try
        {
            var response = _client.PostAsJsonAsync(kategorieUrl, kategorie).Result;
            if (response.IsSuccessStatusCode)
            {
                var kId = response.Content.ReadAsStringAsync().Result;
                return Task.FromResult(int.Parse(kId));
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

    public Task<bool> UpdateKategorieAsync(Kategorie kategorie)
    {
        try
        {
            var response = _client.PutAsJsonAsync(kategorieUrl, kategorie).Result;
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

    public Task<bool> DeleteKategorieAsync(int id)
    {
        try
        {
            var response = _client.DeleteAsync($"{kategorieUrl}/?id={id}").Result;
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

    public Task<Sichtung> GetSichtungByIdAsync(int id)
    {
        try
        {
            Sichtung sichtung = _client.GetFromJsonAsync<Sichtung>($"{sichtungUrl}/?id={id}").Result;
            return Task.FromResult(sichtung);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Sichtung>> GetAllSichtungAsync()
    {
           try
            {
                return _client.GetFromJsonAsync<List<Sichtung>>(sichtungUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
    }

    public Task<List<Sichtung>> GetSichtungByKategorieIdAsync(int kategorieId)
    {
        try
        {
            List<Sichtung> sichtung = _client.GetFromJsonAsync<List<Sichtung>>($"{sichtungUrl}/KategorieId?kategorieId={kategorieId}").Result;
            return Task.FromResult(sichtung);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Sichtung>> GetSichtungByMelderIdAsync(int melderId)
    {
        try
        {
            List<Sichtung> sichtung = _client.GetFromJsonAsync<List<Sichtung>>($"{sichtungUrl}/MelderId?melderId={melderId}").Result;
            return Task.FromResult(sichtung);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<int> AddSichtungAsync(Sichtung sichtung)
    {
        try
        {
            var response = _client.PostAsJsonAsync(sichtungUrl, sichtung).Result;
            if (response.IsSuccessStatusCode)
            {
               var sId = response.Content.ReadAsStringAsync().Result;
                return Task.FromResult(int.Parse(sId));
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

    public Task<bool> UpdateSichtungAsync(Sichtung sichtung)
    {
        try
        {
            var response = _client.PutAsJsonAsync(sichtungUrl, sichtung).Result;
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

    public Task<bool> DeleteSichtungAsync(int id)
    {
        try
        {
            var response = _client.DeleteAsync($"{sichtungUrl}/?id={id}").Result;
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

    public Task<Bild> GetBildByIdAsync(int id)
    {
        try
        {
            Bild bild = _client.GetFromJsonAsync<Bild>($"{bildUrl}/?id={id}").Result;
            return Task.FromResult(bild);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Bild>> GetBildBySichtungIdAsync(int sichtungId)
    {
        try
        {
            List<Bild> bild = _client.GetFromJsonAsync<List<Bild>>($"{bildUrl}/SichtungId?sichtungId={sichtungId}").Result;
            return Task.FromResult(bild);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Bild>> GetAllBildAsync()
    {
        try
        {
            return _client.GetFromJsonAsync<List<Bild>>(bildUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<int> AddBildAsync(Bild bild)
    {
        try
        {
            var response = _client.PostAsJsonAsync(bildUrl, bild).Result;
            if (response.IsSuccessStatusCode)
            {
                var bId = response.Content.ReadAsStringAsync().Result;
                return Task.FromResult(int.Parse(bId));
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

    public Task<bool> UpdateBildAsync(Bild bild)
    {
        try
        {
            var response = _client.PutAsJsonAsync(bildUrl, bild).Result;
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

    public Task<bool> DeleteBildAsync(int id)
    {
        try
        {
            var response = _client.DeleteAsync($"{bildUrl}/?id={id}").Result;
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
}