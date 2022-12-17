using System;
using Models;
using Views;

namespace Controllers
{
    public class GameController
    {
        #region --- Events ---
        public event Action GameViewLoadCompleted;

        #endregion Events
        
        
        #region --- Members ---

        private GameView _gameView;
        private GameModel _model;

        #endregion Members


        #region --- Constructor/Destructor ---

        public GameController()
        {
            _model = new GameModel(OnDataSetDone);
            RegisterToCallbacks();
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
            _gameView = Client.Instance.GetLoadedBundle(_model.PrefabName).GetComponent<GameView>();
            _gameView.SetupView(model.MainThemeMusicName, model.CoinsText, OnGameViewLoadCompleted, OnGameStarted, OnRestartGame);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---
        
        private void RegisterToCallbacks()
        {
            Client.Instance.LoginController.GameLoadStart += OnGameLoadStart;
            Client.Instance.GameEnded += OnGameEnded;
            
        }

        private void OnGameLoadStart()
        {
            _model.InitData();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }
        
        private void OnGameViewLoadCompleted()
        {
            GameViewLoadCompleted?.Invoke();
        }

        private void OnGameStarted()
        {
            Client.Instance.PlayerController.CoinCollected += OnCoinCollected;
            Client.Instance.BroadcastGameStartedEvent();
        }

        private void OnCoinCollected()
        {
            _model.HandleCoinAmount(false);
            _gameView.CoinsAmountText.text = _model.CoinsText;
        }
        
        private void OnGameEnded()
        {
            _gameView.HandleGameEnded();
        }

        private void OnRestartGame()
        {
            _model.HandleCoinAmount(true);
            Client.Instance.BroadcastRestartGameEvent();
            OnGameLoadStart();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.LoginController.GameLoadStart -= OnGameLoadStart;
            Client.Instance.PlayerController.CoinCollected -= OnCoinCollected;
            Client.Instance.GameEnded -= OnGameEnded;
        }
        
        #endregion Event Handler
    }
}