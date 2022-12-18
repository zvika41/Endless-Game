using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

namespace Managers
{
    public class SwipeManager : MonoBehaviour
    {
        #region --- Members ---

        private static bool _swipeLeft, _swipeRight, _swipeUp, _swipeDown;

        private bool _isDragging;
        private Vector2 _startTouch, _swipeDelta;
        private float _xDirection;
        private float _yDirection;

        #endregion Members


        #region --- Properties ---

        public static bool SwipeLeft => _swipeLeft;
        public static bool SwipeRight => _swipeRight;
        public static bool SwipeUp => _swipeUp;
        public static bool SwipeDown => _swipeDown;

        #endregion Properties
    

        #region --- Mono Methods ---

        private void Update()
        {
            if(!Client.Instance.IsGameStarted) return;
            
            if (_swipeLeft || _swipeRight || _swipeUp || _swipeDown)
            {
                _swipeLeft = _swipeRight = _swipeUp =  _swipeDown = false;
            }

            #if UNITY_EDITOR
            StandaloneInputs();
            #endif
            
            MobileInput();
            HandleDistanceCalculate();
            HandleDistanceCrossed();
        }

        #endregion Mono Methods
    
    
        #region --- Private Methods ---

        private void StandaloneInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                Reset();
            }
        }

        private void MobileInput()
        {
            if (Input.touches.Length <= 0) return;
            
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }

        private void HandleDistanceCalculate()
        {
            _swipeDelta = Vector2.zero;

            if (!_isDragging) return;
        
            if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }

        private void HandleDistanceCrossed()
        {
            if (!(_swipeDelta.magnitude > 100)) return;
        
            //Which direction?
            _xDirection = _swipeDelta.x;
            _yDirection = _swipeDelta.y;
        
            if (Mathf.Abs(_xDirection) > Mathf.Abs(_yDirection))
            {
                //Left or Right
                if (_xDirection < 0)
                {
                    _swipeLeft = true;
                }
                else
                {
                    _swipeRight = true;
                }
            }
            else
            {
                //Up or Down
                if (_yDirection < 0)
                {
                    _swipeDown = true;
                }
                else
                {
                    _swipeUp = true;
                }
            }

            Reset();
        }

        private void Reset()
        {
            _startTouch = _swipeDelta = Vector2.zero;
            _isDragging = false;
        }

        #endregion Private Methods
    }
}