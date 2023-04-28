using UnityEngine;
using Cysharp.Threading.Tasks;
using TezosAPI;
using TezosSDKExamples.Shared.Tezos;
using TezosSDKSamples.RPG.View;

#pragma warning disable CS4014, CS1998, CS0162
namespace TezosSDKSamples.RPG.Scenes
{
    /// <summary>
    /// Controller for <see cref="View.Scene01_IntroMenuView"/>
    /// </summary>
    public class Scene01_IntroMenu : MonoBehaviour
    {
        //  Properties ------------------------------------
        public Scene01_IntroMenuView View { get { return _view;}}

        
        //  Fields ----------------------------------------
        [Header("UI")]
        [SerializeField] 
        private Scene01_IntroMenuView _view;
        
        // Tezos SDK For Unity
        // Usage: Store reference for convenience
        private ITezosAPI _tezos;

        // Tezos SDK For Unity
        // Usage: Display the authentication button (Default is true)
        private const bool IsAuthenticationSupported = true; 


        //  Unity Methods  --------------------------------
        protected async void Start()
        {
            // Observe Events
            View.AuthenticationButtonUI.Button.onClick.AddListener(() => OnAuthenticateButtonClicked());
            View.PlayGameButtonUI.Button.onClick.AddListener( () => OnPlayGameButtonClicked());

            
            // Tezos SDK For Unity
            // Usage: Display the authentication button
            View.AuthenticationButtonUI.IsVisible = IsAuthenticationSupported;
            
            // Tezos SDK For Unity
            // Usage: Store reference for convenience
            _tezos = TezosSingleton.Instance;
            
            // Tezos SDK For Unity
            // Usage: Observe events for Tezos Wallet
            _tezos.MessageReceiver.AccountConnected += Tezos_OnAccountConnected;
            _tezos.MessageReceiver.AccountDisconnected += Tezos_AccountDisconnected;
            
            
            // Refresh
            await RefreshUIAsync();
        }
        
        
        protected async void Update()
        {
            // Debugging
            if (await RPGSingleton.Instance.TakeInputForDebugging())
            {
                // Display Status Text
                RefreshStatusText();
            }
        }


        //  Methods ---------------------------------------
        private async UniTask RefreshUIAsync()
        {
            // Setup
            string demoNFTAddress = RPGConstants.DemoNFTAddress;
            int demoTokenId = RPGConstants.DemoTokenId;
            
            // Tezos SDK For Unity
            // Usage: Determines if the user is authenticated with Tezos
            bool isAuthenticated = _tezos.HasActiveWalletAddress();

            bool hasRequiredNft = false;
            if (isAuthenticated)
            {
                // Tezos SDK For Unity
                // Returns the address of the current active wallet
                string activeWalletAddress = _tezos.GetActiveWalletAddress();

                // Tezos SDK For Unity
                // Determines if the user account owns a given Nft
                hasRequiredNft = await _tezos.IsOwnerOfToken(
                    activeWalletAddress, 
                    demoNFTAddress, 
                    demoTokenId);
                
                // Tezos SDK For Unity
                // Hardcode a special name for the authenticated user
                // You could use Tezos blockchain data to further customize naming 
                RPGSingleton.Instance.PlayerName = "Speedy Thief";
                

            }
            
            
            // Toggle Logo Visibility
            View.HeaderLogoImage.enabled = isAuthenticated;
            
            // Toggle Button Text
            string buttonText = "Log In";
            if (isAuthenticated)
            {
                buttonText = "Log Out";
            }
            View.AuthenticationButtonUI.Text.text = buttonText;

            ////////////////////////////////////////////////////////////
            // Tezos SDK For Unity
            // Store game-specific values now -- Easy use in other scenes
            RPGSingleton.Instance.IsAuthenticated = isAuthenticated;
            RPGSingleton.Instance.HasRequiredNFT = hasRequiredNft;
            ////////////////////////////////////////////////////////////
            
            // Display Status Text
            RefreshStatusText();
        }

        private void RefreshStatusText()
        {
            if (IsAuthenticationSupported)
            {
                View.StatusTextFieldUI.Text.text = 
                    RPGSingleton.Instance.GetStatusText();
            }
        }

        //  Event Handlers --------------------------------
        private async void Tezos_OnAccountConnected(string address)
        {
            // Required: Render UI
            await RefreshUIAsync();
            View.AuthenticationQr.IsVisible = false;
            
            
            // Tezos SDK For Unity
            // Usage: Get the active wallet address
            string activeWalletAddress = _tezos.GetActiveWalletAddress();
            Debug.Log($"You are connected to a wallet with address <b>{activeWalletAddress}</b>.");
        }
        
        
        private async void Tezos_AccountDisconnected(string address)
        {
            // Required: Render UI
            await RefreshUIAsync();
            View.AuthenticationQr.IsVisible = false;
            
            // Optional: Add any custom code here
            Debug.Log($"You are not connected to a wallet.");
        }
        
        
        private async UniTask OnAuthenticateButtonClicked()
        {
            // Required: Render UI
            await View.ShowDialogAsync("Authentication", async () =>
            {

                // Tezos SDK For Unity
                // Usage: Determines if the user is authenticated with Tezos
                if (!_tezos.HasActiveWalletAddress())
                {

                    // Tezos SDK For Unity
                    // Usage: Connect To Wallet Using The Tezos SDK For Unity
                    View.AuthenticationQr.IsVisible = true;
                    View.AuthenticationQr.ShowQrCode();
                    _tezos.ConnectWallet();
                }
                else
                {

                    // Tezos SDK For Unity
                    // Usage: Disconnect From Wallet Using The Tezos SDK For Unity
                    View.AuthenticationQr.IsVisible = false;
                    _tezos.DisconnectWallet();
                }
            });
                
            // Required: Render UI
            await RefreshUIAsync();
     
        }
        
        
        private async UniTask OnPlayGameButtonClicked()
        {
            // Required: Render UI
            await RefreshUIAsync();
            
            // Continue on to the game scene
            View.SceneController.LoadScene("Scene02_Game");
        }
    }
}

