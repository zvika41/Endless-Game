using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class AssetsBundleService : MonoBehaviour
    {
        #region --- Serialize Fields ---

        //private string bundleUrl = "https://localhost/assetbundles";
        [SerializeField] private string bundleUrl;

        #endregion Serialize Fields
        
        
        #region --- Members ---

        private AssetBundle _assetBundle;
        private GameObject _prefab;
        private string _assetName;

        #endregion Members
        
       
        #region --- Events ---

        public event Action AssetBundleDownloadCompleted;

        #endregion Events


        #region --- Public Methods ---

        public void StartDownloading(string assetName)
        {
            StartCoroutine(DownloadingAssetBundle(assetName));
        }

        #endregion Public Methods
        
        
        #region --- Private Methods ---
        
        private IEnumerator DownloadingAssetBundle(string assetName)
        {
            using UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError || uwr.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                _assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
                _prefab = (GameObject) _assetBundle.LoadAsset(assetName);
                Instantiate(_prefab);
                _assetBundle.Unload(false);
                BroadcastAssetBundleLoadCompleted();
            }
        }
        
        private void BroadcastAssetBundleLoadCompleted()
        {
            AssetBundleDownloadCompleted?.Invoke();
        }

        #endregion Private Methods
    }
}