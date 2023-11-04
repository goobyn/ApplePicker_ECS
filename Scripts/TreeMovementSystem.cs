using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct TreeMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleTreeProperties>>())
        {
            var pos = transform.ValueRO.Position;
            var speed = properties.ValueRO.Speed;
            
            pos.x += speed * SystemAPI.Time.DeltaTime;
            transform.ValueRW.Position = pos;
            properties.ValueRW.Position = pos;
          
            if (pos.x < -properties.ValueRO.LeftAndRightEdge)
            {
                properties.ValueRW.Speed = math.abs(speed);
            }
            else if (pos.x > properties.ValueRO.LeftAndRightEdge)
            {
                properties.ValueRW.Speed = -math.abs(speed);
            }
        }
    }
}