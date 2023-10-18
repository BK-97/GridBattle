using UnityEngine;
using UnityEngine.EventSystems;
using GridSystem.Managers;

namespace GridSystem.Controllers
{
    public class MouseController : MonoBehaviour
    {
        #region Params
        [HideInInspector]
        public Grid currentGrid;
        public Transform mousePointer;
        [SerializeField]
        private LayerMask gridLayerMask;
        [SerializeField]
        private LayerMask GroundLayerMask;

        private static GameObject takenObject;
        private static object lockObject = new object();
        #endregion
        #region MonoBehaviourMethods
        
        private void OnEnable()
        {
            InputManager.OnTap.AddListener(TapCheck);
            InputManager.OnHold.AddListener(SetPointerPos);
            InputManager.OnRelease.AddListener(TapCheck);
        }
        private void OnDisable()
        {
            InputManager.OnTap.RemoveListener(TapCheck);
            InputManager.OnHold.RemoveListener(SetPointerPos);
            InputManager.OnRelease.RemoveListener(TapCheck);
        }
#endregion
#region Methods
        private void SetPointerPos(Vector3 clickPoint)
        {
            mousePointer.transform.position = clickPoint;

        }
        private void TapCheck(Vector3 pos)
        {
            SetPointerPos(pos);

            if (CheckIfGrid())
            {
                if (currentGrid.hasObject)
                {
                    if (takenObject != null)
                    {
                        if (CheckIfUpgradable())
                        {
                            currentGrid.GetGridObject().GetComponent<WarriorController>().UpgradeWarrior();
                            CharacterManager.Instance.RemoveAlly(takenObject);
                            PoolingSystem.ReturnObjectToPool(takenObject);
                            takenObject = null;
                            return;
                        }
                    }
                    else
                    {
                        AddToPointer(currentGrid.GetGridObject());
                        currentGrid.RemoveObject();
                    }
                }
                else
                {
                    if (takenObject != null)
                    {
                        currentGrid.AddObject(takenObject);
                        SetTakenObject(null);

                    }
                }
            }

        }
        private void AddToPointer(GameObject spawnable)
        {
            if (takenObject != null)
                Destroy(takenObject);

            SetTakenObject(spawnable);
            takenObject.transform.SetParent(mousePointer);
            takenObject.transform.localPosition = Vector3.zero;
            takenObject.transform.localRotation = Quaternion.identity;
        }

#endregion
#region Helpers
        private bool CheckIfUpgradable()
        {
            WarriorController gridWarrior = currentGrid.GetGridObject().GetComponent<WarriorController>();
            WarriorController pointerWarrior = takenObject.GetComponent<WarriorController>();
            if(pointerWarrior.warriorData.CharacterType==gridWarrior.warriorData.CharacterType)
            {
                if (pointerWarrior.currentLevel == gridWarrior.currentLevel)
                    return true;
            }
            return false;
        }
        private bool CheckIfGrid()
        {
            if (currentGrid == null)
                return false;
            else
                return true;
        }

        public Grid GetMouseOverGrid()
        {
            Vector3 mousePos = mousePointer.position;

            mousePos.z = Camera.main.nearClipPlane;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, gridLayerMask))
            {
                if (hit.collider.gameObject.GetComponent<Grid>() != null)
                    return hit.collider.gameObject.GetComponent<Grid>();
                else
                    return null;
            }
            return null;
        }

        public static void SetTakenObject(GameObject objectToTake)
        {
            lock (lockObject)
            {
                takenObject = objectToTake;
            }
        }

        public static GameObject GetTakenObject()
        {
            lock (lockObject)
            {
                return takenObject;
            }
        }
#endregion
    }
}
