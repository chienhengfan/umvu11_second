using UnityEngine;
using System;

[Serializable]
public class DefaultColliderData
{
    [field: SerializeField] public float Height { get; private set; } = 1.5f;
    [field: SerializeField] public float CenterY { get; private set; } = 0.8f;
    [field: SerializeField] public float Radius { get; private set; } = 0.4f;
}
