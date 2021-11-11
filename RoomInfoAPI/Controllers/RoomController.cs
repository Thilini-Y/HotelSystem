using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomInfoAPI.Helpers;
using RoomInfoAPI.Models;
using RoomInfoAPI.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
  

        public RoomController(IRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;
            
        }

        [HttpGet("")]
        public async Task<List<RoomModel>> GetAllRooms()
        {
            var records = await _roomRepository.getAllRoomsAsync();

            if (records != null)
            {

                return records;

            }
            else
            {
                return null;
            }

        }


        [HttpGet("alldetails")]
        public async Task<List<object>> GetAllRoomsAllDeatils()
        {
            var records = await _roomRepository.getAllRoomsAsync();
            List<object> rooms = new List<object>();

            if (records != null)
            {

                foreach (RoomModel item in records)
                {
                    rooms.Add(await GetRoomByIdAllDetails(item.RoomId));
                }

                    return rooms;
                
            }
            else
            {
                return null;
            }

        }

        [HttpGet("{id}")]
        public async Task<RoomModel> GetRoomById([FromRoute] int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null)
            {
                return null;
            }
            else
            {
                return room;
            }
        }


        [HttpGet("alldetails/{id}")]
        public async Task<List<object>> GetRoomByIdAllDetails([FromRoute] int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null)
            {
                return null;
            }
            else
            {
                List<object> list = new List<object>();

                //room details
                list.Add(room);


                //get property details
                int propertyId = room.PropertyId;
                CallingHelper helper = new CallingHelper();
                PropertyModel propertyModel = await helper.CallPropertybyIdUrl(propertyId);
                list.Add(propertyModel);
                //return propertyModel;

                //price
                int priceId = room.PriceId;
                PriceModel priceModel = await helper.CallPricebyIdUrl(priceId);
                list.Add(priceModel);
                //return priceModel;

                //features
                string featuresIdString = room.FeaturesIds;
                string[] featuresList = featuresIdString.Split(",");
                List<FeaturesModel> features = new List<FeaturesModel>();
                foreach (string item in featuresList)
                {
                    FeaturesModel featureModel = await helper.CallFeatureByIdUrl(int.Parse(item));
                    features.Add(featureModel);
                }

                //return features;
                list.Add(features);
                return list;
            }
        }


        [HttpPost("")]
        public async Task<IActionResult> AddNewRoom([FromBody] RoomModel roomModel)
        {
            var id = await _roomRepository.AddRoomAsync(roomModel);
            if (id != -1)
            {
                return CreatedAtAction(nameof(GetRoomById), new { id = id, Controller = "room" }, id);
            }
            else
            {
                return StatusCode(500, "Can not add data at this moment");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomModel roomModel, [FromRoute] int id)
        {
            var status = await _roomRepository.UpdateRoomAsync(id, roomModel);
            if (status)
            {
                return Ok("Data is successfully updated");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPut("clean/{id}")]
        public async Task<IActionResult> CleanRoom([FromRoute] int id)
        {
            var status = await _roomRepository.ChangeStatusAsync(id, "clean");
            if (status)
            {
                return Ok("Data is successfully updated");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPut("occupied/{id}")]
        public async Task<bool> OccupiedRoom([FromRoute] int id)
        {
            var status = await _roomRepository.ChangeStatusAsync(id, "occupied");
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut("dirty/{id}")]
        public async Task<bool> DirtyRoom([FromRoute] int id)
        {
            var status = await _roomRepository.ChangeStatusAsync(id, "dirty");
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            var status = await _roomRepository.DeleteRoomAsync(id);
            if (status)
            {
                return Ok("Data is successfully deleted");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPost("availablerooms")]
        public async Task<object> AvailableRooms([FromBody] DateModel dates)
        {

            CallingHelper helper = new CallingHelper();
            List<int> unavailableRoomIds = await helper.UnavailableRoom(dates);

            var allRooms = await GetAllRooms();
            List<RoomModel> allRoomList = allRooms.ToList();
            List<int> allRoomIds = new List<int>();
            foreach (RoomModel item in allRoomList)
            {
                allRoomIds.Add(item.RoomId);

            }


            List<int> availableRoomIds = allRoomIds.Except(unavailableRoomIds).ToList();

            List<RoomModel> availableRoomDetailsList = new List<RoomModel>();

            foreach (int id in availableRoomIds)
            {
                RoomModel roomDetails = await GetRoomById(id);
                availableRoomDetailsList.Add(roomDetails);
            }
            

            return availableRoomDetailsList;
        }


    }
}
