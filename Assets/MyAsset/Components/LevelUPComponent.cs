using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPComponent : IComponent
{
    public Entity owner { get; set; }

    public int experience = 0;
    public int levelUpBorder = 0;

    public int attackOldLevel = 0;
    public int attackRiseValue = 0;
    public int attackBaseValue = 0;

    public int hitPointOldLevel = 0;
    public int hitPointRiseValue = 0;
    public int hitPointBaseValue = 0;


    public int speedOldLevel = 0;
    public float speedRiseValue = 0;
    public float speedBaseValue = 0;
}
