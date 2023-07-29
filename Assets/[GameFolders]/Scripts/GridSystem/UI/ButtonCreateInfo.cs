using UnityEngine;
using TMPro;
using GridSystem.Managers;
namespace GridSystem.UI
{
    public class ButtonCreateInfo : MonoBehaviour
    {
        #region Params
        public GameObject spawnPrefab;
        private int spawnCost;
        private TextMeshProUGUI textMesh;
        #endregion
        #region Methods
        private void Start()
        {
            if (spawnPrefab.GetComponent<WarriorController>().warriorData == null)
                return;
            spawnCost = spawnPrefab.GetComponent<WarriorController>().warriorData.cost;
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = spawnCost.ToString();
        }
        public void InvokeSpawnPrefab()
        {
            if (ExchangeManager.Instance.UseCurrency(CurrencyType.Coin,spawnCost))
                SpawnManager.Instance.CreatePrefab(spawnPrefab);

        }
        #endregion
    }
}

