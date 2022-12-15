using Net7.WebApi.Test.Models;
using Net7.WebApi.Test.ResponseModels;
using Newtonsoft.Json;
using System.Text;

namespace ClientForRest
{
    public class Program
    {
        private const string apiUrl = "https://localhost:44303/api/teapots";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Recieving all teapots from db");
            var teapots = await GetAllTeapotsAsync();

            if (teapots != null)
            {
                foreach (var t in teapots)
                {
                    Console.WriteLine($"Id: {t.Id}\nTitle: {t.Title}\nQuantity: {t.Quantity}\nPrice: {t.Price}UAH\nManufacturer: {t.ManufacturerCountry}\n");
                }
            }

            Console.WriteLine("Recieving teapot by id = 1");
            var teapot = await GetTeapotByIdAsync(1);

            if (teapot != null)
                Console.WriteLine($"Id: {teapot.Id}\nTitle: {teapot.Title}\nQuantity: {teapot.Quantity}\nPrice: {teapot.Price}UAH\nManufacturer: {teapot.ManufacturerCountry}\n");

            Console.WriteLine("Adding new teapot");
            var teapotToAdd = new Teapot
            {
                Title = "Teapot 3",
                Description = "Some good teapot",
                ImgUrl = "image3.png",
                ManufacturerCountry = "China",
                Price = 1899,
                Quantity = 10,
                WarrantyInMonths = 12,
                Capacity = 1.9
            };

            await AddNewTeapotAsync(teapotToAdd);

            Console.WriteLine("The result after adding: ");
            teapots = await GetAllTeapotsAsync();

            if (teapots != null)
            {
                foreach (var t in teapots)
                {
                    Console.WriteLine($"Id: {t.Id}\nTitle: {t.Title}\nQuantity: {t.Quantity}\nPrice: {t.Price}UAH\nManufacturer: {t.ManufacturerCountry}\n");
                }
            }

            Console.WriteLine("Updating last teapot");
            var recievedTeapot = await GetTeapotByIdAsync(3);
            
            if (recievedTeapot != null)
            {
                var teapotToUpdate = new Teapot
                {
                    Title = recievedTeapot.Title,
                    Capacity = recievedTeapot.Capacity,
                    Description = recievedTeapot.Description,
                    ImgUrl = recievedTeapot.ImgUrl,
                    ManufacturerCountry = recievedTeapot.ManufacturerCountry,
                    Price = recievedTeapot.Price,
                    Quantity = recievedTeapot.Quantity,
                    WarrantyInMonths = recievedTeapot.WarrantyInMonths
                };

                teapotToUpdate.Title = "Teapot Model C30";
                await UpdateTeapotByIdAsync(3, teapotToUpdate);
            }

            Console.WriteLine("The result after updating: ");
            teapots = await GetAllTeapotsAsync();

            if (teapots != null)
            {
                foreach (var t in teapots)
                {
                    Console.WriteLine($"Id: {t.Id}\nTitle: {t.Title}\nQuantity: {t.Quantity}\nPrice: {t.Price}UAH\nManufacturer: {t.ManufacturerCountry}\n");
                }
            }

            Console.WriteLine("Removing last teapot");
            await DeleteTeapotByIdAsync(3);

            Console.WriteLine("The result after removing: ");
            teapots = await GetAllTeapotsAsync();

            if (teapots != null)
            {
                foreach (var t in teapots)
                {
                    Console.WriteLine($"Id: {t.Id}\nTitle: {t.Title}\nQuantity: {t.Quantity}\nPrice: {t.Price}UAH\nManufacturer: {t.ManufacturerCountry}\n");
                }
            }
        }

        static async Task<List<TeapotResponse>?> GetAllTeapotsAsync()
        {
            List<TeapotResponse>? teapots = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var teapotListToDeserialize = await response.Content.ReadAsStringAsync();
                    teapots = JsonConvert.DeserializeObject<List<TeapotResponse>?>(teapotListToDeserialize);
                }
            }

            return teapots;
        }

        static async Task<TeapotResponse?> GetTeapotByIdAsync(int id)
        {
            TeapotResponse? teapot = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{apiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var teapotToDeserialize = await response.Content.ReadAsStringAsync();
                    teapot = JsonConvert.DeserializeObject<TeapotResponse?>(teapotToDeserialize);
                }
            }

            return teapot;
        }

        static async Task AddNewTeapotAsync(Teapot newTeapot)
        {

            using (var httpClient = new HttpClient())
            {
                var serializedTeapot = JsonConvert.SerializeObject(newTeapot);
                var content = new StringContent(serializedTeapot, Encoding.Unicode, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Teapot has been added!");
                }
            }

        }

        static async Task UpdateTeapotByIdAsync(int id, Teapot changedTeapot)
        {
            using (var httpClient = new HttpClient())
            {
                var serializedTeapot = JsonConvert.SerializeObject(changedTeapot);
                var content = new StringContent(serializedTeapot, Encoding.Unicode, "application/json");
                var response = await httpClient.PutAsync($"{apiUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Teapot has been updated!");
                }
            }

        }

        static async Task DeleteTeapotByIdAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{apiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Teapot has been deleted!");
                }
            }
        }


    }
}