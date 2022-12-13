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
        
        
        #region --- Constructor ---

        public GameController()
        {
            RegisterToCallbacks();
        }

        #endregion Constructor
    

        #region --- Event Handler ---
        
        private void RegisterToCallbacks()
        {
            Client.Instance.StartLoadGameView += OnStartLoadGameView;
            Client.Instance.GameStarted += OnGameStarted;
        }
        
        private void OnStartLoadGameView()
        {
            Client.Instance.StartLoadGameView -= OnStartLoadGameView;
            Client.Instance.DownloadAssetBundle(PREFAB_NAME);
        }

        private void OnGameStarted()
        {
            Client.Instance.GameStarted -= OnGameStarted;
            _isGameStarted = true;
        }
    
        #endregion Event Handler
    }
}