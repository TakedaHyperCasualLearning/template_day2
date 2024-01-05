using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class LevelUPUIGroup : ComponentGroup<LevelUPUIGroup, LevelUPUIComponent> { }

public class LevelUPUISystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<LevelUPUIGroup>(PopUpModal);
    }

    private void PopUpModal(LevelUPUIGroup group)
    {
        if (!gameStat.isLevelUP)
        {
            group.data1.levelUpUI.SetActive(false);
            return;
        }
        else if (gameStat.isLevelUP && !group.data1.levelUpUI.activeSelf)
        {
            Debug.Log("LevelUP");
            group.data1.levelUpUI.SetActive(true);
            OnClickButtonFunction(group);
            return;
        }

    }

    private void OnClickButtonFunction(LevelUPUIGroup group)
    {
        List<int> randomList = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int temp = Random.Range(0, 4);
            if (randomList.Contains(temp))
            {
                i--;
                continue;
            }
            randomList.Add(temp);
        }
        group.data1.buttonNumberList = randomList;

        group.data1.buttonNumberList.Add(0);
        group.data1.buttonNumberList.Add(1);
        group.data1.buttonNumberList.Add(2);

        for (int i = 0; i < group.data1.buttonNumberList.Count; i++)
        {
            group.data1.levelUpButtonList[i].onClick.RemoveAllListeners();
            switch (group.data1.buttonNumberList[i])
            {
                case 0:
                    group.data1.levelUpButtonList[i].onClick.AddListener(AttackUP);
                    group.data1.levelUpTextList[i].text = "Attack UP";
                    break;
                case 1:
                    group.data1.levelUpButtonList[i].onClick.AddListener(HitPointUP);
                    group.data1.levelUpTextList[i].text = "HitPoint UP";
                    break;
                case 2:
                    group.data1.levelUpButtonList[i].onClick.AddListener(SpeedUP);
                    group.data1.levelUpTextList[i].text = "Speed UP";
                    break;
                case 3:
                    group.data1.levelUpButtonList[i].onClick.AddListener(FollowBulletUP);
                    group.data1.levelUpTextList[i].text = "FollowBullet UP";
                    break;
            }
        }
    }

    private void AttackUP()
    {
        gameStat.attackLevel += 1;
    }

    private void HitPointUP()
    {
        gameStat.hitPointLevel += 1;
    }

    private void SpeedUP()
    {
        gameStat.speedLevel += 1;
    }
    private void FollowBulletUP()
    {
        gameStat.followBulletLevel += 1;
    }

}
