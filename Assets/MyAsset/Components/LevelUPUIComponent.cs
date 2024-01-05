using System.Collections;
using System.Collections.Generic;
using Donuts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPUIComponent : IComponent
{
    public Entity owner { get; set; }
    public List<int> buttonNumberList = new List<int>();
    public GameObject levelUpUI;
    public List<Button> levelUpButtonList = new List<Button>();
    public List<TextMeshProUGUI> levelUpTextList = new List<TextMeshProUGUI>();
}
