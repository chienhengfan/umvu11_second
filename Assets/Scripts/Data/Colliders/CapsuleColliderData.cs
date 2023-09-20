using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleColliderData
{
    public CapsuleCollider Collider { get; private set; }
    public Vector3 ColliderCenterLocalSpace { get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if(Collider != null)
        {
            return;
        }

        Collider = gameObject.GetComponent<CapsuleCollider>();
        UpdateColliderData();
    }

    public void UpdateColliderData()
    {
        ColliderCenterLocalSpace = Collider.center;
    }
}