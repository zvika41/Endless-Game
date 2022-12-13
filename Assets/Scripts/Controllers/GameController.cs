namespace Controllers
{
    public class GameController
    {
        #region --- Const ---

        private const string PREFAB_NAME = "GameView";

        #endregion Const


        #region --- Members ---

        private bool _isGameStarted;

        #endregion Members


        #region --- Properties ---

        public bool IsGameStarted => _isGameStarted;

        #endregion Properties
        
        
        #region --- Constructor/Destructor ---

        public GameController()
        {
            RegisterToCallbacks();
        }

        ~GameController()
        {
            UnRegisterFromCallbacks();
        }

        #endregion Constructor/Destructor
    

        #region --- Event Handler ---
        
        private void RegisterToCallbacks()
        {
            Client.Instance.StartLoadGameView += OnStartLoadGameView;
            Client.Instance.GameStarted += OnGameStarted;
            Client.Instance.RestartGame += OnRestartGame;
        }
        
        private void OnStartLoadGameView()
        {
            Client.Instance.DownloadAssetBundle(PREFAB_NAME);
        }

        private void OnGameStarted()
        {
            _isGameStarted = true;
        }
        
        private void OnRestartGame()
        {
            OnStartLoadGameView();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.StartLoadGameView -= OnStartLoadGameView;
            Client.Instance.GameStarted -= OnGameStarted;
            Client.Instance.RestartGame -= OnRestartGame;
        }
    
        #endregion Event Handler
    }
}