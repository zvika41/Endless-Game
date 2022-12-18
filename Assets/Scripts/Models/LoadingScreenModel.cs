using System;

namespace Models
{
    public class LoadingScreenModel
    {
        #region --- Members ---

        private Action _setDataDone;
        private string _prefabName;
        private string _loadingText;

        #endregion Members
        
        
        #region --- Properties ---

        public string PrefabName => _prefabName;
        public string LoadingText => _loadingText;

        #endregion Properties
        
        
        #region --- Constructor/Deconstructor ---

        public LoadingScreenModel(Action setDataDone)
        {
            _setDataDone = setDataDone;
        }

        ~LoadingScreenModel()
        {
            ResetData();
        }

        #endregion Constructor/Deconstructor
        
        
        #region --- Public Methods ---

        public void InitData()
        {
            _prefabName = "LoadingScreenView";
            _loadingText = "Loading...";

            HandleSetDataDone();
        }
        
        #endregion Public Methods
        
        
        #region --- Private Methods ---
        
        private void HandleSetDataDone()
        {
            _setDataDone?.Invoke();
        }
        
        private void ResetData()
        {
            _setDataDone = null;
            _prefabName = null;
            _loadingText = null;
        }

        #endregion Private Methods
    }
}
