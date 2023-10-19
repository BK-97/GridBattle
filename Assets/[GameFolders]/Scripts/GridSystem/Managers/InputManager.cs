using UnityEngine;
using UnityEngine.Events;

namespace GridSystem.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        #region Events
        public static Vector3Event OnTap = new Vector3Event();
        public static Vector3Event OnHold = new Vector3Event();
        public static Vector3Event OnRelease = new Vector3Event();
        #endregion
        public LayerMask GroundLayerMask;
        private Vector3 lastPos;
        #region Methods
        private void Update()
        {
            if (!LevelManager.Instance.IsLevelStarted)
                return;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
                {

                    lastPos = Input.mousePosition;
                    OnTap.Invoke(hit.point);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
                {

                    OnHold.Invoke(hit.point);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
                {

                    lastPos = Input.mousePosition;
                    OnRelease.Invoke(hit.point);
                }
            }
#else

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // Sadece ilk dokunmatik giriþi ele alýyoruz.

                if (touch.phase == TouchPhase.Began)
                {
                    OnTap.Invoke(touch.position);
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    OnHold.Invoke(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    OnRelease.Invoke(touch.position);
                    lastPos = touch.position;
                }

            }
#endif

        }
        #endregion
        public Vector3 GetLastInputPos()
        {
            return lastPos;
        }
    }


}
