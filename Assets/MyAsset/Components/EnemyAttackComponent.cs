using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class EnemyAttackComponent : IComponent
{
    public Entity owner { get; set; }
    public int attackPoint = 0;
    public float interval = 1;
    public float timer = 0;
}
