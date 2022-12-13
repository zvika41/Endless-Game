using UnityEngine;

namespace Views
{
    public class CoinView : MonoBehaviour
    {
        #region --- Const ---

        private const string PLAYER_TAG = "Player";
        private const string COLLECT_COIN_SOUND_NAME = "CollectCoin";

        #endregion Const


        #region --- Members ---

        private Transform _transform;

        #endregion Members
    
    
        #region --- Mono Methods ---

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(45 * Time.deltaTime, 0, 0);
        }

        #endregion Mono Methods
    

        #region --- Event Handler ---

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(PLAYER_TAG)) return;

            Client.Instance.PlayerController.BroadcastCoinCollectedEvent();
            Client.Instance.SoundEffectManager.PlaySound(COLLECT_COIN_SOUND_NAME, 0.30f);
            Destroy(gameObject);
        }

        #endregion Event Handler
    }
}
