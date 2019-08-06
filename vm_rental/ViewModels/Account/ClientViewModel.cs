using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.ViewModels.Account
{
    public class ClientViewModel 
    {
        [Required]
        [MinLength(3, ErrorMessage="There most be more characters")]
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string firmName { get; set; }     

        public bool _isBusinessClient = false;

        public bool isBusinessClient
        {
            get
            {
                  return _isBusinessClient;
            }
            set
            {
                  _isBusinessClient = value;
          
                  if(firmName == null && (firstName != null && lastName != null))
                  {
                        firmName = firstName + " " + lastName;
                  }  
             }
        }
    }
}
