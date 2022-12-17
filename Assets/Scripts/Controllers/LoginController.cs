using System;
using Models;
using Views;

namespace Controllers
{
    public class LoginController
    {
        #region --- Events ---
        public event Action LoginLoadCompleted;
        public event Action GameLoadStart;

        #endregion Events
        
        
        #region --- Members ---

        private LoginModel _model;
        private LoginView _loginView;

        #endregion Members
        
        
        #region --- Constructor/Deconstructor ---

        public LoginController()
        {
            _model = new LoginModel();
            RegisterToCallbacks();
        }

        ~LoginController()
        {
            UnRegisterFromCallbacks();
            _model = null;
        }

        #endregion Constructor/Deconstructor


        #region --- Private Methods ---

        private void SetupView()
        {
            _loginView = Client.Instance.GetLoadedBundle(_model.PrefabName).GetComponent<LoginView>();
            _loginView.SetupView(_model.ButtonName, OnLoginLoadCompleted, OnGameLoadStart);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.AssetsBundleService.AssetBundleDownloadCompleted += OnAssetBundleDownloadCompleted;
        }

        private void OnAssetBundleDownloadCompleted()
        {
           SetupView();
        }

        private void OnLoginLoadCompleted()
        {
            LoginLoadCompleted?.Invoke();
        }

        private void OnGameLoadStart()
        {
            GameLoadStart?.Invoke();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.AssetsBundleService.AssetBundleDownloadCompleted -= OnAssetBundleDownloadCompleted;
        }
    
        #endregion Event Handler
    }
}
