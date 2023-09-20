using UnityEngine;
using System;

[Serializable]
public class PlayerLayerData 
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
}
