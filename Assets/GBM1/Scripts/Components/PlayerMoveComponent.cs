using Unity.Entities;
using UnityEngine;

public class PlayerMoveComponent : MonoBehaviour
{
    public float moveSpeed = 1f;
}

public struct PlayerMoveComponentForBake : IComponentData
{

    public float moveSpeed;

}