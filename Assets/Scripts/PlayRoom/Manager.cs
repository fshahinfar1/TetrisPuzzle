using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayRoom.Blocks;

namespace PlayRoom
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] TextMesh scoreText;
        int score;
        BlockGenerator blockGen;

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
        }

        public void AddScore(int score)
        {
            this.score += score;
            scoreText.text = this.score.ToString();
        }
    }
}