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

        Transform[,] objMap;
        bool initialized = false;

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

            // move the contents position so items are in middle
            var pos = transform.localPosition;
            pos.x -= cols / 2 * colWidth;
            pos.y -= rowNum / 2 * rowHeight;
            transform.localPosition = pos;

            initialized = true;
        }

        void SetItemPos(Transform trans, int row, int col)
        {
            float x = col * colWidth;
            float y = row * rowHeight;
            Vector2 pos = new Vector2(x, y);
            trans.localPosition = pos;
        }
    }
}