using System;
using System.Collections;
using Controllers;
using Managers;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

public class Client : MonoBehaviour
{
    #region --- Singleton ---

    private static Client _instance;
    public static Client Instance => _instance;

    #endregion Singleton


    #region --- Const ---

    private const string ASSET_BUNDLE_SERVICE_OBJECT_NAME = "AssetBundle";
    private const string SOUND_EFFECT_MANAGER_OBJECT_NAME = "SoundEffectManager";

    #endregion Const
    
    
    #region --- Events ---

    public event Action GameStarted;
    public event Action GameEnded;
    public event Action RestartGame;

    #endregion Events
    
    
    #region --- Members ---

    private AssetsBundleService _assetsBundleService;

    private LoginController _loginController;
    private GameController _gameController;
    private PlayerController _playerController;
    
    private SoundEffectManager _soundEffectManager;

    #endregion Members
    
    
    #region --- Properties ---

    public AssetsBundleService AssetsBundleService => _assetsBundleService;
    public SoundEffectManager SoundEffectManager => _soundEffectManager;
    public LoginController LoginController => _loginController;
    public GameController GameController => _gameController;
    public PlayerController PlayerController => _playerController;

    #endregion Properties
    
    
    #region --- Mono Methods ---

    private void Awake()
    {
        Init();
        _assetsBundleService.DownloadBundle();
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
        _assetsBundleService = new AssetsBundleService();
        _soundEffectManager = GameObject.Find(SOUND_EFFECT_MANAGER_OBJECT_NAME).GetComponent<SoundEffectManager>();
        _loginController = new LoginController();
        _gameController = new GameController();
        _playerController = new PlayerController();
        TileController tileController = new TileController();
    }
    
    #endregion Private Methods
    
    
    #region --- Public Methods ---

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
    
    public void BroadcastGameStartedEvent()
    {
        GameStarted?.Invoke();
    }
    
    public void BroadcastGameEndedEvent()
    {
        GameEnded?.Invoke();
    }
    
    public void BroadcastRestartGameEvent()
    {
        RestartGame?.Invoke();
    }
    
    #endregion Public Methods
}