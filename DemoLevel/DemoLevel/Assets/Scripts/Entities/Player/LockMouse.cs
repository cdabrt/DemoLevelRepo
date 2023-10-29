using UnityEngine;

namespace Player
{
    public class LockMouse : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
