using UnityEngine;
using GridSystem.Controllers;
namespace GridSystem
{
    public class Grid : BaseGrid
    {
        #region Params
        public GameObject middleObject;
        public GameObject gridObject;
        public Transform objectArea;
        [HideInInspector]
        public bool hasObject;
        [HideInInspector]
        public Vector3 gridPos;

        protected Material starterMat;

        [SerializeField]
        private Material fullGridMat;
        [SerializeField]
        private Material canObjectTakenMat;
        [SerializeField]
        private Material onMouseMat;
        #endregion
        #region MonoBehavioursMethod
        private void Start()
        {
            Initialize(transform.position);
        }
        #endregion,
        #region BaseGridMethods
        public override void Initialize(Vector3 pos)
        {
            gridPos = pos;
            Material[] sharedMats = middleObject.GetComponent<MeshRenderer>().sharedMaterials;
            starterMat = sharedMats[0];
        }

        public override void AddObject(GameObject gridGameObject)
        {
            gridObject = gridGameObject;
            gridObject.transform.SetParent(objectArea);
            gridObject.transform.localPosition = Vector3.zero;
            gridObject.transform.localRotation = Quaternion.identity;
            gridObject.GetComponent<GridObject>().GridActive();
            hasObject = true;
        }

        public override void RemoveObject()
        {
            gridObject.GetComponent<GridObject>().GridDeactive();
            gridObject = null;
            hasObject = false;
        }

        public override void OnMouseEnter()
        {
            if (hasObject)
            {
                if (MouseController.GetTakenObject() != null)
                    GridFull();
                else
                    GridObjectCanTaken();
            }
            else
                MouseOverEmptyGrid();
        }
        public override void OnMouseExit()
        {
            MouseIsAway();
        }
        #region ColorChangeMethods
        private void GridColorChange(Material changeMats)
        {
            Material[] mats = middleObject.GetComponent<MeshRenderer>().sharedMaterials;
            mats[0] = changeMats;
            middleObject.GetComponent<MeshRenderer>().sharedMaterials = mats;
        }
        public override void MouseOverEmptyGrid()
        {
            GridColorChange(onMouseMat);
        }
        public override void MouseIsAway()
        {
            GridColorChange(starterMat);
        }
        public override void GridObjectCanTaken()
        {
            GridColorChange(canObjectTakenMat);
        }
        public override void GridFull()
        {
            GridColorChange(fullGridMat);
        }
        #endregion
        #endregion

    }
}
