using System;
using System.ComponentModel;

namespace DataModel
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        
        public State State { get; set; }
    }
}
