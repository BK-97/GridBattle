using UnityEngine;
using UnityEngine.EventSystems;
using GridSystem.Managers;

namespace GridSystem.Controllers
{
    public class MouseController : MonoBehaviour
    {
        #region Params
        [SerializeField]
        private InputManager inputManager;
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
        private void Update()
        {
            Grid gridGet = GetMouseOverGrid();
            if (gridGet != null)
            {
                currentGrid = gridGet;
            }
            else
            {
                currentGrid = null;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue,GroundLayerMask))
            {
                mousePointer.position = hit.point;
            }

        }
        private void OnEnable()
        {
            InputManager.OnClick.AddListener(ClickOn);
            CharacterManager.OnSpawnObject.AddListener(AddToPointer);
        }
        private void OnDisable()
        {
            InputManager.OnClick.RemoveListener(ClickOn);
            CharacterManager.OnSpawnObject.RemoveListener(AddToPointer);
        }
        #endregion
        #region Methods
        private void ClickOn()
        {
            if (CheckIfGrid())
            {
                if (currentGrid.hasObject)
                {
                    if (takenObject != null)
                    {
                        if(CheckIfUpgradable())
                        {
                            currentGrid.gridObject.GetComponent<WarriorController>().UpgradeWarrior();
                            Destroy(takenObject);
                            return;
                        }
                    }
                    else
                    {
                        AddToPointer(currentGrid.gridObject);
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
            else
            {
                if (!IsPointerOnUI())
                    CharacterManager.Instance.ClearObjectToSpawn();
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
            WarriorController gridWarrior = currentGrid.gridObject.GetComponent<WarriorController>();
            WarriorController pointerWarrior = takenObject.GetComponent<WarriorController>();
            if(pointerWarrior.warriorData.WarriorType==gridWarrior.warriorData.WarriorType)
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
        private bool IsPointerOnUI()
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Grid GetMouseOverGrid()
        {
            Vector3 mousePos = Input.mousePosition;
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
