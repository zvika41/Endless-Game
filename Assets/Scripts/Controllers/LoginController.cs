using System;
using Models;
using Unity.VisualScripting;
using UnityEngine;
using Views;

namespace Controllers
{
    public class LoginController
    {
        #region --- Events ---
        
        public event Action LoginLoadCompleted;

        #endregion Events
        
        
        #region --- Members ---

        private LoginModel _model;
        private LoginView _loginView;

        #endregion Members
        
        
        #region --- Constructor/Deconstructor ---

        public LoginController()
        {
            RegisterToCallbacks();
        }

        ~LoginController()
        {
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
            Client.Instance.GameSettingsDone += OnGameSettingsDone;
        }

        private void OnGameSettingsDone()
        {
            UnRegisterFromCallbacks();
            _model = new LoginModel();
            SetupView();
        }

        private void OnLoginLoadCompleted()
        {
            LoginLoadCompleted?.Invoke();
        }

        private void OnGameLoadStart()
        {
            Client.Instance.BroadcastGameLoadStart();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameSettingsDone -= OnGameSettingsDone;
        }
    
        #endregion Event Handler
    }
}
