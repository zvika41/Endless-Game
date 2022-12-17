namespace Models
{
    public class LoginModel
    {
        #region --- Members ---

        private string _prefabName;
        private string _buttonName;
        

        #endregion Members
        
        
        #region --- Properties ---

        public string PrefabName => _prefabName;
        public string ButtonName => _buttonName;

        #endregion Properties


        #region --- Constructor/Deconstructor ---

        public LoginModel()
        {
            _prefabName = "LoginView";
            _buttonName = "Login";
        }

        ~LoginModel()
        {
            _prefabName = null;
            _buttonName = null;
        }

        #endregion Constructor/Deconstructor
    }
}