using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Views
{
    public class CoinView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private List<GameObject> coinsPrefab;

        #endregion Serialize Fields


        #region --- Mono Methods ---

        private void Update()
        {
            foreach (GameObject coin in coinsPrefab)
            {
                if (coin != null)
                {
                    ObjectRotationService.HandleRotation(coin);
                }
            }
        }

        #endregion Mono Methods
    }
}
