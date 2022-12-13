using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerController
    {
        #region --- Const ---

        private const string PREFAB_NAME = "PlayerView";

        #endregion Const
        
        
        #region --- Events ---

        public event Action CoinCollected;

        #endregion Events
    

        #region --- Properties ---

        public Transform PlayerTransform { get; set; }

        #endregion Properties
    

        #region --- Constructor ---

        public PlayerController()
        {
            RegisterToCallbacks();
        }

        #endregion Constructor


        #region --- Public Methods ---

        public void BroadcastCoinCollectedEvent()
        {
            CoinCollected?.Invoke();
        }

        #endregion Public Methods
        

        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.CompleteLoadGameView += OnCompleteLoadGameView;
        }
    
        private void OnCompleteLoadGameView()
        {
            UnRegisterFromCallbacks();
            Client.Instance.DownloadAssetBundle(PREFAB_NAME);
        }
    
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameStarted -= OnCompleteLoadGameView;
        }

        #endregion Event Handler
    }
}
