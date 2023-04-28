using TezosAPI;
using UnityEngine;

namespace TezosSDKExamples.Shared.Tezos
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class AuthenticationQr : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;

                if (_isVisible)
                {
                    _canvasGroup.alpha = 1;
                    _canvasGroup.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                }
                else
                {
                    _canvasGroup.alpha = 0;
                    _canvasGroup.interactable = false;
                    _canvasGroup.blocksRaycasts = false;
                }
            }
        }
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private QRCodeView qrCodeView;
        
        [SerializeField]
        private GameObject logoutPanel;
        
        [SerializeField]
        private GameObject deepLinkButton;
        
        [SerializeField] 
        private GameObject qrCodePanel;

        [SerializeField] 
        private bool _isVisibleOnAwake = true;

        [SerializeField] 
        private bool _isVisibleOnAccountConnected = false;

        
        private bool _isMobile;
        private ITezosAPI _tezos;
        private string _lastHandshake = "";
        private bool _isVisible = false;
   
        //  Initialization --------------------------------
        protected void Start()
        {
        
            
#if (UNITY_IOS || UNITY_ANDROID)
		    _isMobile = true;
#else
            _isMobile = false;
#endif

            
            IsVisible = _isVisibleOnAwake;
            
            _tezos = TezosSingleton.Instance;
            _tezos.MessageReceiver.HandshakeReceived += OnHandshakeReceived;
            _tezos.MessageReceiver.AccountConnected += OnAccountConnected;
            _tezos.MessageReceiver.AccountDisconnected += OnAccountDisconnected;
        }



        //  Methods ---------------------------------------

        
        public void ShowQrCode()
        {
            EnableUI(isAuthenticated: false);
            qrCodeView.SetQrCode(_lastHandshake);
        }

        public void DisconnectWallet()
        {
            EnableUI(isAuthenticated: false);
            _tezos.DisconnectWallet();
        }

        public void ConnectByDeeplink()
        {
            _tezos.ConnectWallet();
        }

        void EnableUI(bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                deepLinkButton.SetActive(false);
                qrCodePanel.SetActive(false);
            }
            else
            {
                if (_isMobile)
                {
                    deepLinkButton.SetActive(true);
                    qrCodePanel.SetActive(false);
                }
                else
                {
                    qrCodePanel.SetActive(true);
                    deepLinkButton.SetActive(false);
                }
            }

            logoutPanel.SetActive(isAuthenticated);
        }


        //  Event Handlers --------------------------------
        
        void OnHandshakeReceived(string handshake)
        {
            _lastHandshake = handshake;
        }
        
        void OnAccountConnected(string result)
        {
            EnableUI(isAuthenticated: true);
            IsVisible = _isVisibleOnAccountConnected;
            //Debug.Log("OnAccountConnected");
        }
        
        void OnAccountDisconnected(string result)
        {
            //Debug.Log("OnAccountDisconnected");
        }

    }
}