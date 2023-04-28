using RMC.Core.UI;
using TezosSDKExamples.Shared.Tezos;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS1998
namespace TezosSDKSamples.RPG.View
{
    /// <summary>
    /// View for the <see cref="Scenes.Scene01_IntroMenu"/>
    /// </summary>
    public class Scene01_IntroMenuView :  Scene_BaseView
    {
        //  Properties ------------------------------------
        public Image HeaderLogoImage { get { return _headerLogoImage;} }
        public TextFieldUI StatusTextFieldUI { get { return _statusTextFieldUI;} }
        public ButtonUI AuthenticationButtonUI { get { return _authenticationButtonUI;} }
        public ButtonUI PlayGameButtonUI { get { return _playGameButtonUI;} }
        public AuthenticationQr AuthenticationQr { get { return _authenticationQr;} }
        
        //  Fields ----------------------------------------
        [Header("Child")]
        
        [SerializeField]
        private TextFieldUI _statusTextFieldUI;
        
        [SerializeField] 
        private ButtonUI _playGameButtonUI = null;

        [SerializeField]
        private Image _headerLogoImage;

        [SerializeField]
        private ButtonUI _authenticationButtonUI;

        [SerializeField]
        private AuthenticationQr _authenticationQr;

        
        protected override async void Awake()
        {
            base.Awake();
            
            //Optional: Do something here...
        }

        
        protected override async void Start()
        {
            base.Start();
                        
            //Optional: Do something here...
        }


        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        
        
    }
}