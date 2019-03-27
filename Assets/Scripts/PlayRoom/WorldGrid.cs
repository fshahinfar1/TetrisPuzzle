using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayRoom.Blocks;

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

        List<Tile> shadowList;

        void Awake()
        {
            matrice = new WorldMatrice(row, column);
            shadowList = new List<Tile>();
            InstantiateTiles();
            General.RefBook.Register("WorldGrid", this);
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
                    var tileComp = matrice[r, c].GetComponent<Tile>();
                    tileComp.SetSpriteActive(false); // hide sprite in the begining
                    tiles[r, c] = tileComp;
                }
            }
        }

        public void SetTiles(int row, int column, Sprite s)
        {
            matrice.SetTile(row, column);
            var tile = tiles[row, column];
            tile.SetSprite(s);
            tile.SetSpriteActive(true);
        }

        /// <summary>
        /// Apply a block shadow
        /// </summary>
        /// <param name="pos">Global position</param>
        /// <param name="block">Block data</param>
        public void ApplyShadow(Vector3 pos, BaseBlock block)
        {
            ClearShadows();
            Coordinate coordinate = gridView.Pos2RowColumn(pos);
            var relativePos = block.GetRelativePos();
            foreach (Coordinate delta in relativePos) {
                int row = coordinate.row + delta.row;
                int col = coordinate.column + delta.column;
                if (!gridView.IsCoordinateValid(new Coordinate(row, col)))
                    continue;
                Debug.Log(row + ", " + col);
                var tile = tiles[row, col];
                if (!tile.IsSpriteActive())
                {
                    var sprite = block.GetSprite();
                    tile.SetSprite(sprite);
                    tile.SetSpriteActive(true);
                    shadowList.Add(tile);
                }
            }
        }

        public void ClearShadows()
        {
            foreach (Tile tile in shadowList)
            {
                tile.SetSpriteActive(false);
            }
        }

        //void Update()
        //{
        //    var pos = (Vector2)Input.mousePosition;
        //    pos = Camera.main.ScreenToWorldPoint(pos);
        //    var cordinates = gridView.Pos2RowColumn(pos);
        //    int row = cordinates.row;
        //    int col = cordinates.column;
        //    if (row != -1 && col != -1) {
        //        tiles[row, col].SetSpriteActive(false);
        //    }
        //    Debug.Log(cordinates);
        //}
    }
}