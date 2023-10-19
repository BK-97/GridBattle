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
        [HideInInspector]
        public Grid previousGrid;
        public Transform mousePointer;
        [SerializeField]
        private LayerMask gridLayerMask;
        [SerializeField]
        private GameObject takenObject;
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
                        if (currentGrid == previousGrid)
                            return;
                        if (CheckIfUpgradable())
                        {
                            currentGrid.GetGridObject().GetComponent<WarriorController>().UpgradeWarrior();
                            CharacterManager.Instance.RemoveAlly(takenObject);
                            PoolingSystem.ReturnObjectToPool(takenObject);
                            takenObject = null;
                            return;
                        }
                        else
                        {
                            SwitchObjects();
                        }
                    }
                    else
                    {
                        AddToPointer(currentGrid.GetGridObject());
                        currentGrid.RemoveObject();
                        previousGrid = currentGrid;
                    }
                }
                else
                {
                    if (takenObject != null)
                    {
                        currentGrid.AddObject(takenObject);
                        takenObject = null;
                    }
                }
            }
        }
        private void SwitchObjects()
        {
            previousGrid.AddObject(currentGrid.GetGridObject());
            currentGrid.AddObject(takenObject);
            takenObject = null;
        }
        private void AddToPointer(GameObject spawnable)
        {
            if (takenObject != null)
                Destroy(takenObject);

            takenObject = spawnable;
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
            if (pointerWarrior.warriorData.CharacterType == gridWarrior.warriorData.CharacterType)
            {
                if (pointerWarrior.currentLevel == gridWarrior.currentLevel)
                    return true;
            }
            return false;
        }
        private bool CheckIfGrid()
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetLastInputPos());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, gridLayerMask))
            {
                currentGrid = hit.collider.gameObject.GetComponent<Grid>();

                if (currentGrid != null)
                {
                    return true;
                }
            }
            currentGrid = null;
            return false;
        }

        #endregion
    }
}
