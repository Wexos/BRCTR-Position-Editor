using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class WrongMagicException : Exception
    {
        public WrongMagicException(string WrongMagic, string Magic, long offset)
            : base("The magic " + WrongMagic + "  at offset 0x" + HexUtil.ConvertToHex(offset) + " does not match the magic " + Magic + "!")
        {

        }
    }
}
