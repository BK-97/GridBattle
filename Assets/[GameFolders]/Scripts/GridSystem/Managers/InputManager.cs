using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace GridSystem.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        #region Params
        private Grid currentGrid;
        private Grid previousGrid;
        private Transform mousePointer;
        [HideInInspector]
        public GameObject takenObject;
        private Vector3 lastPos;

        [SerializeField]
        public LayerMask gridLayerMask;
        [SerializeField]
        public LayerMask GroundLayerMask;

        #endregion
        #region Events
        public static Vector3Event OnTap = new Vector3Event();
        public static Vector3Event OnHold = new Vector3Event();
        public static Vector3Event OnRelease = new Vector3Event();
        public static UnityEvent OnCheckSell = new UnityEvent();
        #endregion
        #region MonoBehaviourMethods
        private void OnEnable()
        {
            OnTap.AddListener(TapCheck);
            OnHold.AddListener(SetPointerPos);
            OnRelease.AddListener(TapCheck);
        }
        private void OnDisable()
        {
            OnTap.RemoveListener(TapCheck);
            OnHold.RemoveListener(SetPointerPos);
            OnRelease.RemoveListener(TapCheck);
        }
        private void Update()
        {
            if (!LevelManager.Instance.IsLevelStarted)
                return;
            InputCalcualte();
        }
        #endregion
        #region Methods
        private void InputCalcualte()
        {
            Ray ray;
            RaycastHit hit;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
                {
                    lastPos = Input.mousePosition;
                    OnTap.Invoke(hit.point);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
                {
                    OnHold.Invoke(hit.point);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
                {
                    lastPos = Input.mousePosition;
                    OnRelease.Invoke(hit.point);
                }
            }
#else
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        ray = Camera.main.ScreenPointToRay(touch.position);
        
        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    lastPos = touch.position;
                    OnTap.Invoke(touch.position);
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    OnHold.Invoke(hit.point);
                }
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, GroundLayerMask))
            {
                lastPos = touch.position;
                OnRelease.Invoke(touch.position);
            }
        }
    }
#endif
        }
        private void SetPointerPos(Vector3 clickPoint)
        {
            mousePointer.transform.position = clickPoint;
        }
        private void TapCheck(Vector3 pos)
        {
            SetPointerPos(pos);

            if (CheckIfGrid())
            {
                if (currentGrid.isInvaded)
                {
                    previousGrid.AddObject(takenObject);
                    takenObject = null;

                }
                else if (currentGrid.hasObject)
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
            else
            {
                OnCheckSell.Invoke();
                if (takenObject != null)
                {
                    previousGrid.AddObject(takenObject);
                    takenObject = null;
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
            Ray ray = Camera.main.ScreenPointToRay(lastPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, gridLayerMask))
            {
                currentGrid = hit.collider.gameObject.GetComponent<Grid>();

                if (currentGrid != null)
                {
                    if (currentGrid.isInvaded)
                    {
                        currentGrid = null;
                        return false;
                    }
                    else
                        return true;
                }
            }
            currentGrid = null;
            return false;
        }
        public void SetPointer(Transform _pointer)
        {
            mousePointer = _pointer;
        }
        #endregion
    }


}
