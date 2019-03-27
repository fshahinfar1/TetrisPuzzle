using System.Collections;
using System.Collections.Generic;

namespace PlayRoom
{
    public class WorldMatrice
    {
        int row;
        int column;
        bool[,] matrice;
        List<int> squashRow;
        List<int> squashColumn;
        

        public WorldMatrice(int row, int column) 
        {
            this.row = row;
            this.column = column;
            matrice = new bool[row, column];
            squashColumn = new List<int>();
            squashRow = new List<int>();
        }

        public void SetTile(int row, int column)
        {
            matrice[row, column] = true;
            CheckRow(row);
            CheckColumn(column);
        }

        void CheckRow(int row)
        {
            bool fullRow = true;
            for (int i = 0; i < this.column; i++) {
                if (!matrice[row, i]) {
                    fullRow = false;
                    break;
                }
            }
            if (fullRow) {
                squashRow.Add(row);
            }
        }

        void CheckColumn(int column)
        {
            bool fullColumn = true;
            for (int i = 0; i < this.row; i++) {
                if (!matrice[i, column]) {
                    fullColumn = false;
                    break;
                }
            }
            if (fullColumn) {
                squashColumn.Add(column);
            }
        }

        public bool IsFilled(int row, int column)
        {
            return matrice[row, column];
        }

        public void ClearRow(int row)
        {
            for (int i = 0; i < this.column; i++) {
                matrice[row, i] = false;
            }
        }

        public void ClearColumn(int column)
        {
            for (int i = 0; i < this.row; i++) {
                matrice[i, column] = false;
            }
        }

        public int NextSquashRow()
        {
            if (squashRow.Count == 0)
                return -1;
            int next = squashRow[0];
            squashRow.RemoveAt(0);
            return next;
        }

        public int NextSquashColumn()
        {
            if (squashColumn.Count == 0)
                return -1;
            int next = squashColumn[0];
            squashColumn.RemoveAt(0);
            return next;
        }
    }
}