using UnityEngine;
using System;

namespace Movementsystem
{
    [Serializable]
    public class CapsuleColliderUtility
    {
        public CapsuleColliderData CapsuleColliderData { get; private set; }
        [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }

        [field: SerializeField] public SlopeData SlopeData { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if(CapsuleColliderData != null)
            {
                return;
            }
            CapsuleColliderData = new CapsuleColliderData();

            CapsuleColliderData.Initialize(gameObject);

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {

        }
        public void CalculateCapuleColliderDimensions()
        {
            SetCapuleColliderRadius(DefaultColliderData.Radius);
            SetCapuleColliderHeight(DefaultColliderData.Height * (1f - SlopeData.StepHeightPercentage));

            RecalculateCapsuleColliderCenter();

            float halfColliderHeight = CapsuleColliderData.Collider.height / 2f;
            if ( halfColliderHeight> CapsuleColliderData.Collider.radius)
            {
                SetCapuleColliderRadius(halfColliderHeight);
            }

            CapsuleColliderData.UpdateColliderData();
        }

        public void SetCapuleColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }

        public void SetCapuleColliderHeight(float hight)
        {
            CapsuleColliderData.Collider.height = hight;
        }

        public void RecalculateCapsuleColliderCenter()
        {
            float colliderHeightDifference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;

            Vector3 newColliderCenter = new Vector3(0f, DefaultColliderData.Height + (colliderHeightDifference / 2), 0f);

            CapsuleColliderData.Collider.center = newColliderCenter;
        }
    }

}
