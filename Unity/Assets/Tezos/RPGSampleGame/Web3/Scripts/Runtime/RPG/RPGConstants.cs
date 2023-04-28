
using RMC.Core.UI.DialogSystem;

namespace TezosSDKSamples.RPG
{
    /// <summary>
    /// Shared, constant values
    /// </summary>
    public static class RPGConstants
    {
        //  Tezos Fields ----------------------------------------
        //public const string DemoNFTAddress = "KT1BRADdqGk2eLmMqvyWzqVmPQ1RCBCbW5dY"; //Developer may NOT have this one
        public const string DemoNFTAddress = "KT1GkE3oFqv8gyvCe3MvT5upXLcMtWmmj3DD"; //Developer may have this one
        public const int DemoTokenId = 1;
        
        //  Other Fields ----------------------------------------
        public const string PathMenuItemAssetsCompanyProject = "Assets/" + CompanyName + "/" + ProjectName;
        public const string PathMenuItemWindowCompanyProject = "Window/" + CompanyName + "/" + ProjectName;
        public const string CompanyName = "Tezos";
        public const string ProjectName = "RPG Sample Game";
        public const int PriorityMenuItem_Examples = 1;
        public const int BuildScreenResolutionWidth = 1280;
        public const int BuildScreenResolutionHeight = 960;
        
        //  Other Methods ----------------------------------------
        public static DialogData CreateNewDialogData(string dialogTitle)
        {
            float delaySecondsSending = 0.7f;
            float delaySecondsSent = 0.0f;
            float delaySecondsAwaiting = 0.7f;
                
            return  new DialogData(
                $"~ <b>{dialogTitle}</b> ~\nSending...",
                $"~ <b>{dialogTitle}</b> ~\nSent...",
                $"~ <b>{dialogTitle}</b> ~\nAwaiting...", 
                delaySecondsSending, 
                delaySecondsSent, 
                delaySecondsAwaiting
            );
        }
    }
}