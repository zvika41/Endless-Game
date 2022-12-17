using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class AssetsBundleService
    {
        #region --- Const ---

        private const string BundleUrl = "https://drive.google.com/uc?export=download&id=1yQsWvBZZ2TXiIduAEkxFHT3i4PWPL6C9";

        #endregion Const
        
        
        #region --- Members ---
        
        private  AssetBundle _assetBundle;

        #endregion Members
        
       
        #region --- Events ---
        public event Action AssetBundleDownloadCompleted;

        #endregion Events


        #region --- Public Methods ---

        public void DownloadBundle()
        {
            Client.Instance.HandleCoroutine(DownloadingAssetBundle(BundleUrl));
        }

        public GameObject GetLoadedBundle(string assetName)
        {
            return Client.Instance.InstantiatedObject(_assetBundle.LoadAsset<GameObject>(assetName));
        }

        #endregion Public Methods
        
        
        #region --- Private Methods ---

        private IEnumerator DownloadingAssetBundle(string bundleUrl)
        {
            using UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl, 0);
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError || uwr.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                _assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
                OnDownloadCompleted();
            }
        }
        
        private void OnDownloadCompleted()
        {
            AssetBundleDownloadCompleted?.Invoke();
        }

        #endregion Private Methods
    }
}