using System;

namespace Models
{
    public class GameModel
    {
        #region --- Members ---

        private Action _setDataDone;
        private string _prefabName;
        private string _mainThemeMusicName;
        private string _coinsText;
        private int _coinsAmountCollected;

        #endregion Members
        
        
        #region --- Properties ---

        public string PrefabName => _prefabName;
        public string MainThemeMusicName => _mainThemeMusicName;
        public string CoinsText { get; private set; }

        #endregion Properties
        
        
        #region --- Constructor/Deconstructor ---

        public GameModel(Action setDataDone)
        {
            _setDataDone = setDataDone;
        }

        ~GameModel()
        {
           ResetData();
        }

        #endregion Constructor/Deconstructor
        
        
        #region --- Public Methods ---

        public void InitData()
        {
            _prefabName = "GameView";
            _mainThemeMusicName = "MainTheme";
            _coinsText =  "Coins: ";
            
            SetCurrentCoinsAmount();
            HandleSetDataDone();
        }
        
        public void HandleCoinAmount(bool shouldReset)
        {
            if (shouldReset)
            {
                _coinsAmountCollected = 0;
                return;
            }
            
            _coinsAmountCollected++;
            SetCurrentCoinsAmount();
        }
        
        #endregion Public Methods
        
        
        #region --- Private Methods ---
        
        private void SetCurrentCoinsAmount()
        {
            CoinsText = _coinsText + _coinsAmountCollected;
        }

        private void HandleSetDataDone()
        {
            _setDataDone?.Invoke();
        }
        
        private void ResetData()
        {
            _setDataDone = null;
            _prefabName = null;
            _mainThemeMusicName = null;
            _coinsText = null;
            _coinsAmountCollected = 0;
        }

        #endregion Private Methods
    }
}
