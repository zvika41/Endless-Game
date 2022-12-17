using Models;
using Views;

namespace Controllers
{
    public class TileController
    {
        #region --- Members ---

        private TileView _tileView;
        private TileModel _model;

        #endregion Members
        
        
        #region --- Constructor/Destructor ---

        public TileController()
        {
            _model = new TileModel(OnDataSetDone);
            RegisterToCallbacks();
        }

        ~TileController()
        {
            _model = null;
            UnRegisterFromCallbacks();
        }

        #endregion Constructor/Destructor
        
        
        #region --- Private Methods ---

        private void SetupView(TileModel model)
        {
            _tileView = Client.Instance.GetLoadedBundle(model.PrefabName).GetComponent<TileView>();
            _tileView.SetupView(_model.TileLength, _model.NumberOfTiles);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---
        
        private void RegisterToCallbacks()
        {
            Client.Instance.GameStarted += OnGameStarted;
            Client.Instance.RestartGame += OnRestartGame;
        }
        
        private void OnGameStarted()
        {
            _model.InitData();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }

        private void OnRestartGame()
        {
            _tileView.RestartGame();
        }

        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameStarted -= OnGameStarted;
            Client.Instance.RestartGame -= OnRestartGame;
        }
        
        #endregion Event Handler
    }
}
