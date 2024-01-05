using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class HitPointGroup : ComponentGroup<HitPointGroup, HitPointComponent, CharacterBaseComponent> { }

public class HitPointSystem : AGameSystem, IUpdateSystem
{
    public override void SetupEvents()
    {
        gameEvent.onSpawnedEntity += Initialize;
    }

    public void Initialize(Entity entity)
    {
        CharacterBaseComponent character = entity.GetComponent<CharacterBaseComponent>();
        if (character == null) return;
        character.hitPoint = character.hitPointMax;
    }

    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<HitPointGroup>(FollowPlayer);
    }

    private void FollowPlayer(HitPointGroup group)
    {
        group.data1.hitPointText.text = group.data2.hitPoint.ToString() + " / " + group.data2.hitPointMax.ToString();
    }
}
