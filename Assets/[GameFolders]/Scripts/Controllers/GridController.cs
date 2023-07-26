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
                allGrids[i].gridController = this;
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
        }
        private void OnDisable()
        {
            OnGridLiberate.RemoveListener(PermamentGridLiberate);
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
                liberatedGrid.gameObject.SetActive(true);
            }
        }
    }
}
