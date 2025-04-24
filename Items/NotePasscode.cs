namespace WPFTutorial.Items
{
    public class NotePasscode
    {
        public string code;
        public StorageEnums connectedStorage;

        public NotePasscode(string _code, StorageEnums _connectedStorage)
        {
            code = _code;
            connectedStorage = _connectedStorage;



        }


    }
}
