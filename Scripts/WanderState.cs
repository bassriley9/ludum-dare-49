using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class WanderState : BaseState
{
    private Vector3 destination;
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask layermask = LayerMask.NameToLayer("Walls");
    private float raydistance = 3.5f;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private EnemyAI2 enemy;

    public WanderState(EnemyAI2 enemy) : base(enemy.gameObject)
    {
        enemy = enemy;
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            enemy.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }
        
        if (destination.Equals(null) || Vector3.Distance(a: transform.position, b: destination) <= stopDistance)
        {
            FindRandomDestination();
        }
        transform.rotation = Quaternion.Slerp(a: transform.rotation, b: desiredRotation, t: Time.deltaTime * turnSpeed);

        if(IsForwardBlocked())
        {
            transform.rotation = Quaternion.Slerp(a: transform.rotation, b: desiredRotation, t: .2f);

        }
        else
        {
            transform.Translate(translation: Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);


        }
        Debug.DrawRay(start: transform.position, dir: direction * raydistance, Color.red);
        while(IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log(message: "Wall");
        }
        return null;
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(origin: transform.position, direction: transform.forward);
        return Physics.SphereCast(ray, radius: 0.5f, raydistance, layermask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(origin: transform.position, direction);
        return Physics.SphereCast(ray, radius: .5f, raydistance, layermask);
    }
    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) + new Vector3(x: UnityEngine.Random.Range(-4.5f, 4.5f), y: 0f, direction.z);
        destination = new Vector3(testPosition.x, y: 1f, testPosition.z);

        direction = Vector3.Normalize(destination - transform.position);
        direction = new Vector3(direction.x, y: 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);


    }

    Quaternion startingAngle = Quaternion.AngleAxis(angle: -60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(angle: 5, Vector3.up);

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(origin: pos, direction, out hit, GameSettings.AggroRadius))
            {
                var drone = hit.collider.GetComponent<GameObject>();
                Debug.DrawRay(start: pos, dir: direction * hit.distance, Color.red);
                return drone.transform;
            }
            else
            {
                Debug.DrawRay(start: pos, dir: direction * hit.distance, Color.yellow);
                
            }
        }
        return null;
    }

}
