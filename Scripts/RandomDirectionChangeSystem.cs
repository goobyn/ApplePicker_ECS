using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct RandomDirectionChangeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleTreeProperties>>())
        {
            if (properties.ValueRW.Random.NextFloat() < properties.ValueRO.ChangeDirectionChance)
            {
                properties.ValueRW.Speed = properties.ValueRO.Speed * -1;
            }
        }
    }
}