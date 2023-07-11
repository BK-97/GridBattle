using UnityEngine;
using TMPro;
using GridSystem.Managers;
namespace GridSystem.UI
{
    public class ButtonCreateInfo : MonoBehaviour
    {
        #region Params
        public GameObject spawnPrefab;

        private TextMeshProUGUI textMesh;
        #endregion
        #region Methods
        private void Start()
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = spawnPrefab.name;
        }
        public void GiveSpawnPrefabInfo()
        {
            SpawnManager.Instance.CreatePrefab(spawnPrefab);
        }
        #endregion
    }
}

