using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

public partial struct SpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        double currentTime = SystemAPI.Time.ElapsedTime;

        foreach (var (spawner, transform, properties) in SystemAPI.Query<RefRW<Spawner>, RefRO<LocalTransform>, RefRO<AppleTreeProperties>>())
        {
            // Check if it's time to spawn for this specific spawner
            if (spawner.ValueRO.NextSpawnTime <= currentTime)
            {
                // Update the spawner's spawn position based on the current transform
                spawner.ValueRW.SpawnPosition = transform.ValueRO.Position;

                // Spawn the apple
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                float3 pos = spawner.ValueRW.SpawnPosition;
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));

                // Set the next spawn time
                spawner.ValueRW.NextSpawnTime = (float)currentTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}
