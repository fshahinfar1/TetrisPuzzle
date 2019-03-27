using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayRoom.Blocks;

namespace PlayRoom
{
    public class WorldGrid : MonoBehaviour
    {
        static Vector3 NintyDegree = new Vector3(0, 0, 90);
        [SerializeField] int row;
        [SerializeField] int column;

        [SerializeField] Transform contents;

        WorldMatrice matrice;
        GridView gridView;

        Tile[,] tiles;

        List<Tile> shadowList;

        [SerializeField] GameObject rayFx;

        Manager manager;

        void Awake()
        {
            matrice = new WorldMatrice(row, column);
            shadowList = new List<Tile>();
            InstantiateTiles();
            General.RefBook.Register("WorldGrid", this);
        }

        private void Start()
        {
            manager = (Manager)General.RefBook.Summon("Manager");
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

        public bool SetTiles(Vector3 pos, BaseBlock block)
        {
            Coordinate coordinate = gridView.Pos2RowColumn(pos);
            if (!CheckPlacementPossible(coordinate, block)) {
                return false;
            }
            var relativePos = block.GetRelativePos();
            foreach (Coordinate delta in relativePos)
            {
                int row = coordinate.row + delta.row;
                int col = coordinate.column + delta.column;
                var tile = tiles[row, col];
                var sprite = block.GetSprite();
                tile.SetSprite(sprite);
                tile.SetSpriteAlpha(1);
                tile.SetSpriteActive(true);
                //Debug.Log(row + ", " + col);
                matrice.SetTile(row, col);
            }
            CheckForSquash();
            return true;
        }

        public bool CheckPlacementPossible(Coordinate coordinate, BaseBlock block)
        {
            if (!gridView.IsCoordinateValid(coordinate))
                return false;
            var relativePos = block.GetRelativePos();
            foreach (Coordinate delta in relativePos)
            {
                // check all coordinates are valid
                int row = coordinate.row + delta.row;
                int col = coordinate.column + delta.column;
                if (!gridView.IsCoordinateValid(new Coordinate(row, col)))
                    return false;
                // check if all coordinates are empty
                if (matrice.IsFilled(row, col))
                    return false;
            }
            return true;
        }

        void CheckForSquash()
        {
            int next = matrice.NextSquashColumn();
            while(next != -1) {
                SquashColumn(next);
                next = matrice.NextSquashColumn();
            }
            next = matrice.NextSquashRow();
            while (next != -1) {
                SquashRow(next);
                next = matrice.NextSquashRow();
            }
        }

        void SquashColumn(int col)
        {
            matrice.ClearColumn(col);
            for (int r = 0; r < this.row; r++) {
                var tile = tiles[r, col];
                tile.SetSpriteActive(false);
            }
            int middleRow = row / 2;
            var pos = gridView.Coordinate2Pos(new Coordinate(middleRow, col));
            var fxObj = Instantiate(rayFx);
            var fxTr = fxObj.transform;
            fxTr.position = pos;
            fxTr.Rotate(NintyDegree);
            General.DelayCall.Call(this, () =>
            {
                Destroy(fxObj);
            }, 0.3f);
            manager.AddScore(this.column);
        }

        void SquashRow(int row)
        {
            matrice.ClearRow(row);
            for (int c = 0; c < this.column; c++)
            {
                var tile = tiles[row, c];
                tile.SetSpriteActive(false);
            }
            int middleCol = column / 2;
            var pos = gridView.Coordinate2Pos(new Coordinate(row, middleCol));
            var fxObj = Instantiate(rayFx);
            fxObj.transform.position = pos;
            General.DelayCall.Call(this, () =>
            {
                Destroy(fxObj);
            }, 0.3f);
            manager.AddScore(this.row);
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
            if (!gridView.IsCoordinateValid(coordinate))
                return;
            var relativePos = block.GetRelativePos();
            foreach (Coordinate delta in relativePos) {
                int row = coordinate.row + delta.row;
                int col = coordinate.column + delta.column;
                if (!gridView.IsCoordinateValid(new Coordinate(row, col)))
                    continue;
                var tile = tiles[row, col];
                if (!tile.IsSpriteActive())
                {
                    var sprite = block.GetSprite();
                    tile.SetSprite(sprite);
                    tile.SetSpriteAlpha(.5f);
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
            shadowList.Clear();
        }

        public int GetCountRows()
        {
            return this.row;
        }

        public int GetCountColumns()
        {
            return this.column;
        }
    }
}