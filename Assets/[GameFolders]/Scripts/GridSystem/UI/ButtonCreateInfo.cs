using UnityEngine;
using TMPro;
using GridSystem.Managers;
namespace GridSystem.UI
{
    public class ButtonCreateInfo : MonoBehaviour
    {
        #region Params
        public GameObject spawnPrefab;
        [SerializeField]
        private int spawnCost;
        private TextMeshProUGUI textMesh;
        #endregion
        #region Methods
        private void Start()
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = spawnPrefab.name;
        }
        public void InvokeSpawnPrefab()
        {
            if (ExchangeManager.Instance.UseCurrency(CurrencyType.Coin,spawnCost))
                SpawnManager.Instance.CreatePrefab(spawnPrefab);

        }
        #endregion
    }
}

