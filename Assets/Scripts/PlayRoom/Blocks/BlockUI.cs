using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayRoom.Blocks
{
    public class BlockUI : MonoBehaviour, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        BaseBlock block;
        Vector3 basePosition;

        public void SetBlock(BaseBlock b)
        {
            block = b;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            basePosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            transform.position = pos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = basePosition;
        }
    }
}