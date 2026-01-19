using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxularyApp.Models
{
    public class CommandRequest
    {
        public ushort[] Value { get; set; }
        public byte BlockId { get; set; }

    }
}
