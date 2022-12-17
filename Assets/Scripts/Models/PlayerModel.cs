using System;

namespace Models
{
    public class PlayerModel
    {
        #region --- Members ---

        private Action _setDataDone;
        private string _prefabName;
        private float _speed;
        private float _landDistance;
        private float _jumpForce;
        private int _currentLane;
        private float _gravity;
        private float _maxSpeed;

        #endregion Members
        
        
        #region --- Properties ---

        public string PrefabName => _prefabName;
        public float Speed => _speed;
        public float LandDistance => _landDistance;
        public float JumpForce => _jumpForce;
        public int CurrentLane => _currentLane;
        public float Gravity => _gravity;
        public float MaxSpeed => _maxSpeed;

        #endregion Properties


        #region --- Constructor/Deconstructor ---

        public PlayerModel(Action setDataDone)
        {
            _setDataDone = setDataDone;
        }

        ~PlayerModel()
        {
            ResetData();
        }

        #endregion Constructor/Deconstructor


        #region --- Public Methods ---

        public void InitData()
        {
            _prefabName = "PlayerView";
            _speed = 18f;
            _landDistance = 2.4f;
            _jumpForce = 12f;
            _currentLane = 1;
            _gravity = -20;
            _maxSpeed = 36f;

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
            _speed = 0;
            _landDistance = 0;
            _jumpForce = 0;
            _currentLane = 0;
            _gravity = 0;
            _maxSpeed = 0;
        }

        #endregion Private Methods
    }
}
