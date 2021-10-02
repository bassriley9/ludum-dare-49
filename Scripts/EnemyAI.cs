using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;


public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private LayerMask layermask;

    public float attackRange;
    public float sightDistance;
    public float stoppingDistance;
    public float turnSpeed;

    private Vector3 destination;
    private Quaternion DesiredRotation;
    private Vector3 direction;
    private EnemyAI target;
    private EnemyState currentState;

    private void Update()
    {
        switch(currentState)
        {
            case EnemyState.Wander:
                {
                    if(NeedsDestination())
                    {
                        GetDestination();
                    }
                    transform.rotation = DesiredRotation;

                    transform.Translate(translation: Vector3.forward * Time.deltaTime * 5f);
                    int randomDir = UnityEngine.Random.Range(1,3);
                    if(randomDir == 1)
                    {
                        transform.Rotate(transform.up * Time.deltaTime * turnSpeed);
                    }
                    else if(randomDir == 2)
                    {
                        transform.Rotate(transform.up * Time.deltaTime * -turnSpeed);

                    }

                    var rayColow = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(start: transform.position, dir: direction * sightDistance, rayColow);

                    while(IsPathBlocked())
                    {
                        GetDestination();
                    }

                    var targetToAggro = CheckForAggro();
                    if(targetToAggro!= null)
                    {
                        target = targetToAggro.GetComponent<EnemyAI>();
                        currentState = EnemyState.Chase;
                    }
                    break;
                }
                case EnemyState.Chase:
                  {
                    if(target == null)
                      {
                      currentState = EnemyState.Wander;
                      return;
                      }
                    transform.LookAt(target.transform);
                    transform.Translate(translation: Vector3.forward * Time.deltaTime * 5f);
                    if(Vector3.Distance(a:transform.position, b:target.transform.position) < attackRange)
                    {
                        currentState = EnemyState.Attack;
                    }
                    break;
                   }
                case EnemyState.Attack:
                {
                    if (target != null)
                    {
                        //attack
                        Destroy(target.gameObject);
                    }
                     

                    currentState = EnemyState.Wander;
                    break;
                }
        }
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(origin: transform.position, direction);
        var hitSomething = Physics.RaycastAll(ray, sightDistance, layermask);
        return hitSomething.Any();
    }

    private void GetDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) +new Vector3(x: UnityEngine.Random.Range(-4.5f, 4.5f), y: 0f, z: UnityEngine.Random.Range(-4.5f, 4.5f));

        destination = new Vector3(testPosition.x, y: 1f, testPosition.z);

        direction = Vector3.Normalize(direction - transform.position);
        direction = new Vector3(direction.x, y: 0f, direction.z);
        DesiredRotation = Quaternion.LookRotation(direction);
    }
    private bool NeedsDestination()
    {
        if (destination == Vector3.zero)
        {
            return true;
        }
        var distance = Vector3.Distance(a: transform.position, b: destination);
        if(distance <= stoppingDistance)
        {
            return true;
        }
        return false;
    }

    Quaternion StartingAngle = Quaternion.AngleAxis(angle: - 60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(angle: 5, Vector3.up);
    
    private Transform CheckForAggro()
    {
        float aggroRadius = 5f;

        RaycastHit hit;
        var angle = transform.rotation * StartingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for(var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(origin:pos, direction, out hit, aggroRadius))
            {
                var enemy = hit.collider.GetComponent<EnemyAI>();
                if (enemy != null && gameObject.GetComponent<EnemyAI>().target)
                {
                    Debug.DrawRay(start: pos, dir: direction, Color.red);
                    return enemy.transform;
                }
                


            }
            else
            {
                Debug.DrawRay(start: pos, dir: direction, Color.yellow);

            }
            direction = stepAngle * direction;
        }
        return null;

    }

    public enum EnemyState
    {
        Wander, Chase, Attack
    }

}
