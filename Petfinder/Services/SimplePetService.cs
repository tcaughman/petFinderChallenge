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
using System.Threading.Tasks;

namespace Petfinder
{

    /**
     * A mock service for the PetService.
     */
    public class SimplePetService : IPetService
    {

        /**
         * A simple collection of pets.
         */
        private static readonly List<Pet> Pets = new List<Pet>();

        public SimplePetService()
        {
        }

        /**
         * Show all the pets available.
         */
        public async Task<IEnumerable<Pet>> DisplayPets()
        {
            return await Task.FromResult(Pets);
        }

        /**
         * Returns the details about a specific pet.
         */
        public Pet GetPetInfo(int id)
        {

            var pet = Pets.Single(p => p.Identifier == id);

            if (pet == null)
            {
                throw GetNullPetException(id);
            }

            return pet;

        }

        /**
         * A pop-operation to remove a Pet from the collection and present it to the new owners.
         */
        public Pet GivePetAway(int id)
        {
            var pet = Pets.Single(p => p.Identifier == id);

            // Ensure we have the pet
            if (pet == null)
            {
                throw GetNullPetException(id);
            }

            return GivePetAway(pet);

        }

        /**
         * A pop-operation to remove a Pet from the collection and present it to the new owners.
         */
        public Pet GivePetAway(Pet value)
        {

            // Ensure we have the pet
            if (!Pets.Contains(value))
            {
                throw GetNullPetException(value.Identifier);
            }

            Pets.Remove(value);

            return value;

        }

        /**
         * Accept a new rescue into the program.
         */
        public void TakeInNewPet(Pet value)
        {
            // Ensure that the identifier isn't already taken.
            if (Pets.Contains(value))
            {
                throw new PetServiceException($"Cannot add a pet with the same identifier '{value.Identifier}'");
            }

            Pets.Add(value);

        }

        /**
         * Allows for the updating of pet info.
         */
        public void UpdatePetInfo(int id, Pet value)
        {

            // Ensure the identifiers match
            if (value.Identifier == id)
                throw new PetServiceException("The provided identifier and the pet's identifier do not match");

            // Ensure the identifier exists
            if (!Pets.Contains(value))
            {
                throw GetNullPetException(value.Identifier);
            }

            Pets.Remove(value);
            Pets.Add(value);

        }

        /**
         * A simple helper for situations where a pet could not be
         * found by the provided identifier.
         */
        private static PetServiceException GetNullPetException(int id)
        {
            return new PetServiceException($"Could not find a pet with the identifier '{id}'");
        }

    }

}
