using RMC.Core.Components;
using RMC.Core.UI.DialogSystem;
using RMC.Core.UI.VisualTransitions;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

#pragma warning disable CS1998
namespace TezosSDKSamples.RPG.View
{
    /// <summary>
    /// Shared functionality for the views.
    /// </summary>
    public class Scene_BaseView : MonoBehaviour
    {
        //  Properties ------------------------------------
        public IVisualTransitionTarget SceneVisualTransitionTargetPrefab { get { return _sceneVisualTransitionTargetPrefab;} }
        public VisualTransition SceneVisualTransition { get { return _sceneVisualTransition;} }
        public SceneController SceneController { get { return _sceneController;} }
        
        public DialogSystem DialogSystem { get { return _dialogSystem;} }
        
        //  Fields ----------------------------------------
        [Header("Base")]
        
        [SerializeReference]
        private VisualTransitionTarget _sceneVisualTransitionTargetPrefab;

        [SerializeField]
        private VisualTransition _sceneVisualTransition;

        [SerializeField]
        private DialogSystem _dialogSystem;
                
        private SceneController _sceneController = new SceneController();

        
        //  Unity Methods  --------------------------------
        protected virtual async void Awake()
        {
            //Setup the tweening library with defaults
            DOTween.useSafeMode = false;
            DOTween.logBehaviour = LogBehaviour.ErrorsOnly;
        }

        
        protected virtual async void Start()
        {
            // Setup system which fades between Scenes
            _sceneController.Initialize(_sceneVisualTransition, _sceneVisualTransitionTargetPrefab);
        }


        //  Methods ---------------------------------------
        
        /// <summary>
        /// Show consistent visuals for UI dialog window when "Loading..."
        /// </summary>
        public async UniTask ShowDialogAsync(
            string dialogTitle,
            System.Func<UniTask> transactionCallAsync)
        {
            // Decorate text
            DialogData dialogData = RPGConstants.CreateNewDialogData(dialogTitle);
            
            await _dialogSystem.ShowDialogAsync(
                dialogData,
                transactionCallAsync,
                async () =>
                {
                    //Optional: Do something after the dialog is closed
                });
        }

        //  Event Handlers --------------------------------
    }
}