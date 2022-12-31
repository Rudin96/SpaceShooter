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
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        var mousePosViewport = CameraSingleton.Instance.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10.0f;
        foreach (var transform in SystemAPI.Query<TransformAspect>().WithAll<Player>())
        {
            var players = PlayerQuery.ToEntityArray(Allocator.Temp);
            var playerSpeed = SystemAPI.GetComponent<Player>(players[0]).Speed;
            var mouseDirFromPlayer = new float3(mousePosViewport) - transform.LocalPosition;
            transform.LocalPosition += new float3(inputX, inputY, 0.0f) * playerSpeed * SystemAPI.Time.DeltaTime;
            transform.LocalRotation = quaternion.LookRotation(Vector3.forward, mouseDirFromPlayer);
        }
    }
}