using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayRoom.Blocks;

namespace PlayRoom
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] TextMesh scoreText;
        BlockGenerator blockGen;
        WorldGrid worldGrid;
        int score;

        void Awake()
        {
            General.RefBook.Register("Manager", this);
            score = 0;
            AddScore(0);
        }

        void Start()
        {
            blockGen = (BlockGenerator)General.RefBook.Summon("BlockGenerator");
            blockGen.Generate();
            blockGen.onGenerate += OnGenerate;
            worldGrid = (WorldGrid)General.RefBook.Summon("WorldGrid");
        }

        public void AddScore(int score)
        {
            this.score += score;
            scoreText.text = this.score.ToString();
        }

        void OnGenerate(BaseBlock block)
        {
            bool possible = CheckIfMovePossible(block, worldGrid);
            if (!possible) {
                Debug.Log("You Lost");
            }
        }

        public bool CheckIfMovePossible(Blocks.BaseBlock block, WorldGrid worldGrid)
        {
            var relativePos = block.GetRelativePos();
            int rows = worldGrid.GetCountRows();
            int cols = worldGrid.GetCountColumns();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    bool res = worldGrid
                        .CheckPlacementPossible(new Coordinate(r, c), block);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}