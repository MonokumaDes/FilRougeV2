using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilRouge.MVC.Entities
{
    public partial class TypeQuestion
    {
        #region Properties
        public int TypeQuestionId { get; set; }
        public string NameType { get; set; }

        #endregion
        #region Association
        public virtual List<Questions> Question { get; set; }
        #endregion
    }
}
