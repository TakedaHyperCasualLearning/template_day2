using System;
using System.Collections.Generic;
using UnityEngine;
namespace Donuts
{
    public partial class GameFunction
    {
        public Func<EntityComponent, Entity> onSpawnEntityFromPool;
        public Func<GameObject, Transform, GameObject> onSpawnGameObjectFromPool;
        public Func<EntityComponent, List<Entity>> onGetEntityList;

        public GameFunction()
        {
            onSpawnEntityFromPool = DefaultSpawnEntity;
            onSpawnGameObjectFromPool = DefaultSpawnGameObject;
            onGetEntityList = DefaultGetEntityList;
        }

        public static GameObject DefaultSpawnGameObject(GameObject prefab, Transform parent)
        {
            return GameObject.Instantiate(prefab, parent);
        }

        public static Entity DefaultSpawnEntity(EntityComponent prefab)
        {
            EntityComponent entityComponent = GameObject.Instantiate<EntityComponent>(prefab);
            return entityComponent.ToEntity();
        }

        public static List<Entity> DefaultGetEntityList(EntityComponent prefab)
        {
            return null;
        }

    }
}