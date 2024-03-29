﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DataCordonBleu.Models {
    public class Stuffer {
        private string _Message;
        private string _Password;
        private string _FilePath;

        [Display(Name = "Message")]
        [Required]
        public string Message {
            get { return _Message; }
            set { _Message = value; }
        }

        [Display(Name = "Password to encode")]
        [Required]
        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }

        public string FilePath {
            get { return _FilePath; }
            set { _FilePath = value; }
        }


    }
}
