using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views
{
    public class GameView : MonoBehaviour, IPointerClickHandler
    {
        #region --- Serialize Fields ---

        [SerializeField] private GameObject startGameText;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text coinsAmountText;

        #endregion Serialize Fields


        #region --- Members ---

        private Action _gameStarted;
        private Action _restartGame;
        private int _coinsAmountCollected;
        private string _themeMusicName;

        #endregion Members


        #region --- Properties ---

        public Text CoinsAmountText => coinsAmountText;

        #endregion Properties
        

        #region --- Mono Methods ---

        private void Start()
        {
            gameOverPanel.SetActive(false);
            coinsAmountText.gameObject.SetActive(false);
            startGameText.SetActive(false);
            Time.timeScale = 1;
            
            startGameText.SetActive(true);
        }

        #endregion Mono Methods


        #region --- Public Methods ---

        public void SetupView(string themeMusicName, string coinText, Action onGameStart, Action onGameRestart)
        {
            _themeMusicName = themeMusicName;
            coinsAmountText.text = coinText;
            _gameStarted = onGameStart;
            _restartGame = onGameRestart;
            
            
        }
        
        public void HandleGameEnded()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion Public Methods
        
        
        #region --- Event Handler ---

        private void OnGameStarted()
        {
            _gameStarted?.Invoke();
        }
    
        #endregion Event Handler


        #region --- UI Buttons Events ---
        
        public void OnPointerClick(PointerEventData eventData)
        {
            coinsAmountText.gameObject.SetActive(true);
            startGameText.SetActive(false);
            Client.Instance.SoundEffectManager.PlaySound(_themeMusicName);
            
            OnGameStarted();
        }

        public void ReplayGame()
        {
            _restartGame?.Invoke();
        }
   
        public void QuitGame()
        {
            Destroy(gameObject);
            
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }

        #endregion UI Buttons Events 
    }
}
