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
        GridView gridView;

        Tile[,] tiles;

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
            gridView = contentsObj.AddComponent<GridView>();
            gridView.contents = contents;
            Debug.Log("Set contents");
            gridView.rowNum = row;
            gridView.rowHeight = 0.8f;
            gridView.colWidth = 0.8f;
            gridView.spacing = 0.2f;
            gridView.SetupGrid(true);

            // setup tile mapping
            tiles = new Tile[row, column];
            var matrice = gridView.GetObjectMapping();
            for (int r = 0; r < row; r++) {
                for (int c = 0; c < column; c++) {
                    tiles[r, c] = matrice[r, c].GetComponent<Tile>();
                }
            }
        }

        void Update()
        {
            var pos = (Vector2)Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            var cordinates = gridView.Pos2RowColumn(pos);
            int row = cordinates.row;
            int col = cordinates.column;
            if (row != -1 && col != -1) {
                tiles[row, col].SetSpriteActive(false);
            }
            Debug.Log(cordinates);
        }
    }
}