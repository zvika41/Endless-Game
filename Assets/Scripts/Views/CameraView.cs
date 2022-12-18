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

        #endregion Members
    
    
        #region --- Mono Methods ---
    
        private void Start()
        {
            _currentTransform = transform;
        }

        private void Update()
        {
            if (!Client.Instance.IsGameStarted || _target == null) return;

            HandleCameraPosition();
        }

        #endregion Mono Methods


        #region --- Public Methods ---
        
        public void SetupView(Transform target)
        {
            _target = target;
            _offset = transform.position - _target.position;
        }

        public void ResetView()
        {
            transform.position = new Vector3(0, 7.53f, -19f);
            _target = null;
            _offset.Set(0, 0, 0);
        }

        #endregion Public Methods
        
        
        #region --- Private Methods ---

        private void HandleCameraPosition()
        {
            _currentPosition = _currentTransform.position;
            _newPosition = new Vector3(transform.position.x, _currentPosition.y, _offset.z + _target.position.z);
            _currentPosition = Vector3.Lerp(_currentPosition, _newPosition, 10 * Time.deltaTime);
            _currentTransform.position = _currentPosition;
        }

        #endregion Private Methods
    }
}
