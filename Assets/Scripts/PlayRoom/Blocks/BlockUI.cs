using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayRoom.Blocks
{
    public class BlockUI : MonoBehaviour, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        static WorldGrid worldGrid;
        BaseBlock block;
        Vector3 basePosition;

        public void SetBlock(BaseBlock b)
        {
            block = b;
            // update sprite of tiles
            foreach(Transform t in transform) {
                var tile = t.GetComponent<Tile>();
                tile.SetSprite(b.GetSprite());
            }
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
            UpdatePosition(pos);
            ApplyShadowOnWorldGrid(pos);

        }

        void UpdatePosition(Vector3 pos)
        {
            transform.position = pos;
        }

        void ApplyShadowOnWorldGrid(Vector3 pos)
        {
            if (worldGrid == null)
            {
                worldGrid = (WorldGrid)General.RefBook.Summon("WorldGrid");
            }
            worldGrid.ApplyShadow(pos, block);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = basePosition;
        }
    }
}