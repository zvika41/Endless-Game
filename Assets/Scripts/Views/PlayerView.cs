using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        #region --- Const ---

        private static readonly int IsGameStarted = Animator.StringToHash("IsGameStarted");
        private static readonly int IsPlayerJumping = Animator.StringToHash("IsPlayerJumping");
        private static readonly int Slide1 = Animator.StringToHash("Slide");

        private const string OBSTACLE_GAME_OBJECT_NAME = "Obstacle";
        private const string GAME_OVER_SOUND_NAME = "GameOver";
        private const string MAIN_THEME_SOUND_NAME = "MainTheme";
        private const string COLLECT_COIN_SOUND_NAME = "CollectCoin";

        #endregion Const
    
    
        #region --- Serialize Fields ---
        
        [SerializeField] private Animator animator;

        #endregion Serialize Fields


        #region --- Members ---

        private Action _coinCollected;
        private Action _gameOver;
        private CharacterController _characterController;
        private SoundEffectManager _soundEffectManager;
        private Transform _localTransform;
        private Vector3 _playerDirection;
        private Vector3 _playerCurrentPos;
        private Vector3 _position;
        private Vector3 _diff;
        private Vector3 _movingDirection;
        private float _speed;
        private float _laneDistance; //distance between 2 lanes
        private float _jumpForce;
        private int _currentLane;
        private float _gravity;
        private float _maxSpeed;
        private bool _shouldStartGame;
        private bool _isPlayerSliding;

        #endregion Members


        #region --- Mono Methods ---

        private void Update()
        {
            if(!_shouldStartGame) return;
            
            if (_speed < _maxSpeed)
            {
                _speed += 0.1f * Time.deltaTime;
            }

            if (!animator.GetBool(IsGameStarted))
            {
                animator.SetBool(IsGameStarted, true);
            }

            HandlePlayerMovement();
        }

        #endregion Mono Methods


        #region --- Public Methods ---

        public void SetupView(float speedValue, float landDistanceValue, float jumpForceValue, int currentLane, float gravity, float maxSpeed, Action onCoinCollected,  Action onGameOver)
        {
            _speed = speedValue;
            _laneDistance = landDistanceValue;
            _jumpForce = jumpForceValue;
            _coinCollected = onCoinCollected;
            _gameOver = onGameOver;
            _currentLane = currentLane;
            _gravity = gravity;
            _maxSpeed = maxSpeed;
            _localTransform = transform;
            Client.Instance.PlayerController.PlayerTransform = _localTransform;
            _characterController = GetComponent<CharacterController>();
            _soundEffectManager = Client.Instance.SoundEffectManager;
            
           _shouldStartGame = true;
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion Public Methods
        
        
        #region --- Private Methods ---

        private void HandlePlayerMovement()
        {
            _playerDirection.z = _speed;

            if (_characterController.isGrounded && SwipeManager.SwipeUp)
            {
                animator.SetBool(IsPlayerJumping, true);
                _playerDirection.y = -1;
                Jump();
            }
            else
            {
                if (!_characterController.isGrounded)
                {
                    _playerDirection.y += _gravity * Time.deltaTime;
                }
            
                if (animator.GetBool(IsPlayerJumping))
                {
                    animator.SetBool(IsPlayerJumping, false);
                }
            }
        
            if (_characterController.isGrounded && SwipeManager.SwipeDown && !_isPlayerSliding)
            {
                StartCoroutine(Slide());
            }
        
            if (SwipeManager.SwipeRight)
            {
                _currentLane++;
            
                if (_currentLane == 3)
                {
                    _currentLane = 2;
                }
            }
        
            if (SwipeManager.SwipeLeft)
            {
                _currentLane--;
        
                if (_currentLane == -1)
                {
                    _currentLane = 0;
                }
            }

            HandlePlayerCurrentPosition();
            MoveForward();
        }

        private void MoveForward()
        {
            _characterController.Move(_playerDirection * Time.deltaTime);
        }

        private void Jump()
        {
            _playerDirection.y = _jumpForce;
        }

        private IEnumerator Slide()
        {
            _isPlayerSliding = true;
            _characterController.center = new Vector3(0, -0.5f, 0);
            _characterController.height = 1;
            animator.SetTrigger(Slide1);
            yield return new WaitForSeconds(1f);
        
            _characterController.center = new Vector3(0, 0, 0);
            _characterController.height = 2;
            _isPlayerSliding = false;
        }
    
        private void HandlePlayerCurrentPosition()
        {
            _position = _localTransform.position;
            _playerCurrentPos = _position.z * _localTransform.forward + _position.y * _localTransform.up;

            if (_currentLane == 0)
            {
                _playerCurrentPos += Vector3.left * _laneDistance;
            }
            else if (_currentLane == 2)
            {
                _playerCurrentPos += Vector3.right * _laneDistance;
            }
        
            if (_localTransform.position == _playerCurrentPos) return;

            _diff  = _playerCurrentPos - _localTransform.position;
            _movingDirection = _diff.normalized * 25 * Time.deltaTime;

            if (_movingDirection.sqrMagnitude < _diff.sqrMagnitude)
            {
                _characterController.Move(_movingDirection);
            }
            else
            {
                _characterController.Move(_diff);
            }
        }

        #endregion Private Methods


        #region --- Event Handler ---

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.transform.CompareTag(OBSTACLE_GAME_OBJECT_NAME)) return;
            
            _shouldStartGame = false;
            animator.SetBool(IsGameStarted, false);
            _soundEffectManager.PlaySound(GAME_OVER_SOUND_NAME, 0.60f);
            _soundEffectManager.StopSound(MAIN_THEME_SOUND_NAME);
            
            OnGameOver();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnCoinCollected();
            Client.Instance.SoundEffectManager.PlaySound(COLLECT_COIN_SOUND_NAME, 0.30f);
            Destroy(other.gameObject);
        }
        
        private void OnCoinCollected()
        {
            _coinCollected?.Invoke();
        }

        private void OnGameOver()
        {
            _gameOver?.Invoke();
        }
    
        #endregion Event Handler
    }
}