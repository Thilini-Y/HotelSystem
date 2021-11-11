using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<ContactModel> CallConactByIdUrl(int id)
        {
            ContactModel contact = new ContactModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44311/api/contact/" + id))
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44333/api/room/" + id))
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44359/api/property/" + id))
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:44333/api/room/occupied/" + id,null))
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:44333/api/room/dirty/" + id, null))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    roomResponse = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            return roomResponse;
        }


    }
}
