namespace Controllers
{
    public class LoginController
    {
        #region --- Const ---

        private const string PREFAB_NAME = "LoginView";

        #endregion Const
        
        
        #region --- Constructor ---

        public LoginController()
        {
            Client.Instance.ConfigurationStateDone += OnConfigurationStateDone;
        }

        #endregion Constructor
        
        
        #region --- Event Handler ---

        private void OnConfigurationStateDone()
        {
            Client.Instance.ConfigurationStateDone -= OnConfigurationStateDone;
            Client.Instance.DownloadAssetBundle(PREFAB_NAME);
        }
    
        #endregion Event Handler
    }
}
