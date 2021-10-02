using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : BaseState
{
    private float AttackReadyTimer;
    private EnemyAI2 enemy;

    public AttackState(EnemyAI2 enemy) : base(enemy.gameObject)
    {
        enemy = enemy;

    }

    public override Type Tick()
    {
        if (enemy.Target == null)
        {
            return typeof(WanderState);
        }
        AttackReadyTimer = Time.deltaTime;

        if (AttackReadyTimer <= 0f)
        {
            enemy.FireWeapon();
        }
        return null;
    }
}
