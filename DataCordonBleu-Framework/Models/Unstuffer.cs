using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DataCordonBleu.Models {
    public class Unstuffer {
        private string _Password;

        [Display(Name = "Password to decode")]
        [Required]
        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }
    }
}
