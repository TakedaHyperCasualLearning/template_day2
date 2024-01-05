using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class BulletHitGroup : ComponentGroup<BulletHitGroup, BulletAttackComponent> { }

public class BulletHitSystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<BulletHitGroup>(HitBullet);
    }

    private void HitBullet(BulletHitGroup group)
    {
        entityManager.Foreach<BulletHitGroup, List<Entity>>(CheckCollision, gameFunction.onGetEntityList(Singleton.instance.resourceData.enemyPrefab.GetComponent<EntityComponent>()));
    }

    private void CheckCollision(BulletHitGroup bulletGroup, List<Entity> enemyList)
    {
        if (enemyList == null || enemyList.Count == 0) return;

        for (int i = 0; i < enemyList.Count; i++)
        {
            Entity enemy = enemyList[i];
            if (!enemy.gameObject.activeSelf) continue;

            float radius = enemy.transform.localScale.x + bulletGroup.entity.transform.localScale.x;
            float distance = Vector3.Distance(enemy.transform.position, bulletGroup.entity.transform.position);

            if (radius < distance) continue;

            enemy.GetComponent<DamageComponent>().damagePoint = bulletGroup.data1.attackPoint;
            gameEvent.onRemovedEntity?.Invoke(bulletGroup.entity);
            return;
        }
    }
}
