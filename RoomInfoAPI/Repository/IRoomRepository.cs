using RoomInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Repository
{
    public interface IRoomRepository
    {
        Task<List<RoomModel>> getAllRoomsAsync();

        Task<RoomModel> GetRoomByIdAsync(int id);

        Task<int> AddRoomAsync(RoomModel roomModel);

        Task<bool> UpdateRoomAsync(int _roomId, RoomModel roomModel);

        Task<bool> DeleteRoomAsync(int id);

        Task<bool> ChangeStatusAsync(int _roomId, string _status);
    }
}
