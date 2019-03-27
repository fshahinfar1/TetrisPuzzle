using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom.Blocks
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField] Transform container;
        List<string> blockNames;
        Sprite[] sprites;

        void Awake()
        {
            blockNames = new List<string>() {
                "1-Block",
                "3-Block",
                "4-Block",
            };
            sprites = Resources.LoadAll<Sprite>("PlayRoom/fruits");
            General.RefBook.Register("BlockGenerator", this);
        }

        public void Generate()
        {
            // instantiate a block prefab
            string name = GetRandomBlock();
            var prefab = Resources.Load<GameObject>("PlayRoom/" + name);
            var obj = Instantiate(prefab);
            var blockUi = obj.GetComponent<BlockUI>();
            // set base block logic
            var sprite = GetRandomSprite();
            var baseBlock = BlockFactory.Build(name, sprite);
            blockUi.SetBlock(baseBlock);
            // add block to pannel
            obj.transform.SetParent(container, false);
        }

        string GetRandomBlock()
        {
            int count = blockNames.Count;
            int index = (int)(Random.Range(0f, 1f) * count);
            string name = blockNames[index];
            return name;
        }

        Sprite GetRandomSprite()
        {
            int count = sprites.Length;
            int index = (int)(Random.Range(0f, 1f) * count);
            var sprite = sprites[index];
            return sprite;
        }
    }
}