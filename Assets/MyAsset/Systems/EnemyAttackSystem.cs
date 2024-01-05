using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class EnemyAttackGroup : ComponentGroup<EnemyAttackGroup, EnemyAttackComponent> { }

public class EnemyAttackSystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        if (gameStat.isGameOver) return;
        entityManager.Foreach<EnemyAttackGroup>(CheckCollision);
    }

    private void CheckCollision(EnemyAttackGroup group)
    {

        if (group.data1.timer < group.data1.interval)
        {
            group.data1.timer += Time.deltaTime;
            return;
        }

        float radius = group.entity.transform.localScale.x + gameStat.playerGroup.entity.transform.localScale.x;
        float distance = Vector3.Distance(group.entity.transform.position, gameStat.playerGroup.entity.transform.position);

        if (radius < distance) return;

        gameStat.playerGroup.entity.GetComponent<DamageComponent>().damagePoint = group.data1.attackPoint;
        Debug.Log(gameStat.playerGroup.entity.gameObject.name + " is Damaged");
        group.data1.timer = 0.0f;
    }
}
