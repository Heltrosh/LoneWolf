using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoneWolf
{
    internal class Character
    {
        static Character _instance = new Character();
        public static Character Instance { get { return _instance; } }


    }
}
