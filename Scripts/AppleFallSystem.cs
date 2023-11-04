using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;



public partial struct AppleFallSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach(var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleGravityComponent>>())
        {
            var pos         = transform.ValueRO.Position;
            var velocity    = properties.ValueRO.Velocity;
            var gravity     = properties.ValueRO.Gravity;
           
            velocity        += gravity * SystemAPI.Time.DeltaTime;
            pos.y           += velocity * SystemAPI.Time.DeltaTime;

            transform.ValueRW.Position   = pos;
            properties.ValueRW.Velocity  = velocity;
        }
    }
}