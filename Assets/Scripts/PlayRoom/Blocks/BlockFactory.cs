using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayRoom.Blocks
{
    public class BlockFactory
    {
        public static BaseBlock Build(string name, Sprite s)
        {
            if (name == "1-Block")
            {
                var block = new BaseBlock(1);
                block.AddRelativePos(new Coordinate(0, 0));
                block.SetSprite(s);
                return block;
            }
            else if (name == "3-Block")
            {
                var block = new BaseBlock(3);
                block.AddRelativePos(new Coordinate(0, 0));
                block.AddRelativePos(new Coordinate(0, 1));
                block.AddRelativePos(new Coordinate(0, 2));
                block.SetSprite(s);
                return block;
            }
            else if (name == "4-Block")
            {
                var block = new BaseBlock(4);
                block.AddRelativePos(new Coordinate(0, 0));
                block.AddRelativePos(new Coordinate(0, 1));
                block.AddRelativePos(new Coordinate(1, 0));
                block.AddRelativePos(new Coordinate(1, 1));
                block.SetSprite(s);
                return block;
            }
            return null;
        }
    }
}