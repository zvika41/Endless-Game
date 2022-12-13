using UnityEngine;

namespace Views
{
    public class CameraView : MonoBehaviour
    {
        #region --- Members ---

        private Transform _currentTransform;
        private Transform _target;
        private Vector3 _offset;
        private Vector3 _currentPosition;
        private Vector3 _newPosition;
        private bool _isGameStarted;

        #endregion Members
    
    
        #region --- Mono Methods ---
    
        private void Start()
        {
            RegisterToCallbacks();
            _currentTransform = transform;
        }

        private void LateUpdate()
        {
            if (!_isGameStarted || _target == null) return;

            _currentPosition = _currentTransform.position;
            _newPosition = new Vector3(transform.position.x, _currentPosition.y, _offset.z + _target.position.z);
            _currentPosition = Vector3.Lerp(_currentPosition, _newPosition, 10 * Time.deltaTime);
            _currentTransform.position = _currentPosition;
        }

        private void OnDestroy()
        {
            UnRegisterFromCallbacks();
        }

        #endregion Mono Methods


        #region --- Private Methods ---

        private void ResetView()
        {
            transform.position = new Vector3(0, 7.53f, -19f);
            _target = null;
            _offset.Set(0, 0, 0);
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
            _target = Client.Instance.PlayerController.PlayerTransform;
            _offset = transform.position - _target.position;
            
            _isGameStarted = true;
        }
        
        private void OnRestartGame()
        {
            ResetView();
        }
    
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameStarted -= OnGameStarted;
            Client.Instance.RestartGame -= OnRestartGame;
        }

        #endregion Event Handler
    }
}
