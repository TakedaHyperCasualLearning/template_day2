using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class EnemyMoveGroup : ComponentGroup<EnemyMoveGroup, EnemyMoveComponent, CharacterMoveComponent> { }
public class EnemyMoveSystem : AGameSystem, IUpdateSystem
{
    public override void SetupEvents()
    {
        gameEvent.onSpawnedEntity += Initialize;
    }

    private void Initialize(Entity entity)
    {
        CharacterMoveComponent enemyMove = entity.GetComponent<CharacterMoveComponent>();
        if (enemyMove == null) return;
        enemyMove.isMove = true;
    }

    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<EnemyMoveGroup>(MoveEnemy);
    }

    public void MoveEnemy(EnemyMoveGroup group)
    {
        group.data2.targetPosition = gameStat.playerGroup.entity.transform.position;
        Vector3 direction = (group.data2.targetPosition - group.entity.transform.position).normalized;
        group.data2.direction = (group.data2.targetPosition - group.entity.transform.position).normalized;
    }
}
