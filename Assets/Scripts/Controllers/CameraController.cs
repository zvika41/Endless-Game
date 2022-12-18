using Models;
using Views;

namespace Controllers
{
    public class CameraController
    {
        #region --- Const ---

        private const string CAMERA_VIEW_OBJECT_NAME = "Main Camera";

        #endregion Const
        
        
        #region --- Members ---

        private CameraModel _model;
        private CameraView _cameraView;

        #endregion Members


        #region --- Constructor/Destructor ---

        public CameraController()
        {
            Client.Instance.GameSettingsDone += OnGameSettingsDone;
        }

        ~CameraController()
        {
            _model = null;
            UnRegisterFromCallbacks();
        }

        #endregion Constructor/Destructor
        
        
        #region --- Private Methods ---

        private void SetupView(CameraModel model)
        {
            _cameraView = Client.Instance.FindObjectByName<CameraView>(CAMERA_VIEW_OBJECT_NAME);
            _cameraView.SetupView(model.TransformTarget);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.PlayerController.PlayerLoadCompleted += OnPlayerLoadCompleted;
            Client.Instance.RestartGame += OnRestartGame;
        }
        
        private void OnGameSettingsDone()
        {
            Client.Instance.GameSettingsDone -= OnGameSettingsDone;
            
            RegisterToCallbacks();
            
        }
        
        private void OnPlayerLoadCompleted()
        {
            _model = new CameraModel(OnDataSetDone);
            _model.InitData();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }

        private void OnRestartGame()
        {
            _cameraView.ResetView();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.RestartGame -= OnRestartGame;
        }

        #endregion Event Handler
    }
}
