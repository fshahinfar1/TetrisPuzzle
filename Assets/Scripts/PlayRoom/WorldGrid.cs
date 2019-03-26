using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom
{
    public class WorldGrid : MonoBehaviour
    {
        [SerializeField] int row;
        [SerializeField] int column;

        WorldMatrice matrice;

        void Awake()
        {
            matrice = new WorldMatrice(row, column);
        }


    }
}