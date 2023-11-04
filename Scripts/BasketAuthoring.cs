using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class BasketAuthoring : MonoBehaviour
{
   public float height = 0f;

   private class BasketBaker : Baker<BasketAuthoring>
   {
      public override void Bake(BasketAuthoring authoring)
      {
         var entity = GetEntity(TransformUsageFlags.Dynamic);

         AddComponent(entity, new BasketProperties
         {
            Height = authoring.height
         });
      }
   }
}

public struct BasketProperties : IComponentData
{
   public float Height;
}