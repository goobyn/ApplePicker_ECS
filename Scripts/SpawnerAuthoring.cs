using UnityEngine;
using Unity.Entities;

class SpawnerAuthoring : MonoBehaviour
{
   public GameObject Prefab;
   public float SpawnRate;
   public float NextSpawnTime;
}

class SpawnerBaker : Baker<SpawnerAuthoring>
   {
      public override void Bake(SpawnerAuthoring authoring)
      {
         var entity = GetEntity(TransformUsageFlags.None);

         AddComponent(entity, new Spawner
         {
             Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
             SpawnPosition = authoring.transform.position,
             NextSpawnTime = authoring.NextSpawnTime,
             SpawnRate = authoring.SpawnRate,
         });
      }
   }
