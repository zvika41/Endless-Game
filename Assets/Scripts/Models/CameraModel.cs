using System;
using Singleton;
using UnityEngine;

namespace Models
{
    public class CameraModel
    {
        #region --- Members ---

        private Action _setDataDone;
        private Transform _transformTarget;

        #endregion Members
        
        
        #region --- Properties ---
        public Transform TransformTarget => _transformTarget;

        #endregion Properties
        
        
        #region --- Constructor/Deconstructor ---

        public CameraModel(Action setDataDone)
        {
            _setDataDone = setDataDone;
        }

        ~CameraModel()
        {
            ResetData();
        }

        #endregion Constructor/Deconstructor
        
        
        #region --- Public Methods ---

        public void InitData()
        {
            _transformTarget = Client.Instance.PlayerController.PlayerTransform;
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
            _transformTarget = null;
        }

        #endregion Private Methods
    }
}
