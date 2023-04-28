
using RMC.Core.UI;
using UnityEngine;

#pragma warning disable CS1998
namespace TezosSDKSamples.RPG.View
{
    /// <summary>
    /// View for the <see cref="Scenes.Scene02_Game"/>
    /// </summary>
    public class Scene02_GameView : Scene_BaseView
    {
        //  Properties ------------------------------------
        public ButtonUI BackButtonUI { get { return _backButtonUI;} }
        
        //  Fields ----------------------------------------
        [Header("Child")]
        
        [SerializeField] 
        private ButtonUI _backButtonUI = null;

        //  Unity Methods  --------------------------------
        protected override async void Awake()
        {
            base.Awake();
        }
        
        protected override async void Start()
        {
            base.Start();
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}