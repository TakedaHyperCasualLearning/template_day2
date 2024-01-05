using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class EnemySpawnGroup : ComponentGroup<EnemySpawnGroup, EnemySpawnComponent> { }

public class EnemySpawnSystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        if (gameStat.isLevelUP || gameStat.isGameOver) return;
        entityManager.Foreach<EnemySpawnGroup>(SpawnEnemy, deltaTime);
    }

    private void SpawnEnemy(EnemySpawnGroup group, float deltaTime)
    {
        group.data1.timer += deltaTime;

        if (group.data1.timer < group.data1.spawnInterval) return;

        group.data1.timer = 0.0f;
        Entity enemy = gameFunction.onSpawnEntityFromPool?.Invoke(Singleton.instance.resourceData.enemyPrefab.GetComponent<EntityComponent>());
        gameEvent.onSpawnedEntity(enemy);
        // enemy.transform.position = gameStat.playerGroup.entity.transform.position;
    }
}
