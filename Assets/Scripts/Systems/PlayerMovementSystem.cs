using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

partial class PlayerMovementSystem : SystemBase
{
    EntityQuery PlayerQuery;

    protected override void OnCreate()
    {
        PlayerQuery = GetEntityQuery(typeof(Player));
        RequireForUpdate(PlayerQuery);
    }

    protected override void OnUpdate()
    {
        foreach (var transform in SystemAPI.Query<TransformAspect>().WithAll<Player>())
        {
            var players = PlayerQuery.ToEntityArray(Allocator.Temp);
            var playerSpeed = SystemAPI.GetComponent<Player>(players[0]).Speed;
            var mousePosViewport = CameraSingleton.Instance.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10.0f;
            var mouseDirFromPlayer = new float3(mousePosViewport) - transform.LocalPosition;
            transform.LocalPosition += new float3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f) * playerSpeed * SystemAPI.Time.DeltaTime;
            transform.LocalRotation = quaternion.LookRotation(Vector3.forward, mouseDirFromPlayer);
        }
    }
}