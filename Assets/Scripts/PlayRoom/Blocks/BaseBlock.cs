using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom.Blocks
{
    public class BaseBlock
    {
        static WorldGrid worldGrid;
        int size;
        Coordinate[] relativePos;
        Sprite sprite;
        int insertIndex;

        public BaseBlock(int size)
        {
            insertIndex = 0;
            this.size = size;
            relativePos = new Coordinate[size];
        }

        public void SetSprite(Sprite s)
        {
            sprite = s;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public void AddRelativePos(Coordinate c)
        {
            relativePos[insertIndex++] = c;
        }

        public void SetGrid(Coordinate coordinate)
        {
            if (worldGrid == null)
            {
                worldGrid = (WorldGrid)General.RefBook.Summon("WorldGrid");
            }
            foreach (Coordinate delta in relativePos) {
                int row = coordinate.row + delta.row;
                int col = coordinate.column + delta.column;
                worldGrid.SetTiles(row, col, sprite);
            }
        }
    }
}