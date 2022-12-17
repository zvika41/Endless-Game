using UnityEngine;

namespace Services
{
    public static class ObjectRotationService
    {
        public static void HandleRotation(GameObject go)
        {
            go.transform.Rotate(45 * Time.deltaTime, 0, 0);
        }
    }
}
