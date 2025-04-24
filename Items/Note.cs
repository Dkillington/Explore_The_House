namespace WPFTutorial.Items
{
    public class Note : Item
    {
        public string text;
        public Note(ItemEnums itemEnum, string name, string description, string _text, string passcode = "") : base(itemEnum, name, description)
        {
            text = _text;

        }
    }
}
