using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerSystem : ISystem
{

    // Called once when the system is created.
    // Can be omitted when empty.
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    { 
    }

    // Called once when the system is destroyed.
    // Can be omitted when empty.
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    // Usually called every frame. When exactly a system is updated
    // is determined by the system group to which it belongs.
    // Can be omitted when empty.
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (pmc, transform) in SystemAPI.Query<RefRO<PlayerMoveComponentForBake>, RefRW<LocalTransform>>()) {

            var p = transform.ValueRO.Position;
            p.y += (pmc.ValueRO.moveSpeed)/1000;
            transform.ValueRW.Position = p;

        }
    }


}
