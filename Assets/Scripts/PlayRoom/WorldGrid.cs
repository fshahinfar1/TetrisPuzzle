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
        }

        void Update()
        {
            var pos = (Vector2)Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos = gridView.Pos2RowColumn(pos);
            Debug.Log(pos);
        }
    }
}