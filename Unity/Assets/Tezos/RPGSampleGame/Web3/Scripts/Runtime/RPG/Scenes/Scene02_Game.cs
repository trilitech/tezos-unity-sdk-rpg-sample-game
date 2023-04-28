using UnityEngine;
using Cysharp.Threading.Tasks;
using RPGM.Gameplay;
using RPGM.UI;
using TezosSDKSamples.RPG.Objects;
using TezosSDKSamples.RPG.View;

#pragma warning disable CS4014, CS1998
namespace TezosSDKSamples.RPG.Scenes
{
    /// <summary>
    /// Controller for <see cref="View.Scene02_GameView"/>
    /// </summary>
    public class Scene02_Game : MonoBehaviour
    {
        //  Properties ------------------------------------

        
        //  Fields ----------------------------------------
        [Header("UI")]
        [SerializeField] 
        private Scene02_GameView _view;
        
        [Header("Gameplay")]
        [SerializeField] 
        private RockObstacle _rockObstacle = null;

        [SerializeField] 
        private InventoryItem _bonusInventoryItem = null;
        
        [SerializeField] 
        private float _bonusWalkingSpeed = 1.5f;

        [SerializeField] 
        private float _normalWalkingSpeed = 1f;

        
        //  Unity Methods  --------------------------------
        protected virtual async void Start()
        {
            // Observe Events
            _view.BackButtonUI.Button.onClick.AddListener(() => OnBackGameButtonClicked());
            
            // Header
            _view.BackButtonUI.Text.text = "Back";
            
            // Refresh
            await RefreshUIAsync();

            
            // Tezos SDK For Unity
            // Usage: Determines if the user has the required NFT
            // This value is SET in the <see cref="Scene01_IntroMenu"/>
            if (RPGSingleton.Instance.HasRequiredNFT)
            {
                if (!RPGSingleton.Instance.HasInventoryItem(_bonusInventoryItem))
                {
                    // Gives map shortcut to player character
                    _rockObstacle.IsVisible = false;
                    
                    //Add bonus item
                    RPGSingleton.Instance.AddInventoryItem(_bonusInventoryItem);

                    //Add bonus message
                    string message = $"<b>{RPGSingleton.Instance.PlayerName}</b> begins with the '{_bonusInventoryItem.name}'.";
                    MessageBar.Show(message);
                }
            }
            else
            {
                //Remove bonus item
                if (RPGSingleton.Instance.HasInventoryItem(_bonusInventoryItem))
                {
                    RPGSingleton.Instance.RemoveInventoryItem(_bonusInventoryItem);
                }
            }
            
            
            
            // Tezos SDK For Unity
            // Usage Determines if the user is authenticated via Tezos
            // This value is SET in the <see cref="Scene01_IntroMenu"/>
            if (RPGSingleton.Instance.IsAuthenticated)
            {
                //Set bonus attribute
                RPGSingleton.Instance.WalkingSpeed = _bonusWalkingSpeed;
                
                //Set bonus message
                string message = $"<b>{RPGSingleton.Instance.PlayerName}</b> begins with 'walking speed' of {_bonusWalkingSpeed}.";
                MessageBar.Show(message);
                
            }
            else
            {
                //Set normal attribute
                RPGSingleton.Instance.WalkingSpeed = _normalWalkingSpeed;
            }
        }


        //  Methods ---------------------------------------
        protected virtual async UniTask RefreshUIAsync()
        {
            //Optional: Add refresh code as needed
        }

        
        //  Event Handlers --------------------------------
        private async UniTask OnBackGameButtonClicked()
        {
            
            await RefreshUIAsync();
            
            _view.SceneController.LoadScene("Scene01_IntroMenu");
        }
    }
}

