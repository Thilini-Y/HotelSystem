using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        public async Task<PropertyModel> CallPropertybyIdUrl(int id)
        {
            PropertyModel propertyDetails = new PropertyModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44359/api/property/"+id))
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44313/api/price/" + id))
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44310/api/features/" + id))
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
            using (var client = new HttpClient())
            {
                
                using (var response = await client.PostAsJsonAsync("https://localhost:44305/api/reservation/unavailablerooms", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    unavailableRoomlist = JsonConvert.DeserializeObject<List<int>>(apiResponse);
                    
                }

            }

            return unavailableRoomlist;
        }


    }
}
