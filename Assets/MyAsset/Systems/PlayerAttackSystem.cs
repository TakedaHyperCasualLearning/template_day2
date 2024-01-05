using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class PlayerAttackGroup : ComponentGroup<PlayerAttackGroup, PlayerAttackComponent, CharacterMoveComponent> { }

public class PlayerAttackSystem : AGameSystem, IUpdateSystem
{
    public override void SetupEvents()
    {
        masterSystem.gameEvent.onOneTouchMove += onTouchStay;
    }

    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<PlayerAttackGroup>(AttackTimer, deltaTime);
    }

    private void AttackTimer(PlayerAttackGroup group, float deltaTime)
    {
        group.data1.timer += deltaTime;
        if (group.data1.timer < group.data1.interval) return;
        if (group.data1.isCanShot) return;
        group.data1.isCanShot = true;
        group.data1.timer = 0.0f;
    }

    private void onTouchStay(Vector3 pos)
    {
        entityManager.Foreach<PlayerAttackGroup>(PlayerAttack);
    }

    private void PlayerAttack(PlayerAttackGroup group)
    {
        if (!group.data1.isCanShot) return;
        for (int i = 0; i < group.data1.weaponList.Count; i++)
        {
            Entity bullet = gameFunction.onSpawnEntityFromPool?.Invoke(group.data1.weaponList[i].GetComponent<EntityComponent>());
            gameEvent.onSpawnedEntity?.Invoke(bullet);
            bullet.transform.position = group.entity.transform.position;
            bullet.GetComponent<BulletMoveComponent>().direction = group.entity.transform.forward;
            bullet.GetComponent<BulletAttackComponent>().attackPoint = group.data1.attackPoint;
            if (bullet.GetComponent<FollowBulletComponent>() != null)
            {
                bullet.GetComponent<FollowBulletComponent>().targetTransform = SearchNearEnemy(group);
                Debug.Log(bullet.GetComponent<FollowBulletComponent>().targetTransform);
            }
            group.data1.isCanShot = false;
        }
    }

    private Transform SearchNearEnemy(PlayerAttackGroup group)
    {
        List<Entity> enemyList = gameFunction.onGetEntityList?.Invoke(Singleton.instance.resourceData.enemyPrefab.GetComponent<EntityComponent>());
        if (enemyList == null || enemyList.Count == 0) return null;

        Transform nearTransform = null;

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (nearTransform == null)
            {
                nearTransform = enemyList[i].transform;
                continue;
            }

            if (Vector3.Distance(enemyList[i].transform.position, group.entity.transform.position) < Vector3.Distance(nearTransform.position, group.entity.transform.position))
            {
                nearTransform = enemyList[i].transform;
            }
        }

        return nearTransform;
    }
}
