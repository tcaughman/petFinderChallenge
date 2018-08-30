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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Petfinder.API.Responses
{
    public class LastOffset
    {
        public string item { get; set; }
    }

    public class Photo
    {
        [JsonProperty("@size")]
        public string size { get; set; }
        [JsonProperty("$t")]
        public string item { get; set; }
        [JsonProperty("@id")]
        public string id { get; set; }
    }

    public class Photos
    {
        [JsonProperty("photo")]
        public List<Photo> photo { get; set; }
    }

    public class Media
    {
        [JsonProperty("photos")]
        public Photos photos { get; set; }
    }

    public class Id
    {
        [JsonProperty("$t")]
        public string id { get; set; }
    }

    public class Breeds
    {   
        public object breed { get; set; }
    }

    public class Name
    {
        [JsonProperty("$t")]
        public string name { get; set; }
    }

    public class Animal
    {
        [JsonProperty("$t")]
        public string item { get; set; }
    }

    public class Pet
    {
        public Media media { get; set; }
        public Id id { get; set; }
        public Breeds breeds { get; set; }
        public Name name { get; set; }
        public Animal animal { get; set; }
    }

    public class Pets
    {
        public List<Pet> pet { get; set; }
    }

    public class Timestamp
    {
        public DateTime item { get; set; }
    }

    public class Code
    {
        public string item { get; set; }
    }

    public class Status2
    {
        public Code code { get; set; }
    }

    public class Version
    {
        public string item { get; set; }
    }

    public class Header
    {
        public Timestamp timestamp { get; set; }
        public Status2 status { get; set; }
        public Version version { get; set; }
    }

    public class Petfinder
    {
        public string xsi { get; set; }
        public LastOffset lastOffset { get; set; }
        public Pets pets { get; set; }
        public Header header { get; set; }
        public string noNamespaceSchemaLocation { get; set; }
    }

    public class RootObject
    {
        public string encoding { get; set; }
        public string version { get; set; }
        public Petfinder petfinder { get; set; }
    }


}