using ContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Repository
{
    public interface IContactRepository
    {
        Task<List<ContactModel>> getAllContactAsync();

        Task<ContactModel> GetContactByIdAsync(int contactId);

        Task<int> AddContactAsync(ContactModel contactModel);

        Task<bool> UpdateContactAsync(int _contactId, ContactModel contactModel);

        Task<bool> DeleteContactAsync(int contactId);

        
    }
}
