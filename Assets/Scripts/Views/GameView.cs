using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Views
{
    public class GameView : MonoBehaviour, IPointerClickHandler
    {
        #region --- Const ---

        private const string MAIN_THEME_MUSIC_NAME = "MainTheme";
        private const string COINS_TEXT = "Coins ";

        #endregion Const
        
        
        #region --- Serialize Fields ---

        [SerializeField] private GameObject startGameText;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text coinsAmountText;

        #endregion Serialize Fields


        #region --- Members ---

        private int _coinsAmountCollected;

        #endregion Members


        #region --- Mono Methods ---

        private void Start()
        {
            coinsAmountText.gameObject.SetActive(false);
            startGameText.SetActive(true);
            Time.timeScale = 1;
        
            RegisterToCallbacks();
        }

        #endregion Mono Methods


        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.GameEnded += OnGameEnded;
            Client.Instance.PlayerController.CoinCollected += HandleCoinCollected;
        }
    
        private void HandleCoinCollected()
        {
            _coinsAmountCollected++;
            coinsAmountText.text = COINS_TEXT + _coinsAmountCollected;
        }
    
        private void OnGameEnded()
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            UnRegisterFromCallbacks();
        }

        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameEnded -= OnGameEnded;
            Client.Instance.PlayerController.CoinCollected -= HandleCoinCollected;
        }

        #endregion Event Handler


        #region --- UI Buttons Events ---
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Client.Instance.BroadcastCompleteLoadGameViewEvent();
            coinsAmountText.gameObject.SetActive(true);
            coinsAmountText.text = COINS_TEXT + _coinsAmountCollected;
            startGameText.SetActive(false);
            Client.Instance.SoundEffectManager.PlaySound(MAIN_THEME_MUSIC_NAME);
        }

        public void ReplayGame()
        {
            UnRegisterFromCallbacks();
            SceneManager.LoadScene("EndlessRunner");
        }
   
        public void QuitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #endif
        }

        #endregion UI Buttons Events 
    }
}
