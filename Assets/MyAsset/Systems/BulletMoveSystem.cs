using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class BulletMoveGroup : ComponentGroup<BulletMoveGroup, BulletMoveComponent> { }

public class BulletMoveSystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        if (gameStat.isLevelUP) return;
        entityManager.Foreach<BulletMoveGroup>(MoveBullet, deltaTime);
    }

    private void MoveBullet(BulletMoveGroup group, float deltaTime)
    {
        FollowBulletComponent followBullet = group.entity.GetComponent<FollowBulletComponent>();
        if (followBullet != null && followBullet.targetTransform != null)
        {
            group.entity.transform.position = Vector3.MoveTowards(group.entity.transform.position, followBullet.targetTransform.position, group.data1.speed * deltaTime);
            return;
        }

        group.entity.transform.position += group.data1.direction * group.data1.speed * deltaTime;
    }
}
