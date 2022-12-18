using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LoadingScreenView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private Text loadingScreenText;

        #endregion Serialize Fields
        
        
        #region --- Public Methods ---

        public void SetupView(string loadingText)
        {
            loadingScreenText.text = loadingText;
        }
        
        public void HandleLoadingState(bool shouldEnabled)
        {
            gameObject.SetActive(shouldEnabled);
        }

        #endregion Public Methods
    }
}
