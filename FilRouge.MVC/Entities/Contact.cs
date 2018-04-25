using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilRouge.MVC.Entities
{
    public partial class Contact : IdentityUser
    {
        #region Properties
        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }*/
        public string Name { get; set; }
        #endregion
        #region Association
        public virtual List<Quizz> Quizzs { get; set; }
        #endregion

    }
}

