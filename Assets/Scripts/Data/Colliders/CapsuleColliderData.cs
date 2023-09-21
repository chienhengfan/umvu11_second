using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class CapsuleColliderData
    {
        public CapsuleCollider Collider { get; private set; }
        public Vector3 ColliderCenterLocalSpace { get; private set; }
        public Vector3 ColliderVerticalExtents { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if (Collider != null)
            {
                return;
            }

            Collider = gameObject.GetComponent<CapsuleCollider>();
            UpdateColliderData();
        }

        public void UpdateColliderData()
        {
            ColliderCenterLocalSpace = Collider.center;

            ColliderVerticalExtents = new Vector3(0f, Collider.bounds.extents.y, 0f);
        }
    }
}

