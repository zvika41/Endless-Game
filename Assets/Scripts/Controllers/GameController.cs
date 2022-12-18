using Models;
using Views;

namespace Controllers
{
    public class GameController
    {
        #region --- Members ---

        private GameModel _model;
        private GameView _gameView;
        
        #endregion Members


        #region --- Constructor/Destructor ---

        public GameController()
        {
            Client.Instance.GameSettingsDone += OnGameSettingsDone;
        }

        ~GameController()
        {
            _model = null;
            UnRegisterFromCallbacks();
        }

        #endregion Constructor/Destructor


        #region --- Private Methods ---

        private void SetupView(GameModel model)
        {
            _gameView = Client.Instance.GetLoadedBundle(model.PrefabName).GetComponent<GameView>();
            _gameView.SetupView(model.MainThemeMusicName, model.CoinsText, OnGameStarted, OnRestartGame);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---
        
        private void RegisterToCallbacks()
        {
            Client.Instance.PlayerController.CoinCollected += OnCoinCollected;
            Client.Instance.TileController.TileLoadCompleted += OnTileLoadCompleted;
            Client.Instance.GameEnded += OnGameEnded;
        }
        
        private void OnGameSettingsDone()
        {
            RegisterToCallbacks();
        }

        private void OnTileLoadCompleted()
        {
            _model = new GameModel(OnDataSetDone);
            _model.InitData();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }

        private void OnGameStarted()
        {
            Client.Instance.BroadcastGameStartedEvent();
        }

        private void OnCoinCollected()
        {
            _model.HandleCoinAmount();
            _gameView.CoinsAmountText.text = _model.CoinsText;
        }
        
        private void OnGameEnded()
        {
            _gameView.HandleGameEnded();
        }

        private void OnRestartGame()
        {
           _gameView.Destroy();
           _model.ResetData();
           Client.Instance.BroadcastRestartGameEvent();
           Client.Instance.BroadcastGameLoadStart();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameLoadStart -= OnTileLoadCompleted;
            Client.Instance.PlayerController.CoinCollected -= OnCoinCollected;
            Client.Instance.GameEnded -= OnGameEnded;
        }
        
        #endregion Event Handler
    }
}