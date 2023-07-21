using UnityEngine;
using UnityEngine.Events;

namespace GridSystem.Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        #region Params
        [HideInInspector]
        public GameObject currentSpawnObject;
        #endregion
        #region Events
        public static GameObjectEvent OnSpawnObject = new GameObjectEvent();
        #endregion
        #region Methods
        public GameObject GetSelectedPrefab()
        {
            return currentSpawnObject;
        }
        public void CreatePrefab(GameObject prefab)
        {
            var go = Instantiate(prefab);
            OnSpawnObject.Invoke(go);
        }
        public void ClearObjectToSpawn()
        {
            Destroy(currentSpawnObject);
        }
        #endregion
    }
    public class GameObjectEvent : UnityEvent<GameObject> { }
}
