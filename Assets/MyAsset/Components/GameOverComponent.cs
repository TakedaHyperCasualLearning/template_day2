using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class GameOverComponent : IComponent
{
    public Entity owner { get; set; }
    public GameObject gameOverUI;
}
