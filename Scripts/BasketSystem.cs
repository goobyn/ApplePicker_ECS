using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial struct BasketSystem : ISystem
{

   public void OnUpdate(ref SystemState state)
   {
       Vector3 basketPos = new Vector3(0, 0, 0);   
       
       // get the basket entities and set position to mouse position
       foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketProperties>>())
       {
          Vector3 mousePosition      = Input.mousePosition;
          mousePosition.z            = -Camera.main.transform.position.z;   
          Vector3 mousePos3D         = Camera.main.ScreenToWorldPoint(mousePosition);
          basketPos                  = transform.ValueRW.Position;
          basketPos.x                = mousePos3D.x;
          transform.ValueRW.Position = basketPos;
       }   
 
       // Get the query for all apple entities   
       var appleQuery = state.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<AppleGravityComponent>());
       
       // Get array of apple entities and iterate through them
       using (var apples = appleQuery.ToEntityArray(Allocator.TempJob))
       {
           foreach (var apple in apples)
           {
               // get position of apple 
               var applePosition = state.EntityManager.GetComponentData<LocalTransform>(apple).Position;

               // check if it is in basket
               if (applePosition.y < basketPos.y && applePosition.x < (basketPos.x + 2) && applePosition.x > (basketPos.x -2))
               {
                  // destroy apple
                  state.EntityManager.DestroyEntity(apple);
               }

               // otherwise, check if apple has fallen off screen
               else if (applePosition.y < -20)
               {
                   // destroy apple
                   state.EntityManager.DestroyEntity(apple);
                   
                   // get query for basket entities, get array of those entities, and destroy the first one
                   var basketQuery = state.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<BasketProperties>());
                   using (var baskets = basketQuery.ToEntityArray(Allocator.TempJob))
                   {
                      // get length of array
                      int basketLength = baskets.Length;
                      if (basketLength > 0)
                      {
                         state.EntityManager.DestroyEntity(baskets[0]);
                      }
                      else {
                        SceneManager.LoadScene("Menu_Scene");
                      }
                   }
               }
           }
       }
   }
}