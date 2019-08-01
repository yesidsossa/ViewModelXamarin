using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace ViewModel
{
    public class ItemViewModel : BaseViewModel<List<Item>>
    {
        protected override List<Item> LoadInBackground()
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://pokeapi.co/api/v2/pokemon?offset=20&limit=964");

            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            string responseString = response.Content.ReadAsStringAsync().Result;
            RootObject serializedObject = JsonConvert.DeserializeObject<RootObject>(responseString);


            var list = new List<Item>();
            foreach (var pokemon in serializedObject.results)
            {
                list.Add(new Item { Name = pokemon.name });
            }

            return list;
        }
    }

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public object next { get; set; }
        public string previous { get; set; }
        public List<Result> results { get; set; }
    }
}
