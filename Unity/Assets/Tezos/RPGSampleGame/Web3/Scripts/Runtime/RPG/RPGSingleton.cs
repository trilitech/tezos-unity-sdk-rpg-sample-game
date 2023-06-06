
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RMC.Core.DesignPatterns.Creational.Singleton.CustomSingleton;
using RPGM.Core;
using RPGM.Gameplay;
using Scripts.Tezos;
using TezosSDKExamples.Shared.Tezos;
using TezosSDKSamples.RPG.Scenes;
using UnityEngine;

namespace TezosSDKSamples.RPG
{
    /// <summary>
    /// The main entry point for the Tezos-related RPG functionality .
    /// </summary>
    public class RPGSingleton : Singleton<RPGSingleton>, ISingletonParent
    {
        //  Properties ------------------------------------

        /// <summary>
        /// Determines if the user is authenticated via Tezos
        /// 
        /// Set called by <see cref="Scene01_IntroMenu"/> via Tezos SDK For Unity
        /// Get called by <see cref="Scene02_Game"/>
        /// </summary>
        public bool IsAuthenticated { get; set; }
        
        /// <summary>
        /// Determines if the user has the required NFT
        /// 
        /// Set called by <see cref="Scene01_IntroMenu"/> via Tezos SDK For Unity
        /// Get called by <see cref="Scene02_Game"/>
        /// </summary>
        public bool HasRequiredNFT { get; set; }

        /// <summary>
        /// Determines the player display name. 
        /// 
        /// Set called by <see cref="Scene01_IntroMenu"/> via Tezos SDK For Unity
        /// Get called by <see cref="Scene02_Game"/>
        /// </summary>
        public string PlayerName { get; set; }
        
        /// <summary>
        /// The walking speed for the player character
        /// </summary>
        public float WalkingSpeed
        {
            get
            {
                GameModel gameModel = Schedule.GetModel<GameModel>();
                return gameModel.input.stepSize;
            }
            set
            {
                GameModel gameModel = Schedule.GetModel<GameModel>();
                gameModel.input.stepSize = value;
            }
        }

       

        //  Fields ----------------------------------------

        
        //  Initialization --------------------------------
        void ISingletonParent.OnInstantiatedChild()
        {
            // Default Name
            PlayerName = "Player"; 
            
            // Default Desktop Build Screen Resolution
            // So the pixel art looks nice and sharp
            Screen.SetResolution(
                RPGConstants.BuildScreenResolutionWidth, 
                RPGConstants.BuildScreenResolutionHeight,
                FullScreenMode.FullScreenWindow);
        }

        
        
        //  Methods ---------------------------------------
        
        
        /// <summary>
        /// Determines if 1 item exists in the player character inventory
        /// </summary>
        public bool HasInventoryItem(InventoryItem inventoryItem)
        {
            GameModel gameModel = Schedule.GetModel<GameModel>();
            
            int itemCountToCheck = 1;
            return gameModel.HasInventoryItem(inventoryItem.name, itemCountToCheck);
        }

        
        /// <summary>
        /// Adds 1 item from the player character inventory
        /// </summary>
        public void AddInventoryItem(InventoryItem inventoryItem)
        {
            GameModel gameModel = Schedule.GetModel<GameModel>();
            gameModel.AddInventoryItem(inventoryItem);
        }

        
        /// <summary>
        /// Removes 1 item from the player character inventory
        /// </summary>
        public void RemoveInventoryItem(InventoryItem inventoryItem)
        {
            GameModel gameModel = Schedule.GetModel<GameModel>();
            
            int itemCountToRemove = 1;
            gameModel.RemoveInventoryItem(inventoryItem, itemCountToRemove);
        }

        
        /// <summary>
        /// Show dynamic instructions text on the <see cref="Scene01_IntroMenu"/>
        /// </summary>
        /// <returns></returns>
        public string GetStatusText()
        {
            string isAuthenticatedMessage = "You are logged out";
            if (IsAuthenticated)
            {
                isAuthenticatedMessage = $"You are <b>logged in</b> as <b>{PlayerName}</b>";
            }

            string hasRequiredNFTMessage = "";
            if (HasRequiredNFT)
            {
                hasRequiredNFTMessage = " with the <b>Green Emerald</b>";
            }
            return $"{isAuthenticatedMessage}{hasRequiredNFTMessage}.";
        }
        
        
        /// <summary>
        /// For debugging, accept keyboard input and Debug log helpful text
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> TakeInputForDebugging()
        {
            bool isRefreshNeeded = false;
            // When the user presses 'N', toggle the value of HasRequiredNFT
            if (Input.GetKeyDown(KeyCode.N) && IsAuthenticated)
            {
                HasRequiredNFT = !HasRequiredNFT;
                Debug.Log($"Debugging: HasRequiredNFT now is = {RPGSingleton.Instance.HasRequiredNFT}");
                isRefreshNeeded = true;
            }
            
            // When the user presses 'D', Debug.log some helpful text
            if (Input.GetKeyDown(KeyCode.D) && IsAuthenticated)
            {
                // Tezos SDK For Unity
                // Usage: Store reference for convenience
                ITezos tezos = TezosSingleton.Instance;
                
                // Tezos SDK For Unity
                // Returns the address of the current active wallet
                string activeWalletAddress = tezos.Wallet.GetActiveAddress();
                
                // Tezos SDK For Unity
                // Usage: Gets all tokens owned by the authenticated user account
                List<TokenBalance> nfts = 
                    await tezos.GetAllTokensForOwner(activeWalletAddress);
                
                // Show debug text
                // What NFT(s) does the game require?
                Debug.Log($"Debugging: DemoNFTAddress = {RPGConstants.DemoNFTAddress}");
                Debug.Log($"Debugging: DemoTokenId = {RPGConstants.DemoTokenId}");
                
                // Show debug text
                // What NFT(s) does the current user have?
                string listOfNftAddresses = "";
                foreach (TokenBalance tokenBalance in nfts)
                {
                    listOfNftAddresses += $"Address = {tokenBalance.tokenContract}, TokenID = {tokenBalance.tokenId}\n";
                }
                Debug.Log($"Debugging: HasRequiredNft = {HasRequiredNFT}");
                Debug.Log($"Debugging: Nfts.Count = {nfts.Count}, Details...\n\n{listOfNftAddresses}");
            }

            // Let the scene know to refresh any screen text
            return isRefreshNeeded;
        }
        
        //  Event Handlers --------------------------------
    }
}
