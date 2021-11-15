using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoomInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RoomInfoAPI.Helpers
{
    public class CallingHelper
    {

        public CallingHelper()
        {

        }

        public static string urlJsonString = System.IO.File.ReadAllText("ServerUrls.json");
        public static JObject urlJObject = JObject.Parse(urlJsonString);


        public async Task<PropertyModel> CallPropertybyIdUrl(int id)
        {
            PropertyModel propertyDetails = new PropertyModel();
           
            string propertyUrl = urlJObject.SelectToken("PropertyByID").Value<string>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(propertyUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    propertyDetails = JsonConvert.DeserializeObject<PropertyModel>(apiResponse);
                }
            }
            return propertyDetails;
        }

        public async Task<PriceModel> CallPricebyIdUrl(int id)
        {
            PriceModel priceDetails = new PriceModel();
            string priceUrl = urlJObject.SelectToken("PriceByID").Value<string>();


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(priceUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    priceDetails = JsonConvert.DeserializeObject<PriceModel>(apiResponse);
                }
            }
            return priceDetails;
        }

        public async Task<FeaturesModel> CallFeatureByIdUrl(int id)
        {
            FeaturesModel featureDetails = new FeaturesModel();
            string featuresUrl = urlJObject.SelectToken("FeaturesByID").Value<string>();


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(featuresUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    featureDetails = JsonConvert.DeserializeObject<FeaturesModel>(apiResponse);
                }
            }
            return featureDetails;
        }

       
        public async Task<List<int>> UnavailableRoom(DateModel content)
        {
            List<int> unavailableRoomlist = new List<int>();
            string unavailableRoomIdsUrl = urlJObject.SelectToken("UnavailableRoomIds").Value<string>();


            using (var client = new HttpClient())
            {
                
                using (var response = await client.PostAsJsonAsync(unavailableRoomIdsUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    unavailableRoomlist = JsonConvert.DeserializeObject<List<int>>(apiResponse);
                    
                }

            }

            return unavailableRoomlist;
        }


    }
}
