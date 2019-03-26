using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom {
    public struct Coordinate
    {
        public int row;
        public int column;
        
        public Coordinate(int r, int c)
        {
            row = r;
            column = c;
        }

        public override string ToString()
        {
            return "<" + row + ", " + column + ">";
        }
    }

    public class GridView : MonoBehaviour 
    {
        public Transform contents;
        public int rowNum;
        public float colWidth;
        public float rowHeight;
        public float spacing;

        Transform[,] objMap;
        bool initialized = false;

        float totalHeight;
        float totalWidht;

        void Awake () 
        {
            if (contents != null)
                SetupGrid(false);
        }

        public void SetupGrid(bool force)
        {
            if (initialized && !force)
                return;

            // create a mapping from childs to row and col position
            int countItems = contents.childCount;
            float ratio = (float)countItems / rowNum;
            int cols = Mathf.CeilToInt(ratio);
            Debug.Log("Cols: " + cols);
            objMap = new Transform[rowNum, cols];
            int child = 0;
            for (int r = 0; r < rowNum; r++) {
                for (int c = 0; c < cols; c++) {
                    var trans = contents.GetChild(child);
                    objMap[r, c] = trans;
                    SetItemPos(trans, r, c);
                    child++;
                    if (child >= countItems)
                        break;
                }
            }

            // calculate total height and width
            totalHeight = rowNum * rowHeight + (rowNum - 1) * spacing;
            totalWidht = cols * colWidth + (cols - 1) * spacing;
            // move the contents position so items are in middle
            var pos = transform.localPosition;
            pos.x -= totalWidht / 2 - rowHeight / 2;
            pos.y += totalHeight / 2 - colWidth / 2;
            transform.localPosition = pos;

            initialized = true;
        }

        void SetItemPos(Transform trans, int row, int col)
        {
            float x = col * (colWidth + spacing);
            float y = -row * (rowHeight + spacing);
            Vector2 pos = new Vector2(x, y);
            trans.localPosition = pos;
        }

        /// <summary>
        /// convert global position to (row, col)
        /// </summary>
        /// <param name="pos">Global Position</param>
        /// <returns>a Cordinate having (row, col) value</returns>
        public Coordinate Pos2RowColumn(Vector2 pos)
        {
            Vector2 relativePos = pos - (Vector2)transform.position 
                + new Vector2(colWidth / 2, - rowHeight / 2);
            relativePos.y *= -1;

            if (relativePos.x * relativePos.y < 0 || relativePos.x > totalWidht
                || relativePos.y > totalHeight)
            {
                // not in the region
                return new Coordinate(-1, -1);
            }

            float tmpRow = relativePos.y / (rowHeight + spacing);
            float tmpCol = relativePos.x / (colWidth + spacing);
            int row = Mathf.FloorToInt(tmpRow);
            int col = Mathf.FloorToInt(tmpCol);
            return new Coordinate(row, col);
        }

        /// <summary>
        /// the returned object is refrenced to the
        /// original value so it should not be changed
        /// </summary>
        /// <returns></returns>
        public Transform[,] GetObjectMapping()
        {
            return objMap;
        }
    }
}