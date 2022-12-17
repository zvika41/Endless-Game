using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LoadingScreenView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private Text loadingScreenText;

        #endregion Serialize Fields
        
    
        #region --- Mono Methods ---

        private void Start()
        {
            RegisterToCallbacks();
            loadingScreenText.gameObject.SetActive(true);
        }
        
        private void OnDestroy()
        {
            UnRegisterFromCallbacks();
        }

        #endregion Mono Methods
        

        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.LoginController.LoginLoadCompleted += OnLoginLoadCompleted;
            Client.Instance.LoginController.GameLoadStart += OnGameLoadStarted;
            Client.Instance.GameController.GameViewLoadCompleted += OnGameViewLoadCompleted;
        }
        
        private void OnLoginLoadCompleted()
        {
            loadingScreenText.gameObject.SetActive(false);
        }
        
        private void OnGameLoadStarted()
        {
            loadingScreenText.gameObject.SetActive(true);
        }
        
        private void OnGameViewLoadCompleted()
        {
            loadingScreenText.gameObject.SetActive(false);
        }
    
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.LoginController.LoginLoadCompleted -= OnLoginLoadCompleted;
            Client.Instance.LoginController.GameLoadStart -= OnGameLoadStarted;
            Client.Instance.GameController.GameViewLoadCompleted -= OnGameViewLoadCompleted;
        }

        #endregion Event Handler
    }
}
