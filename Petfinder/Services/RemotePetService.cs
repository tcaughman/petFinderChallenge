/**
 *   The MIT License
 *   
 *   Copyright (c) 2018 Terrance Caughman.
 *   
 *   Permission is hereby granted, free of charge, to any person obtaining a copy
 *   of this software and associated documentation files (the "Software"), to deal
 *   in the Software without restriction, including without limitation the rights
 *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *   copies of the Software, and to permit persons to whom the Software is
 *   furnished to do so, subject to the following conditions:
 *   
 *   The above copyright notice and this permission notice shall be included in
 *   all copies or substantial portions of the Software.
 *   
 *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 *   THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Petfinder.API.Responses;

namespace Petfinder.Services
{
    public class RemotePetService : IPetService
    {

        private readonly HttpClient _client = new HttpClient();

        //Request return formart
        private readonly string _format = "json";
        //PetFinderApi zip code search criterion
        private string _location = "28206";
        //Api key for request 
        private readonly string _key = "dc8c4f1246830a89ed3647e312ee124a";


        public RemotePetService()
        {
            _client.BaseAddress = new Uri("http://api.petfinder.com");
            _client.DefaultRequestHeaders
                .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Pet>> DisplayPets()
        {
            var response = await GetLatestPetRepo(new CancellationToken());

            return await Task.FromResult(ConvertToPet(response.petfinder));

        }

        public async Task<RootObject> GetLatestPetRepo(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var request = $"/pet.find?key={this._key}&format={this._format}&location={this._location}";

            var response = await _client.GetAsync(request, token);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<RootObject>(token);
            }

            return null;
        }

        private IEnumerable<Pet> ConvertToPet(API.Responses.Petfinder response)
        {
            var pets = new List<Pet>();
           
            foreach (var pet in response.pets.pet)
            {
                pets.Add(new Pet()
                {
                    Name = pet.name.name,
                    Breed = FormatPetName(pet.breeds.breed.ToString()),
                    Identifier = Int32.Parse(pet.id.id),
                    Type = pet.animal.item
                });

                foreach (var photo in pet.media.photos.photo)
                {
                    if (photo.size.Equals("x") && string.IsNullOrWhiteSpace(pets[pets.Count - 1].ImageUrl))
                    {
                        pets[pets.Count - 1].ImageUrl = photo.item;
                        break;
                    }

                }
            }

            return pets;

        }

        private string FormatPetName(string petName)
        {
            string[] charsToRemove = { "[", "]", "{", "}", string.Format(@"""$t"""), ":", @"""" };
            var sb = new StringBuilder();
            sb.Append($"{petName}");
            Console.WriteLine(sb.ToString());
            foreach (var str in charsToRemove)
            {
                sb.Replace(str, "");
            }

            sb.Replace("  ", "");

            return sb.ToString();
        }

    }
}
