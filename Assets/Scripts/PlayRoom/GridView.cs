using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom {
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
        /// <returns>a vector having (row, col) value</row></returns>
        public Vector2 Pos2RowColumn(Vector2 pos)
        {
            Vector2 relativePos = pos - (Vector2)transform.position 
                + new Vector2(colWidth / 2, - rowHeight / 2);
            relativePos.y *= -1;

            if (relativePos.x * relativePos.y < 0 || relativePos.x > totalWidht
                || relativePos.y > totalHeight)
            {
                // not in the region
                return new Vector2(-1, -1);
            }

            float row = relativePos.y / (rowHeight + spacing);
            float col = relativePos.x / (colWidth + spacing);
            row = Mathf.FloorToInt(row);
            col = Mathf.FloorToInt(col);
            return new Vector2(row, col);
        }
    }
}