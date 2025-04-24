using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Items
{
    public class Photograph : Item
    {
        public string path;
        public Photograph(ItemEnums itemEnum, string name, string description, string _path) : base(itemEnum, name, description)
        {
            path = _path;
        }
    }
}
