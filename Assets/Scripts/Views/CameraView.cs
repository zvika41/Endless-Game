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
    
        #endregion Mono Methods
    
    
        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.GameStarted += OnGameStarted;
        }
    
        private void OnGameStarted()
        {
            UnRegisterFromCallbacks();
            _isGameStarted = true;
            _target = Client.Instance.PlayerController.PlayerTransform;
            _offset = transform.position - _target.position;
        }
    
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameStarted -= OnGameStarted;
        }

        #endregion Event Handler
    }
}
