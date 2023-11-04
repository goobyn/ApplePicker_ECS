using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class AppleTreeAuthoring : MonoBehaviour
{
    public float speed = 6f;
    public float3 position = 0f;
    public float leftAndRightEdge = 20;
    public float changeDirectionChance = 0.04f;
    
    private class AppleTreeBaker : Baker<AppleTreeAuthoring>
    {
        public override void Bake(AppleTreeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new AppleTreeProperties
            {
                Speed = authoring.speed,
                LeftAndRightEdge = authoring.leftAndRightEdge,
                ChangeDirectionChance = authoring.changeDirectionChance,
                Random = Random.CreateFromIndex((uint)entity.Index),
                Position = authoring.position

            };
            
            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct AppleTreeProperties : IComponentData
{
    public float Speed;
    public float LeftAndRightEdge;
    public float ChangeDirectionChance;
    public float3 Position;

    public Random Random;
}