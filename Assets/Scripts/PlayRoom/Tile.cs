using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayRoom
{
    public class Tile : MonoBehaviour
    {
        SpriteRenderer shape;
        bool spriteActive;

        void Awake()
        {
            var shapeTrans = transform.Find("shape");
            shape = shapeTrans.GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite s)
        {
            shape.sprite = s;
        }

        public void SetSpriteActive(bool state)
        {
            shape.gameObject.SetActive(state);
            spriteActive = state;
        }

        public bool IsSpriteActive()
        {
            return spriteActive;
        }
    }
}