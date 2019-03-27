using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayRoom.Blocks;

namespace PlayRoom
{
    public class Manager : MonoBehaviour
    {
        BlockGenerator blockGen;
        void Start()
        {
            blockGen = (BlockGenerator)General.RefBook.Summon("BlockGenerator");
            blockGen.Generate();
        }
    }
}