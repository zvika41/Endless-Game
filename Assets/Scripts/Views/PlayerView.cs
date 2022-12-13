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

        #endregion Const
    
    
        #region --- Serialize Fields ---

        [SerializeField] private GameObject player;
        [SerializeField] private float speed;
        [SerializeField] private float laneDistance; //distance between 2 lanes
        [SerializeField] private float jumpForce;
        [SerializeField] private Animator animator;

        #endregion Serialize Fields


        #region --- Members ---

        private CharacterController _characterController;
        private SoundEffectManager _soundEffectManager;
        private Transform _localTransform;
        private Vector3 _playerDirection;
        private Vector3 _playerCurrentPos;
        private Vector3 _position;
        private Vector3 _diff;
        private Vector3 _movingDirection;
        private int _currentLane;
        private float _gravity;
        private float _maxSpeed;
        private bool _isGameStarted;
        private bool _isPlayerSliding;

        #endregion Members

    
        #region --- Properties ---

        public GameObject Player => player;

        #endregion Properties
    
   
        #region --- Mono Methods ---

        private void Start()
        {
            RegisterToCallbacks();

            Client.Instance.PlayerController.PlayerTransform = transform;
            _characterController = GetComponent<CharacterController>();
            _soundEffectManager = Client.Instance.SoundEffectManager;
            _localTransform = transform;
            _currentLane = 1;
            _gravity = -20;
            _maxSpeed = 36;
            _isGameStarted = true;
            
            Client.Instance.BroadcastGameStartedEvent();
        }

        private void Update()
        {
            if(!_isGameStarted) return;
        
            if (speed < _maxSpeed)
            {
                speed += 0.1f * Time.deltaTime;
            }

            if (!animator.GetBool(IsGameStarted))
            {
                animator.SetBool(IsGameStarted, true);
            }

            HandlePlayerMovement();
            
        }

        private void FixedUpdate()
        {
            if(!_isGameStarted) return;
        
            
        }
    
        #endregion Mono Methods

    
        #region --- Private Methods ---

        private void HandlePlayerMovement()
        {
            _playerDirection.z = speed;

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
            _playerDirection.y = jumpForce;
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
                _playerCurrentPos += Vector3.left * laneDistance;
            }
            else if (_currentLane == 2)
            {
                _playerCurrentPos += Vector3.right * laneDistance;
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

        private void RegisterToCallbacks()
        {
            Client.Instance.GameStarted += OnGameStarted;
            Client.Instance.RestartGame += OnRestartGame;
        }

        private void OnGameStarted()
        {
            _isGameStarted = true;
            Client.Instance.GameStarted -= OnGameStarted;
        
        }
        
        private void OnRestartGame()
        {
            Client.Instance.RestartGame -= OnRestartGame;
            Destroy(gameObject);
        }
    
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.transform.CompareTag(OBSTACLE_GAME_OBJECT_NAME)) return;
        
            Client.Instance.BroadcastGameEndedEvent();
            _isGameStarted = false;
            animator.SetBool(IsGameStarted, false);
            _soundEffectManager.PlaySound(GAME_OVER_SOUND_NAME, 0.60f);
            _soundEffectManager.StopSound(MAIN_THEME_SOUND_NAME);
        }
    
        #endregion Event Handler
    }
}