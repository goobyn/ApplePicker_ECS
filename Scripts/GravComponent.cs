using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

public class GravAuthoring : MonoBehaviour
{
    public float velocity = 0f;
    public float gravity = -5f;

    private class GravBaker : Baker<GravAuthoring>
    {
        public override void Bake(GravAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new AppleGravityComponent
            {
                Velocity = authoring.velocity,
                Gravity = authoring.gravity,
            };
            
            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct AppleGravityComponent : IComponentData
{
    public float Velocity;
    public float Gravity;
}