using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LoadingScreenView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private Text loadingScreenText;

        #endregion Serialize Fields
        
    
        #region --- Mono Methods ---

        private void Start()
        {
            RegisterToCallbacks();
            loadingScreenText.gameObject.SetActive(true);
        }
        
        private void OnDestroy()
        {
            UnRegisterFromCallbacks();
        }

        #endregion Mono Methods
        

        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.AssetsBundleService.AssetBundleDownloadCompleted += OnAssetBundleDownloadCompleted;
        }
        
        private void OnAssetBundleDownloadCompleted()
        {
            loadingScreenText.gameObject.SetActive(false);
        }
    
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.AssetsBundleService.AssetBundleDownloadCompleted -= OnAssetBundleDownloadCompleted;
        }

        #endregion Event Handler
    }
}
