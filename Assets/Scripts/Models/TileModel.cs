using System;

namespace Models
{
    public class TileModel
    {
        #region --- Members ---

        private Action _setDataDone;
        private string _prefabName;
        private float _tileLength;
        private int _numberOfTiles;

        #endregion Members
        
        
        #region --- Properties ---

        public string PrefabName => _prefabName;
        public float TileLength => _tileLength;
        public int NumberOfTiles => _numberOfTiles;

        #endregion Properties
        
        
        #region --- Constructor/Deconstructor ---

        public TileModel(Action setDataDone)
        {
            _setDataDone = setDataDone;
        }

        ~TileModel()
        {
            ResetData();
        }

        #endregion Constructor/Deconstructor
        
        
        #region --- Public Methods ---

        public void InitData()
        {
            _prefabName = "TileView";
            _tileLength = 30;
            _numberOfTiles = 6;
            
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
            _numberOfTiles = 0;
            _tileLength = 0;
        }

        #endregion Private Methods
    }
}
