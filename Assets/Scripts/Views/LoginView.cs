using System;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LoginView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private List<GameObject> coinsPrefab;
        [SerializeField] private Text button;

        #endregion Serialize Fields


        #region --- Events ---
        public event Action LoginLoadCompleted;
        public event Action GameStart;

        #endregion Events

        
        #region --- Mono Methods ---

        private void Update()
        {
            HandleCoinRotation();
        }

        #endregion Mono Methods
        
        
        #region --- Public Methods ---

        public void SetupView(string buttonText, Action onViewReady, Action onGameStart)
        {
            button.text = buttonText;
            LoginLoadCompleted = onViewReady;
            GameStart = onGameStart;
            OnLoginLoadCompleted();
        }

        #endregion Public Methods


        #region --- Private Methods ---

        private void HandleCoinRotation()
        {
            foreach (GameObject coin in coinsPrefab)
            {
                ObjectRotationService.HandleRotation(coin);
            }
        }

        #endregion Private Methods
        
        
        #region --- UI Button Events ---

        public void OnPlayButtonClicked()
        {
            OnGameLoadStarted();
            Destroy(gameObject);
        }

        #endregion UI Button Events


        #region --- Events Handler ---

        private void OnLoginLoadCompleted()
        {
            LoginLoadCompleted?.Invoke();
        }

        private void OnGameLoadStarted()
        {
            GameStart?.Invoke();
        }

        #endregion Events Handler
    }
}
