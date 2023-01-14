using Inform.BLL.Models;
using Inform.DAL.Entities;
using Inform.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inform.BLL.Services
{
    public class ContactService
    {
        private readonly IRepository<Contact> _repository;

        public ContactService(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<List<ContactDTO>> GetContacts()
        {
            try
            {
                var getData = await _repository.GetAllAsync();

                return getData.Select(a => new ContactDTO
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email= a.Email,
                    Address = a.Address,
                    City = a.City,
                    PhoneNumber= a.PhoneNumber,
                    Gender= a.Gender,

                }).OrderBy(e => e.FirstName).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddContact(ContactDTO contactDTO)
        {
            try
            {
                var contact = new Contact();

                contact.FirstName = contactDTO.FirstName;
                contact.LastName = contactDTO.LastName;
                contact.Email = contactDTO.Email;
                contact.Address = contactDTO.Address;
                contact.City = contactDTO.City;
                contact.PhoneNumber = contactDTO.PhoneNumber;
                contact.Gender = contactDTO.Gender;

                var response = await _repository.AddAsync(contact);

                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public ContactDTO GetContact(int id)
        {
            try
            {
                var record = new ContactDTO();

                var contact = _repository.GetById(id);

                if (contact != null)
                {
                    record.Id = contact.Id;
                    record.FirstName = contact.FirstName;
                    record.LastName = contact.LastName;
                    record.Email = contact.Email;
                    record.Address = contact.Address;
                    record.City = contact.City;
                    record.PhoneNumber = contact.PhoneNumber;
                    record.Gender = contact.Gender;

                    return record;
                }

                return record;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> UpdateContact(ContactDTO contactDTO)
        {
            try
            {
                var updateContact = _repository.Get(e => e.Id == contactDTO.Id);

                if (updateContact != null)
                {
                    updateContact.FirstName = contactDTO.FirstName;
                    updateContact.LastName = contactDTO.LastName;
                    updateContact.Email = contactDTO.Email;
                    updateContact.Address = contactDTO.Address;
                    updateContact.City = contactDTO.City;
                    updateContact.PhoneNumber = contactDTO.PhoneNumber;
                    updateContact.Gender = contactDTO.Gender;

                    return await _repository.UpdateAsync(updateContact);
                }

                return false;
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }
    }
}
