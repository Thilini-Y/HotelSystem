using AutoMapper;
using ContactAPI.Data;
using ContactAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactStoreContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(ContactStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<ContactModel>> getAllContactAsync()
        {
            var records = await _context.Contacts.ToListAsync();
            return _mapper.Map<List<ContactModel>>(records);
        }

        public async Task<ContactModel> GetContactByIdAsync(int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);
            return _mapper.Map<ContactModel>(contact);
        }

        public async Task<int> AddContactAsync(ContactModel contactModel)
        {
            try
            {
                Contacts contact = _mapper.Map<Contacts>(contactModel);
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return contact.ContactId;
            }

            catch {

                return -1;
            
            }
            
        }

        public async Task<bool> UpdateContactAsync(int _contactId, ContactModel contactModel)
        {
           
            try
            {
                Contacts contact = _mapper.Map<Contacts>(contactModel);
                contact.ContactId = _contactId;

                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeleteContactAsync(int contactId)
        {

            try
            {

                var contact = new Contacts()
                {
                    ContactId = contactId
                };


                _context.Contacts.Remove(contact);

                await _context.SaveChangesAsync();

                return true;

            }

            catch {

                return false;
            
            }

           
        }

    }
}
