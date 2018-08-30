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

using System.Collections.Generic;
using Petfinder.API.Responses;

namespace Petfinder
{

    /**
     * A representation of the details
     * pertaining to a pet for sale.
     */

    public class Pet
    {

        /**
         * The breed of the pet.
         */
        public string Breed { get; set; }

        /**
         * The identifier of the pet.
         */
        public int Identifier { get; set; }

        /**
         * The URL to the image of the pet.
         */
        public string ImageUrl { get; set; }
        

        /**
         * The name of the pet.
         */
        public string Name { get; set; }

        /**
         * The type of pet.
         */
        public string Type { get; set; }

        public override int GetHashCode()
        {
            var hash = 17;

            hash = hash * 23 * this.Name.GetHashCode();
            hash = hash * 23 * this.ImageUrl.GetHashCode();

            return hash;

        }

        public override bool Equals(object obj)
        {

            var item = obj as Pet;

            if (item == null)
            {
                return false;
            }

            return this.Identifier == item.Identifier;

        }

    }

}