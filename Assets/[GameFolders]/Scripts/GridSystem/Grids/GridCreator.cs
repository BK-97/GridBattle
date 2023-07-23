using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GridSystem
{
    public class GridCreator : MonoBehaviour
    {
        #region Params
        [SerializeField]
        private int gridMapHeight = 1;
        [SerializeField]
        private int gridMapWidth = 1;
        [SerializeField]
        private GameObject gridPrefab;
        public List<Grid> liberatedGrids;
        public List<Grid> allGrids;
        #endregion
        #region Methods
        private void Start()
        {
            for (int i = 0; i < allGrids.Count; i++)
            {
                if (allGrids[i].gameObject.activeSelf)
                    liberatedGrids.Add(allGrids[i]);
            }
        }
        public void GridInvaded(Grid invadedGrid)
        {
            if (liberatedGrids.Contains(invadedGrid))
            {
                liberatedGrids.Remove(invadedGrid);
                invadedGrid.gameObject.SetActive(false);
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
        public void CreateGrid()
        {
            allGrids.Clear();
            DeleteChildGrids();
            Vector3 creatorPosition = transform.position;

            float offsetX = (gridMapWidth - 1) * 0.5f;
            float offsetZ = (gridMapHeight - 1) * 0.5f;

            if (gridMapWidth % 2 == 1)
                creatorPosition.x += 0.5f;
            if (gridMapHeight % 2 == 1)
                creatorPosition.z += 0.5f;
            for (int row = 0; row < gridMapHeight; row++)
            {
                for (int col = 0; col < gridMapWidth; col++)
                {
                    Vector3 offset = new Vector3(col - offsetX, 0, row - offsetZ);
                    Vector3 position = creatorPosition + offset;
                    GameObject gridObject = Instantiate(gridPrefab, position, Quaternion.identity);
                    gridObject.transform.SetParent(transform);
                    Grid grid = gridObject.GetComponent<Grid>();
                    grid.Initialize(position);
                    allGrids.Add(grid);
                }
            }
        }
        private void DeleteChildGrids()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(transform.GetChild(i).gameObject);
#else
            Destroy(transform.GetChild(i).gameObject);
#endif
            }
        }
        #endregion
    }
}
