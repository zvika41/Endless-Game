using UnityEngine;

namespace Views
{
    public class LoginView : MonoBehaviour
    {
        #region --- UI Button Events ---

        public void OnPlayButtonClicked()
        {
            Client.Instance.BroadcastStartLoadGameViewEvent();
            Destroy(gameObject);
        }

        #endregion UI Button Events
    }
}
