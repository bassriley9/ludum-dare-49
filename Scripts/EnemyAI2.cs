using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI2 : MonoBehaviour
{

    [SerializeField]
    private GameObject laserVisual;

    public Transform Target { get; private set; }
    public StateMachine stateMachine => GetComponent<StateMachine>();

    private void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>();
        /*{
            { typeof(WanderState), new WanderState(enemy: this)},
            { typeof(ChaseState). new ChaseState(enemy: this)},
            { typeof(AttackState), new AttackState(enemy: this)}

        };*/
       // GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void FireWeapon()
    {
        laserVisual.transform.position = (Target.position + transform.position) / 2f;

        float distance = Vector3.Distance(a: Target.position, b: transform.position);
        laserVisual.transform.localScale = new Vector3(x: 0.1f, y: 0.1f, z: distance);
        laserVisual.SetActive(value: true);

        //StartCoroutine(TurnOffLaser());
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
