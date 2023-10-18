using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace GridSystem
{
    public class GridController : MonoBehaviour
    {
        public static UnityEvent OnGridLiberate = new UnityEvent();
        public List<Grid> liberatedGrids;
        public List<Grid> allGrids;
        private void Start()
        {
            allGrids.Clear();
            liberatedGrids.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                allGrids.Add(transform.GetChild(i).GetComponent<Grid>());
                allGrids[i].Initialize(this);
            }
            for (int i = 0; i < allGrids.Count; i++)
            {
                if (allGrids[i].gameObject.activeSelf)
                    liberatedGrids.Add(allGrids[i]);
            }
        }
        private void OnEnable()
        {
            OnGridLiberate.AddListener(PermamentGridLiberate);
            CharacterManager.OnNewAllySpawned.AddListener(SetNewAllyToGrid);

        }
        private void OnDisable()
        {
            OnGridLiberate.RemoveListener(PermamentGridLiberate);
            CharacterManager.OnNewAllySpawned.RemoveListener(SetNewAllyToGrid);
        }
        private void SetNewAllyToGrid(GameObject newAlly)
        {
            GridSystem.Grid emptyGrid = GetEmptyGrid();
            if (emptyGrid != null)
            {
                Debug.Log(newAlly.name);
                emptyGrid.AddObject(newAlly);
            }
        }
        private GridSystem.Grid GetEmptyGrid()
        {
            for (int i = 0; i < liberatedGrids.Count; i++)
            {
                if (!liberatedGrids[i].hasObject)
                    return liberatedGrids[i];
            }
            return null;
        }
        private void PermamentGridLiberate()
        {
            Grid liberatedGrid = null;
            for (int i = 0; i < allGrids.Count; i++)
            {
                if (!liberatedGrids.Contains(allGrids[i]))
                {
                    liberatedGrid = allGrids[i];
                    break;
                }
            }
            GridLiberated(liberatedGrid);

        }
        public void GridInvaded(Grid invadedGrid)
        {
            if (liberatedGrids.Contains(invadedGrid))
            {
                liberatedGrids.Remove(invadedGrid);
                invadedGrid.gameObject.SetActive(false);
                int currentGridCount = PlayerPrefs.GetInt(PlayerPrefKeys.GridCount, 4);
                PlayerPrefs.SetInt(PlayerPrefKeys.GridCount, currentGridCount - 1);
            }

        }
        public void GridLiberated(Grid liberatedGrid)
        {
            if (!liberatedGrids.Contains(liberatedGrid))
            {
                liberatedGrids.Add(liberatedGrid);
                liberatedGrid.Liberated();
                liberatedGrid.gameObject.SetActive(true);
            }
        }
    }
}
