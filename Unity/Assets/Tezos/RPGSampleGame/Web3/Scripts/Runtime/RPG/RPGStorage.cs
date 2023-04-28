
using RMC.Core.Components.Attributes;

namespace TezosSDKSamples.RPG
{
    /// <summary>
    /// Disk storage that persist across scenes and across game sessions.
    /// </summary>
    [CustomFilePath("RPGStorage", CustomFilePathLocation.StreamingAssetsPath)]
    public class RPGStorage
    {
        //  Properties ------------------------------------
        
        
        //  Fields ----------------------------------------
        
        /// <summary>
        /// Determines if the user is authenticated via Tezos
        /// </summary>
        public bool IsAuthenticated = false;
        
        /// <summary>
        /// Determines if the user has the required item asset via Tezos
        /// </summary>
        public bool HasRequiredNFT = false;
        
        //  Initialization --------------------------------

        
        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
    }
}