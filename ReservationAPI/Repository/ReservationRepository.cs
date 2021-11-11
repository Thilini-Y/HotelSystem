using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository 
    {
        private readonly ReservationStoreContext _context;
        private readonly IMapper _mapper;

        public ReservationRepository(ReservationStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<ReservationModel>> getAllReservationAsync()
        {
            try
            {

                var records = await _context.Reservations.ToListAsync();
                return _mapper.Map<List<ReservationModel>>(records);

            }
            catch
            {
                return null;
            }

        }

        public async Task<ReservationModel> GetReservationByIdAsync(int id)
        {
            var reaservation = await _context.Reservations.FindAsync(id);
            return _mapper.Map<ReservationModel>(reaservation);
        }

        public async Task<int> AddReservationAsync(ReservationModel reservationModel)
        {
            try
            {
                Reservations _reservation = _mapper.Map<Reservations>(reservationModel);
                _reservation.Status = "Confirmed";
                _context.Reservations.Add(_reservation);
                await _context.SaveChangesAsync();

                return _reservation.ReservationId;
            }

            catch
            {

                return -1;

            }

        }

        public async Task<bool> UpdateReservationAsync(int _reservationId, ReservationModel reservationModel)
        {

            try
            {
                Reservations _reservation = _mapper.Map<Reservations>(reservationModel);
                _reservation.ReservationId = _reservationId;

                _context.Reservations.Update(_reservation);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> ChangeStatusAsync(int _reservationId, string _status)
        {

            try
            { 

                Reservations reservation = new Reservations();

                reservation = _context.Reservations.Where(x => x.ReservationId == _reservationId).FirstOrDefault<Reservations>();
                if (reservation != null)
                {
                    reservation.Status = _status;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
            }

        }

        

        public async Task<bool> DeleteReservationAsync(int id)
        {

            try
            {

                var _reservation = new Reservations()
                {
                    ReservationId = id
                };


                _context.Reservations.Remove(_reservation);

                await _context.SaveChangesAsync();

                return true;

            }

            catch
            {

                return false;

            }
        }

        public async Task<List<ReservationModel>> FindUnconflictReservations(DateModel dates)
        {
            var records = await _context.Reservations.Where(x => x.StartDate > dates.endDate || x.EndDate < dates.startDate).ToListAsync();
            return _mapper.Map<List<ReservationModel>>(records);
        }
    }
}
