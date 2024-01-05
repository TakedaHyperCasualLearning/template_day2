using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class PlayerInputGroup : ComponentGroup<PlayerInputGroup, PlayerInputComponent, CharacterMoveComponent> { }

public class PlayerInputSystem : AGameSystem, IUpdateSystem
{
    public override void SetupEvents()
    {
        masterSystem.gameEvent.onOneTouchMove += InputMove;
        masterSystem.gameEvent.onOneTouchEnded += InputChancel;
    }
    public void OnUpdate(float deltaTime)
    {
    }

    private void InputMove(Vector3 pos)
    {
        if (gameStat.isLevelUP) return;
        entityManager.Foreach<PlayerInputGroup, Vector3>(MoveCharacter, pos);
    }

    private void MoveCharacter(PlayerInputGroup group, Vector3 pos)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;
        group.data2.direction = direction.normalized;

        // Vector3 playerPos = Camera.main.WorldToScreenPoint(group.entity.transform.position);
        // Vector3 mousePos = pos;
        // Vector3 direction = mousePos - playerPos;
        // direction.z = 0.0f;
        // group.data2.direction = new Vector3(direction.x, 0.0f, direction.y).normalized;

        Vector3 playerPoint = Camera.main.WorldToScreenPoint(group.entity.transform.position);
        Vector3 rotationDirection = Input.mousePosition - playerPoint;
        rotationDirection = rotationDirection.normalized;
        rotationDirection.z = 0.0f;
        group.data2.targetPosition = Camera.main.ScreenToWorldPoint(playerPoint + rotationDirection);

        group.data2.isMove = true;
    }

    private void InputChancel(Vector3 pos)
    {
        entityManager.Foreach<PlayerInputGroup, Vector3>(ChancelCharacter, pos);
    }

    private void ChancelCharacter(PlayerInputGroup group, Vector3 pos)
    {
        group.data2.isMove = false;
    }
}
