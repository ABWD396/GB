using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveComponent))]
public class PlayerEntity : MonoBehaviour
{
    class Baker : Baker<PlayerEntity>
    {
        public override void Bake(PlayerEntity authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerMoveComponentForBake
            {
                moveSpeed = authoring.GetComponent<PlayerMoveComponent>().moveSpeed,
            });
        }
    }
}


