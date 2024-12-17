using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseClass
    {
        public long Id { get; set; }
        public DateTime Creation {  get; set; }
        public DateTime Modification { get; set; }  
        public DateTime Deleted { get; set; }
    }
}
