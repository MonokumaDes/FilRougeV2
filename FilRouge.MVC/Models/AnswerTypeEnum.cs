using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FilRouge.MVC.Models
{
    public enum AnswerTypeEnum
    {
        [Display(Name = "Saisie libre")]
        SaisieLibre = 1,

        [Display(Name = "Choix unique")]
        ChoixUnique = 2,

        [Display(Name = "Choix multiple")]
        ChoixMultiple = 3,

        [Display(Name = "Saisie code")]
        SaisieCode = 4
    }

    public static class EnumExtensions
    {
        /*
        Retrieves the<see cref= "DisplayAttribute.Name" /> property on the <see cref = "DisplayAttribute" />
        of the current enum value, or the enum's member name if the <see cref="DisplayAttribute" /> is not present.
        </summary>
        <param name = "val" > This enum member to get the name for.</param>
        <returns>The<see cref="DisplayAttribute.Name" /> property on the<see cref= "DisplayAttribute" /> attribute, if present.</returns>*/
        /// <summary>
        /// Permet de retourner l'attribut <see cref="Name" /> de l'annotation Display définit pour l'Enum
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DisplayAttribute>(false)
                      ?.Name
                      ?? val.ToString();
        }

    }

}