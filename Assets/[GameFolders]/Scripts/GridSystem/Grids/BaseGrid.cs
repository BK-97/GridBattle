using UnityEngine;
namespace GridSystem
{
    public abstract class BaseGrid : MonoBehaviour
    {
        public abstract void Initialize(GridController gridController);
        public abstract void AddObject(GameObject gridGameObject);
        public abstract void RemoveObject();
    }
}