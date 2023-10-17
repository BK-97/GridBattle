using UnityEngine;
using UnityEngine.UI;
using GridSystem.Controllers;
using DG.Tweening;
namespace GridSystem
{
    public class Grid : BaseGrid
    {
        #region Params
        private GameObject gridObject;
        public Transform objectArea;
        public bool hasObject { get; private set; }
        private GridController gridController;
        #endregion
        #region BaseGridMethods
        public override void Initialize(GridController _gridController)
        {
            gridController = _gridController;
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
        #endregion
        #region InvadeMethods
        public void Invaded()
        {
            gridController.GridInvaded(this);
            PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("Invaded"),transform.position,Quaternion.identity);
        }
        public void Liberated()
        {
            PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("Liberated"), transform.position,Quaternion.identity);
        }
        [SerializeField]
        private Image blackKnob;
        private float currentInvadeTime = 0f;
        public void Invading(float invadeDuration)
        {
            currentInvadeTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentInvadeTime / invadeDuration);
            blackKnob.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);
        }
        public void CancelInvading()
        {
            blackKnob.transform.localScale = Vector3.zero;
            currentInvadeTime = 0;
        }
        #endregion
        #region Helpers
        public GameObject GetGridObject()
        {
            return gridObject;
        }
        #endregion
    }
}
