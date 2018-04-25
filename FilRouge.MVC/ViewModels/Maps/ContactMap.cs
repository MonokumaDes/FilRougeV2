using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.Entities;
using Microsoft.AspNet.Identity;

namespace FilRouge.MVC.ViewModels.Maps
{
    public static class ContactMap
    {
        /// <summary>
        /// Convertir un "Contact" en "ContactViewModel"
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static ContactViewModel MapToContactViewModel(this Contact contact)
        {
            var contactViewModel = new ContactViewModel();
            if (contact == null)
            {
                return contactViewModel;
            }

            contactViewModel = new ContactViewModel
            {
                Id = contact.Id,
                UserName = contact.UserName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                //SesRoles = contact.Roles.Select(s => s.RoleId).ToList(), //recupere juste les id des roles
                Password = contact.PasswordHash,
                Name = contact.Name,
                RoleId = contact.Roles.Count > 0 ? contact.Roles.First().RoleId : "", //faudra afficher le nom du role

            };
            return contactViewModel;
        }

        /// <summary>
        /// Convertir un "ContactViewModel" en "Contact"
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        public static Contact MapToContact(this ContactViewModel contactViewModel)
        {
            var contact = new Contact();
            if (contactViewModel == null)
            {
                return contact;
            }

            contact = new Contact
            {
                Id = contactViewModel.Id,
                UserName = contactViewModel.UserName,
                Email = contactViewModel.Email,
                PhoneNumber = contactViewModel.PhoneNumber,
                Name = contactViewModel.Name,
                //Roles = contactViewModel.SesRoles //impossible Roles est seulement en lecture....
                PasswordHash = contactViewModel.Password
            };
            return contact;
        }
    }
}