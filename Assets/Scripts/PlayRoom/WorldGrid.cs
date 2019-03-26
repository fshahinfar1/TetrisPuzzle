using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom
{
    public class WorldGrid : MonoBehaviour
    {
        [SerializeField] int row;
        [SerializeField] int column;

        [SerializeField] Transform contents;


        WorldMatrice matrice;

        void Awake()
        {
            matrice = new WorldMatrice(row, column);
            InstantiateTiles();
        }

        void InstantiateTiles()
        {
            var tile = Resources.Load<GameObject>("PlayRoom/Tile");
            int count = row * column;
            for (int i = 0; i < count; i++) {
                var newObj = Instantiate(tile);
                var trans = newObj.transform;
                trans.SetParent(contents);
                //trans.SetAsLastSibling();
            }

            // add grid view
            var contentsObj = contents.gameObject;
            var grid = contentsObj.AddComponent<GridView>();
            grid.contents = contents;
            Debug.Log("Set contents");
            grid.rowNum = row;
            grid.rowHeight = 0.8f;
            grid.colWidth = 0.8f;
            grid.SetupGrid(true);
        }
    }
}