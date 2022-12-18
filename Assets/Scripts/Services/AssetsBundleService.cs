using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class AssetsBundleService
    {
        #region --- Const ---

        private const string BundleUrl = "https://drive.google.com/uc?export=download&id=1aZ3oTAQMAtERJL_g7PMRHf8OXPP5OghM";

        #endregion Const
        
        
        #region --- Members ---
        
        private  AssetBundle _assetBundle;
        private Action _downloadSucceed;
        private Action<string> _downloadFailed;

        #endregion Members
        
       
        #region --- Public Methods ---

        public void DownloadBundle(Action onSuccess, Action<string> onFailed)
        {
            _downloadSucceed = onSuccess;
            _downloadFailed = onFailed;
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
                _downloadFailed?.Invoke(uwr.error);
            }
            else
            {
                _assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
                _downloadSucceed?.Invoke();
            }
        }
        
        #endregion Private Methods
    }
}