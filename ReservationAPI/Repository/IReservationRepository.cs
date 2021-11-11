using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Repository
{
    public interface IReservationRepository
    {
        Task<List<ReservationModel>> getAllReservationAsync();

        Task<ReservationModel> GetReservationByIdAsync(int id);

        Task<int> AddReservationAsync(ReservationModel reservationModel);

        Task<bool> UpdateReservationAsync(int _reservationId, ReservationModel reservationModel);

        Task<bool> DeleteReservationAsync(int id);

        Task<bool> ChangeStatusAsync(int _reservationId, string _status);
    }
}
