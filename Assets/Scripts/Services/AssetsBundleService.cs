using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class AssetsBundleService : MonoBehaviour
    {
        //private string bundleUrl = "https://localhost/assetbundles";
        [SerializeField] private string bundleUrl;
        
        private AssetBundle _assetBundle;
        private GameObject _prefab;
        private string _assetName;

        public void StartNewCoroutine(string assetName)
        {
            StartCoroutine(StartDownloading(assetName));
        }

        private IEnumerator StartDownloading(string assetName)
        {
            using UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
            yield return uwr.SendWebRequest();
 
            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError || uwr.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
                _prefab = (GameObject) assetBundle.LoadAsset(assetName);
                Instantiate(_prefab);
                assetBundle.Unload(false);
            }
        }
    }
}