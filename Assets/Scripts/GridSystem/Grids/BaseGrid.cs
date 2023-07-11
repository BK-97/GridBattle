using UnityEngine;
namespace GridSystem
{
    public abstract class BaseGrid : MonoBehaviour
    {
        public abstract void Initialize(Vector3 pos);
        public abstract void AddObject(GameObject gridGameObject);
        public abstract void RemoveObject();
        public abstract void OnMouseEnter();
        public abstract void OnMouseExit();
        public abstract void MouseOverEmptyGrid();
        public abstract void GridFull();
        public abstract void GridObjectCanTaken();
        public abstract void MouseIsAway();
    }
}