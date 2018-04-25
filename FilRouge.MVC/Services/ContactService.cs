using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FilRouge.MVC.Services
{
    public class ContactService
    {
        /// <summary>
        /// Récuperer tous les contacts
        /// </summary>
        /// <returns>une liste de contact</returns>
        public List<ContactViewModel> GetAllContact()
        {
            List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            using (var db = new FilRougeDBContext())
            {
                var contacts = db.Users.Include(u => u.Roles).ToList();
                foreach (var contact in contacts)
                {
                    var contactVM = contact.MapToContactViewModel();
                    contactVM.RoleName = GetRoleName(contact.Roles.Count>0 ? contact.Roles.FirstOrDefault().RoleId : "");
                    contactViewModels.Add(contactVM);
                }
            }
            return contactViewModels;
        }

        /// <summary>
        /// Récuperer un contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactViewModel GetContactById(string id)
        {
            var contactViewModel = new ContactViewModel();
            using (var db = new FilRougeDBContext())
            {
                var contact = db.Users.Find(id);
                
                contactViewModel = contact.MapToContactViewModel();
                contactViewModel.RoleName = GetRoleName(contact.Roles.Count > 0 ? contact.Roles.FirstOrDefault().RoleId : "");
            }
            return contactViewModel;
        }


        /// <summary>
        /// Ajouter un contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <param name="manager"></param>
        /// <returns></returns>
        public string AddContact(ContactViewModel contactViewModel, ApplicationUserManager manager)
        {
            var id = string.Empty;
            using (var db = new FilRougeDBContext())
            {
                var user = new Contact { UserName = contactViewModel.UserName, Email = contactViewModel.UserName, Name = contactViewModel.Name, PhoneNumber = contactViewModel.PhoneNumber };
                var result = manager.CreateAsync(user, contactViewModel.Password).Result;
                if (result.Succeeded)
                {
                    id = user.Id;
                    //recuperation du nom du role 
                    var roleName = db.Roles.First(r => r.Id == contactViewModel.RoleId).Name;
                    var addRole = manager.AddToRole(user.Id, roleName).Succeeded; ;
                }

            }
            return id;
        }

        /// <summary>
        /// Modifier un contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <param name="manager"></param>
        /// <returns></returns>
        public string EditContact(ContactViewModel contactViewModel, ApplicationUserManager manager)
        {
            var id = string.Empty;
            using (var db = new FilRougeDBContext())
            {
                var contact = manager.Users.FirstOrDefault(u => u.Id == contactViewModel.Id);

                //creation du nouveau role
                //var oldRole = contact.Roles.FirstOrDefault();
                var listRoles = db.Roles.ToList();
                //List<string> listRoles = new List<string>();
                foreach (var role in listRoles)
                {
                    if(contact.Roles.Where(r => r.RoleId == role.Id).Count() > 0)
                    {
                        var removeRoleTest = manager.RemoveFromRoles(contact.Id, role.Name);
                    } 
                }
                //var listNameRoles = listRoles.ToArray();

                var roleName = db.Roles.First(r => r.Id == contactViewModel.RoleId).Name;
                var addRole = manager.AddToRole(contact.Id, roleName).Succeeded;

                contact.UserName = contactViewModel.UserName;
                contact.PhoneNumber = contactViewModel.PhoneNumber;
                contact.Email = contactViewModel.UserName;
                contact.Name = contactViewModel.Name;

                /* /!\ non terminer mais fonction 
                (si l'ancien mot de passe n'est pas bon ou que le nouveau mot de passe ne respecte pas 
                les contrainte de verification du nouveau mot de passe : 
                longueur mini de 6, longueur max 100, une lettre majuscule, un caractere special
                les autres modifications sont faites sauf pour le mot de passe
                 */
                if(contactViewModel.OldPassword != null && contactViewModel.Password != null 
                    && contact.PasswordHash.Length >= 6 && contactViewModel.Password.Length <= 100 )
                {
                    var resultPass = manager.ChangePasswordAsync(contact.Id, contactViewModel.OldPassword, contactViewModel.Password).Result;
                }

                IdentityResult result = manager.UpdateAsync(contact).Result;
                id = contact.Id;
            }
            return id;
        }

        public List<SelectListItem> GetListItemsRoles()
        {

            var listeRoles = new List<SelectListItem>();


            using (var db = new FilRougeDBContext())
            {
                var roles = db.Roles.ToList();

                foreach (var role in roles)
                {
                    listeRoles.Add(new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Id
                    });
                }

                return listeRoles;
            }
        }

        public string GetRoleName(string id)
        {
            using (var db = new FilRougeDBContext())
            {
                var role = db.Roles.FirstOrDefault(r => r.Id==id);
                if(role != null)
                {
                    return role.Name;
                }
            }
            return "";
        }
    }
}