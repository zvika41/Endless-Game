using System;
using Controllers;
using Managers;
using Services;
using UnityEngine;

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

    public event Action ConfigurationStateDone;
    public event Action StartLoadGameView;
    public event Action CompleteLoadGameView;
    public event Action GameStarted;
    public event Action GameEnded;
    public event Action RestartGame;

    #endregion Events
    
    
    #region --- Members ---

    private LoginController _loginController;
    private GameController _gameController;
    private PlayerController _playerController;
    private AssetsBundleService _assetsBundleService;
    private SoundEffectManager _soundEffectManager;

    #endregion Members
    
    
    #region --- Properties ---

    public GameController GameController => _gameController;
    public PlayerController PlayerController => _playerController;
    public SoundEffectManager SoundEffectManager => _soundEffectManager;

    public AssetsBundleService AssetsBundleService => _assetsBundleService;

    #endregion Properties
    
    
    #region --- Mono Methods ---

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }

        _instance = this;
        
        Init();
        BroadcastConfigurationStateDoneEvent();
    }

    #endregion Mono Methods
    
    
    #region --- Private Methods ---

    private void Init()
    {
        _assetsBundleService = GameObject.Find(ASSET_BUNDLE_SERVICE_OBJECT_NAME).GetComponent<AssetsBundleService>();
        _soundEffectManager = GameObject.Find(SOUND_EFFECT_MANAGER_OBJECT_NAME).GetComponent<SoundEffectManager>();
        _loginController = new LoginController();
        _gameController = new GameController();
        _playerController = new PlayerController();
    }
    
    private void BroadcastConfigurationStateDoneEvent()
    {
        ConfigurationStateDone?.Invoke();
    }

    #endregion Private Methods
    
    
    #region --- Public Methods ---

    public void DownloadAssetBundle(string prefabName)
    {
        _assetsBundleService.StartDownloading(prefabName);
    }
    
    public void BroadcastStartLoadGameViewEvent()
    {
        StartLoadGameView?.Invoke();
    }
    
    public void BroadcastCompleteLoadGameViewEvent()
    {
        CompleteLoadGameView?.Invoke();
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