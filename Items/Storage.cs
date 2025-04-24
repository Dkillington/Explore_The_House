using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFTutorial.View.UserControls;

namespace WPFTutorial.Items
{
    public class Storage
    {
        public string name;
        public string? requiredPassword;
        public List<ItemEnums> items = new List<ItemEnums>();

        public Storage(string _name, List<ItemEnums> startingItems, string? _requiredPassword = null)
        {
            name = _name;
            items = startingItems;

            if(_requiredPassword != null)
            {
                VerifyPasswordIsWithin4Digits();
            }

            void VerifyPasswordIsWithin4Digits()
            {
                int codeLength = _requiredPassword.Length;
                if (codeLength != 4)
                {
                    Debug.Print($"ERROR: Storage Container '{_name}' had a password length of {codeLength}, a length of 4 is required.");
                    requiredPassword = null;
                }
                else
                {
                    requiredPassword = _requiredPassword;
                }
            }
        }
    }

    public enum StorageEnums
    {
        none, // For NULL
        inventory,
        coatPocket,
        bedroomDrawer,
        bedroomChest,
        kitchenCabinet,
        upperKitchenCabinet,
        
        // Attic
        atticTable,
        atticChest,
    }
}
