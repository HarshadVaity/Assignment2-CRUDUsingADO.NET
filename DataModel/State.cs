using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class State
    {
        public int Id { get; set; }

        [DisplayName("State")]
        public string Name { get; set; }

    }
}
