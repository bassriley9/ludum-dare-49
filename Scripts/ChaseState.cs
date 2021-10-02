using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ChaseState : BaseState
{
    private EnemyAI2 enemy;

    public ChaseState(EnemyAI2 enemy) : base(enemy.gameObject)
    {
        enemy = enemy;

    }

    public override Type Tick()
    {
        if (enemy.Target == null)
        {
            return typeof(WanderState);
        }
        transform.LookAt(enemy.Target);
        transform.Translate(translation: Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);

        var distance = Vector3.Distance(a: transform.position, b: enemy.Target.position);
        if (distance <= GameSettings.AttackRange)
        {
            return typeof(AttackState);
        }
        return null;
    }
}
