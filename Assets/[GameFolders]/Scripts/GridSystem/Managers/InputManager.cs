using UnityEngine;
using UnityEngine.Events;

namespace GridSystem.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        #region Events
        public static UnityEvent OnClick = new UnityEvent();
        #endregion
        #region Methods
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick.Invoke();
            }
        }
        #endregion
    }
}
