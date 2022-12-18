using Models;
using Views;

namespace Controllers
{
    public class LoadingScreenController
    {
        #region --- Const ---

        private const string LOADING_SCREEN_VIEW_OBJECT_NAME = "LoadingScreen";

        #endregion Const
        
        
        #region --- Members ---

        private LoadingScreenModel _model;
        private LoadingScreenView _loadingScreenView;

        #endregion Members


        #region --- Constructor/Destructor ---

        public LoadingScreenController()
        {
            Client.Instance.GameSettingsDone += OnGameSettingsDone;
        }

        ~LoadingScreenController()
        {
            _model = null;
            UnRegisterFromCallbacks();
        }

        #endregion Constructor/Destructor


        #region --- Public Methods ---

        public void ParseData()
        {
            _model = new LoadingScreenModel(OnDataSetDone);
            _model.InitData();
        }

        #endregion Public Methods
        
        
        #region --- Private Methods ---

        private void SetupView(LoadingScreenModel model)
        {
            _loadingScreenView = Client.Instance.FindObjectByName<LoadingScreenView>(LOADING_SCREEN_VIEW_OBJECT_NAME);
            _loadingScreenView.SetupView(model.LoadingText);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.LoginController.LoginLoadCompleted += OnLoginLoadCompleted;
        }
        
        private void OnGameSettingsDone()
        {
            Client.Instance.GameSettingsDone -= OnGameSettingsDone;
            RegisterToCallbacks();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }

        private void OnLoginLoadCompleted()
        {
            _loadingScreenView.HandleLoadingState(false);
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.LoginController.LoginLoadCompleted -= OnLoginLoadCompleted;
        }

        #endregion Event Handler
    }
}
