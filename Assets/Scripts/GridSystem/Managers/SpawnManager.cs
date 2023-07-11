using UnityEngine;
using UnityEngine.Events;

namespace GridSystem.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance { get; private set; }
        #region Params
        [HideInInspector]
        public GameObject currentSpawnObject;
        #endregion
        #region Events
        public static GameObjectEvent OnSpawnObject = new GameObjectEvent();
        #endregion
        #region Methods
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
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
