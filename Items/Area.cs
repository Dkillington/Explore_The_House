using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFTutorial.Items
{
    // Different locations in game (With unique background, buttons to press, and sounds)
    public class Area
    {
        public AreaEnum imgName; // Image file name (Without extension)
        public List<Button> buttons = new List<Button>(); // List of Buttons associated with this area
        public List<SoundEnums> sounds = new List<SoundEnums>(); // List of sounds associated with this area

        public Area(AreaEnum _imgName, List<Button> _areaButtons, List<SoundEnums> _areaSounds)
        {
            imgName = _imgName;
            buttons = _areaButtons;
            sounds = _areaSounds;
        }
        public Area()
        {

        }
    }

    // When you add a picture file to 'pics', make sure it is a jpg and spelled exactly like you put it here
    public enum AreaEnum
    {
        front,
        hall,
        bedroom,
        pot,
        kitchen,
        table,
        underbed,
        attic,

        // Unused
        basement,
        lab,
    }
}
