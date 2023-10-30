using UnityEngine;
using TMPro;
using GridSystem.Managers;
using UnityEngine.UI;
namespace GridSystem.UI
{
    public class ButtonCreateInfo : MonoBehaviour
    {
        #region Params
        public GameObject spawnPrefab;
        private int spawnCost;
        private TextMeshProUGUI textMesh;
        private Button button;
        public Image soldierIcon;
        public Image textImage;
        #endregion
        #region Methods
        private void Start()
        {
            if (spawnPrefab.GetComponent<WarriorController>().warriorData == null)
                return;
            button = GetComponent<Button>();
            spawnCost = spawnPrefab.GetComponent<WarriorController>().warriorData.cost;
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = spawnCost.ToString();
        }
        public void InvokeSpawnPrefab()
        {
            if (ExchangeManager.Instance.UseCurrency(CurrencyType.Coin, spawnCost))
            {
                var go=PoolingSystem.SpawnObject(spawnPrefab);
                go.GetComponent<WarriorController>().Initalize(1);
            }

        }
        private void Update()
        {
            if (!LevelManager.Instance.IsLevelStarted)
                return;
            if (button == null)
                return;
            if (spawnCost > ExchangeManager.Instance.GetCurrency(CurrencyType.Coin))
                ButtonOff();
            else
                ButtonOn();
        }
        private void ButtonOff()
        {
            button.interactable = false;
            Color newColor = soldierIcon.color;
            newColor.a = 0.8f;
            soldierIcon.color = newColor;
            Color newColor2 = textImage.color;
            newColor2.a = 0.2f;
            textImage.color = newColor2;
        }
        private void ButtonOn()
        {
            button.interactable = true;
            Color newColor = soldierIcon.color;
            newColor.a = 1;
            soldierIcon.color = newColor;
            Color newColor2 = textImage.color;
            newColor2.a = 1;
            textImage.color = newColor2;
        }
        #endregion
    }
}

