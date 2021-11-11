using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomInfoAPI.Data;
using RoomInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomStoreContext _context;
        private readonly IMapper _mapper;

        public RoomRepository(RoomStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<RoomModel>> getAllRoomsAsync()
        {
            try
            {

                var records = await _context.Room.ToListAsync();
                return _mapper.Map<List<RoomModel>>(records);

            }
            catch
            {
                return null;
            }

        }

        public async Task<RoomModel> GetRoomByIdAsync(int id)
        {
            var room = await _context.Room.FindAsync(id);
            return _mapper.Map<RoomModel>(room);
        }

        public async Task<int> AddRoomAsync(RoomModel roomModel)
        {
            try
            {
                Room _room = _mapper.Map<Room>(roomModel);
                _room.RoomStatus = "Clean";
                _context.Room.Add(_room);
                await _context.SaveChangesAsync();

                return _room.RoomId;
            }

            catch
            {

                return -1;

            }

        }

        public async Task<bool> UpdateRoomAsync(int _roomId, RoomModel roomModel)
        {

            try
            {
                Room _room = _mapper.Map<Room>(roomModel);
                _room.RoomId = _roomId;

                _context.Room.Update(_room);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> ChangeStatusAsync(int _roomId, string _status)
        {

            try
            {

                Room _room = new Room();

                _room = _context.Room.Where(x => x.RoomId == _roomId).FirstOrDefault<Room>();
                if (_room != null)
                {
                    _room.RoomStatus = _status;
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

        public async Task<bool> DeleteRoomAsync(int id)
        {

            try
            {

                var _room = new Room()
                {
                    RoomId = id
                };


                _context.Room.Remove(_room);

                await _context.SaveChangesAsync();

                return true;

            }

            catch
            {

                return false;

            }
        }
    }
}
