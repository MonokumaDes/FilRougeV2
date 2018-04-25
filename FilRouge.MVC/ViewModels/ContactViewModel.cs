using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace FilRouge.MVC.ViewModels
{
    public class ContactViewModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse email")]
        public string UserName { get; set; }

        [Display(Name = "Téléphone")]
        public string PhoneNumber { get; set; }

        //[Required]
        [Display(Name = "Adresse d'utilisateur")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        /*[Display(Name = "Roles")]
        public List<string> SesRoles { get; set; }*/

        [Required]
        [Display(Name = "Nom du Role")]
        public string RoleId { get; set; }

        [Display(Name = "Ancien Role")]
        public string OldRoleId { get; set; }

        [Display(Name = "Ancien Mot de passe")]
        public string OldPassword { get; set; }

        [Display(Name = "Nom d'utilisateur")]
        public string Name { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}