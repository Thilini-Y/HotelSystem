using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReservationAPI.Helpers
{
    public class CallingHelper
    {
        public CallingHelper()
        {

        }

        public static string urlJsonString = System.IO.File.ReadAllText("ServerUrls.json");
        public static JObject urlJObject = JObject.Parse(urlJsonString);

        public async Task<ContactModel> CallConactByIdUrl(int id)
        {
            ContactModel contact = new ContactModel();
            string contactUrl = urlJObject.SelectToken("ContactByID").Value<string>();


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(contactUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    contact = JsonConvert.DeserializeObject<ContactModel>(apiResponse);
                }
             
            }
            return contact;
        }

        public async Task<RoomModel> CallRoomByIdUrl(int id)
        {

            RoomModel roomObject = new RoomModel();
            string roomUrl = urlJObject.SelectToken("RoomByID").Value<string>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(roomUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    roomObject = JsonConvert.DeserializeObject<RoomModel>(apiResponse);
                  
                }
                 
            }
            return roomObject;
        }

        public async Task<PropertyModel> CallPropertyByIdUrl(int id)
        {

            PropertyModel propertyObject = new PropertyModel();
            string propertyUrl = urlJObject.SelectToken("PropertyByID").Value<string>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(propertyUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    propertyObject = JsonConvert.DeserializeObject<PropertyModel>(apiResponse);

                }

            }
            return propertyObject;
        }

        public async Task<bool> CallOccupiedRoom(int id)
        {
            bool roomResponse;
            string OccupiedRoomUrl = urlJObject.SelectToken("OccupiedRoomIDs").Value<string>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync(OccupiedRoomUrl + id,null))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   roomResponse = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            return roomResponse;
        }

        public async Task<bool> CallDirtyRoom(int id)
        {
            bool roomResponse;
            string dirtyRoomUrl = urlJObject.SelectToken("DirtyRoomIDs").Value<string>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync(dirtyRoomUrl + id, null))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    roomResponse = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            return roomResponse;
        }


    }
}
