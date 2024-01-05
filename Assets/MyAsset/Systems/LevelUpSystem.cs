using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Donuts;
using UnityEngine;

public class LevelUpGroup : ComponentGroup<LevelUpGroup, LevelUPComponent, CharacterBaseComponent, PlayerAttackComponent> { }

public class LevelUpSystem : AGameSystem, IUpdateSystem
{
    public override void SetupEvents()
    {
        gameEvent.onSpawnedEntity += SetupInitialize;
    }

    public void SetupInitialize(Entity entity)
    {
        LevelUPComponent levelUP = entity.GetComponent<LevelUPComponent>();
        CharacterBaseComponent character = entity.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttack = entity.GetComponent<PlayerAttackComponent>();

        if (levelUP == null || character == null || playerAttack == null) return;

        levelUP.attackBaseValue = playerAttack.attackPoint;
        levelUP.hitPointBaseValue = character.hitPointMax;
        levelUP.speedBaseValue = playerAttack.interval;
    }

    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<LevelUpGroup>(LevelUp);
    }

    private void LevelUp(LevelUpGroup group)
    {
        if (group.data1.attackOldLevel < gameStat.attackLevel)
        {
            group.data3.attackPoint = group.data1.attackBaseValue + group.data1.attackRiseValue * gameStat.attackLevel;
            group.data1.attackOldLevel = gameStat.attackLevel;
            gameStat.isLevelUP = false;
        }

        if (group.data1.hitPointOldLevel < gameStat.hitPointLevel)
        {
            group.data2.hitPointMax = group.data1.hitPointBaseValue + group.data1.hitPointRiseValue * gameStat.hitPointLevel;
            group.data2.hitPoint += group.data1.hitPointRiseValue;
            group.data1.hitPointOldLevel = gameStat.hitPointLevel;
            gameStat.isLevelUP = false;
        }

        if (group.data1.speedOldLevel < gameStat.speedLevel)
        {
            group.data3.interval = group.data1.speedBaseValue - group.data1.speedRiseValue * gameStat.speedLevel;
            group.data1.speedOldLevel = gameStat.speedLevel;
            gameStat.isLevelUP = false;
        }


        if (group.data1.experience < group.data1.levelUpBorder) return;

        gameStat.isLevelUP = true;
        group.data1.experience -= group.data1.levelUpBorder;
    }
}