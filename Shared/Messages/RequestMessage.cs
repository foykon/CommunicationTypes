using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public record RequestMessage
    {
        public string Text { get ; set ; }
        public int No { get ; set ; }
    }
}
