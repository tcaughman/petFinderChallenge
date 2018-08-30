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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petfinder.Services;



namespace Petfinder
{

    [Route("api/pets")]
    public class PetController : Controller
    {

        private static List<Pet> _pets = new List<Pet>();

        private readonly IPetService _service;

        public PetController(PetServiceFactory factory)
        {
            this._service = factory.GetService();
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Pet>> Get()
        {
            return await _service.DisplayPets();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Pet Get(int id)
        {

            if (!isMockServiceAvailable())
            {
                this.HttpContext.Response.StatusCode = 405;
                return null;
            }

            var service = (SimplePetService) this._service;

            return service.GetPetInfo(id);

        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Pet pet)
        {

            if (!isMockServiceAvailable())
            {
                this.HttpContext.Response.StatusCode = 405;
                return;
            }

            var service = (SimplePetService) this._service;

            service.TakeInNewPet(pet);
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Pet value)
        {

            if (!isMockServiceAvailable())
            {
                this.HttpContext.Response.StatusCode = 405;
                return;
            }


            var service = (SimplePetService) this._service;

            service.UpdatePetInfo(id, value);
            
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete([FromBody]Pet value)
        {
            if (!isMockServiceAvailable())
            {
                this.HttpContext.Response.StatusCode = 405;
                return;
            }

            var service = (SimplePetService) this._service;

            service.GivePetAway(value);

        }

        private bool isMockServiceAvailable()
        {
            return this._service is SimplePetService;
        }

    }

}
