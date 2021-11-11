using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Helpers;
using ReservationAPI.Models;
using ReservationAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }

        [HttpGet("")]
        public async Task<List<ReservationModel>> GetAllReservations()
        {
            var reservations = await _reservationRepository.getAllReservationAsync();

            if (reservations != null)
            {
                return reservations;
            }
            else
            {
                return null;
            }

        }

        [HttpGet("alldetails")]
        public async Task<List<object>> GetAllReservationsAllDeatils()
        {
            var records = await _reservationRepository.getAllReservationAsync();
            List<object> reservations = new List<object>();

            if (records != null)
            {

                foreach (ReservationModel item in records)
                {
                    reservations.Add(await GetReservationByIdAllDetails(item.ReservationId));
                }

                return reservations;

            }
            else
            {
                return null;
            }

        }

        [HttpGet("{id}")]
        public async Task<ReservationModel> GetReservationById([FromRoute] int id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }
            else
            {
                return reservation;
            }

        }


        [HttpGet("alldetails/{id}")]
        public async Task<List<object>> GetReservationByIdAllDetails([FromRoute] int id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }
            else
            {
                //return Ok(reservation);

                List<object> list = new List<object>();
                list.Add(reservation);

                //get contact details relavent to the reservation
                int contactId = reservation.ContactId;
                CallingHelper helper = new CallingHelper();
                object contactDetails = await helper.CallConactByIdUrl(contactId);
                list.Add(contactDetails);

                //get room details
                int roomId = reservation.RoomId;
                object roomDetails = await helper.CallRoomByIdUrl(roomId);
                list.Add(roomDetails);

                //get property details
                int propertyId = reservation.PropertyId;
                object propertyDetails = await helper.CallPropertyByIdUrl(propertyId);
                list.Add(propertyDetails);


                return list;
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewReservation([FromBody] ReservationModel reservationModel)
        {
            var id = await _reservationRepository.AddReservationAsync(reservationModel);
            if (id != -1)
            {
                return CreatedAtAction(nameof(GetReservationById), new { id = id, Controller = "reservation" }, id);
            }
            else
            {
                return StatusCode(500, "Can not add data at this moment");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationModel reservationModel, [FromRoute] int id)
        {
            var status = await _reservationRepository.UpdateReservationAsync(id, reservationModel);
            if (status)
            {
                return Ok("Data is successfully updated");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPut("checkin/{id}")]
        public async Task<IActionResult> CheckIn( [FromRoute] int id)
        {
            var status = await _reservationRepository.ChangeStatusAsync(id,"checkedin");
            if (status)
            {
                CallingHelper helper = new CallingHelper();
                bool response = await helper.CallOccupiedRoom(id);
                if (response)
                {
                    return Ok("Data is successfully updated");
                }
                else
                {
                    await _reservationRepository.ChangeStatusAsync(id, "confirmed");
                    return StatusCode(500, "Requested data in not in the database");

                }
                
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPut("checkout/{id}")]
        public async Task<IActionResult> CheckOut([FromRoute] int id)
        {
            var status = await _reservationRepository.ChangeStatusAsync(id, "checkedout");
            if (status)
            {

                CallingHelper helper = new CallingHelper();
                bool response = await helper.CallDirtyRoom(id);
                if (response)
                {
                    return Ok("Data is successfully updated");
                }
                else
                {
                    await _reservationRepository.ChangeStatusAsync(id, "checkedin");
                    return StatusCode(500, "Requested data in not in the database");

                }
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancelled([FromRoute] int id)
        {
            var status = await _reservationRepository.ChangeStatusAsync(id, "cancelled");
            if (status)
            {
                return Ok("Data is successfully updated");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            var status = await _reservationRepository.DeleteReservationAsync(id);
            if (status)
            {
                return Ok("Data is successfully deleted");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpPost("unavailablerooms")]
        public async Task<List<int>> UnavailableRooms([FromBody] DateModel dates)
        {
            var unconflictReservations = await _reservationRepository.FindUnconflictReservations(dates);
            

            List<ReservationModel> unconflictReservationsList = unconflictReservations.ToList();

            var allreservations = await GetAllReservations();
            List<ReservationModel> allreservationsList = allreservations.ToList();

            List<ReservationModel> conflictReservations = allreservationsList.Where(p => !unconflictReservationsList.Any(x => x.ReservationId == p.ReservationId)).ToList();


            List<int> unavailableRoomIds = new List<int>();
            foreach (ReservationModel item in conflictReservations)
            {
                unavailableRoomIds.Add(item.RoomId);

            }
            return unavailableRoomIds;
            
        }


    }
}
