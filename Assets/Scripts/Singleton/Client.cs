using System;
using System.Collections;
using Controllers;
using Managers;
using Services;
using UnityEngine;

namespace Singleton
{
    public class Client : MonoBehaviour
    {
        #region --- Singleton ---

        private static Client _instance;
        public static Client Instance => _instance;

        #endregion Singleton


        #region --- Const ---

        private const string SOUND_EFFECT_MANAGER_OBJECT_NAME = "SoundEffectManager";

        #endregion Const
    
    
        #region --- Events ---

        public event Action GameSettingsDone;
        public event Action GameLoadStart;
        public event Action GameStarted;
        public event Action GameEnded;
        public event Action RestartGame;

        #endregion Events
    
    
        #region --- Members ---

        private AssetsBundleService _assetsBundleService;

        private LoadingScreenController _loadingScreenController;
        private CameraController _cameraController;
        private LoginController _loginController;
        private TileController _tileController;
        private GameController _gameController;
        private PlayerController _playerController;

        private SoundEffectManager _soundEffectManager;

        #endregion Members
    
    
        #region --- Properties ---

        public SoundEffectManager SoundEffectManager => _soundEffectManager;
        public LoginController LoginController => _loginController;
        public TileController TileController => _tileController;
        public PlayerController PlayerController => _playerController;
    
        public bool IsGameStarted { get; private set; }

        #endregion Properties
    
    
        #region --- Mono Methods ---

        private void Awake()
        {
            Init();
        }

        #endregion Mono Methods
    
    
        #region --- Private Methods ---

        private void Init()
        {
            if (_instance != null)
            {
                Destroy(_instance);
            }

            _instance = this;
            _loadingScreenController = new LoadingScreenController();
            _loadingScreenController.ParseData();
            _assetsBundleService = new AssetsBundleService();
            _assetsBundleService.DownloadBundle(OnAssetBundleDownloadCompleted, OnAssetBundleDownloadFailed);
        }

        #endregion Private Methods
    
    
        #region --- Event Handler ---

        private void OnAssetBundleDownloadCompleted()
        {
            _soundEffectManager = FindObjectByName<SoundEffectManager>(SOUND_EFFECT_MANAGER_OBJECT_NAME);

            _loginController = new LoginController();
            _gameController = new GameController();
            _playerController = new PlayerController();
            _tileController = new TileController();
            _cameraController = new CameraController();
        
            OnGameSettingsDone();
        }
    
        private void OnAssetBundleDownloadFailed(string errorReason)
        {
            // Add error popup
            Debug.LogError(errorReason);
        }
    
        private void OnGameSettingsDone()
        {
            GameSettingsDone?.Invoke();
        }

        #endregion Event Handler
    
    
        #region --- Public Methods ---

        public T FindObjectByName<T>(string objectName) where T : MonoBehaviour
        {
            return GameObject.Find(objectName).GetComponent<T>();
        }

        public GameObject InstantiatedObject(GameObject go)
        {
            return Instantiate(go);
        }

        public void HandleCoroutine(IEnumerator methodName)
        {
            StartCoroutine(methodName);
        }

        public GameObject GetLoadedBundle(string prefabName)
        {
            return _assetsBundleService.GetLoadedBundle(prefabName);
        }

        public void BroadcastGameLoadStart()
        {
            GameLoadStart?.Invoke();
        }
    
        public void BroadcastGameStartedEvent()
        {
            IsGameStarted = true;
            GameStarted?.Invoke();
        }
    
        public void BroadcastGameEndedEvent()
        {
            IsGameStarted = false;
            GameEnded?.Invoke();
        }
    
        public void BroadcastRestartGameEvent()
        {
            RestartGame?.Invoke();
        }
    
        #endregion Public Methods
    }
}