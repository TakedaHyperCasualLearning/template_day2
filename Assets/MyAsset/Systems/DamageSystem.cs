using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class DamageGroup : ComponentGroup<DamageGroup, DamageComponent, CharacterBaseComponent> { }

public class DamageSystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<DamageGroup>(Damage);
    }

    private void Damage(DamageGroup group)
    {
        if (group.data1.damagePoint <= 0) return;

        group.data2.hitPoint -= group.data1.damagePoint;
        group.data1.damagePoint = 0;

        if (group.data2.hitPoint <= 0)
        {
            Debug.Log(group.entity.gameObject.name + " is Dead");
            if (group.entity.GetComponent<EnemyAttackComponent>() != null)
            {
                gameStat.playerGroup.entity.GetComponent<LevelUPComponent>().experience++;
            }
            if (group.entity.GetComponent<PlayerAttackComponent>() != null)
            {
                gameStat.isGameOver = true;
                group.data2.hitPoint = 0;
            }

            gameEvent.onRemovedEntity?.Invoke(group.entity);
        }
    }
}
