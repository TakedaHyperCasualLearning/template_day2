using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Donuts;

public class FollowBulletComponent : IComponent
{
    public Entity owner { get; set; }
    public Transform targetTransform;
}
