using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLaser : MonoBehaviour
{
    public LineRenderer laser;

    public Transform target;
    public bool StaticLaser;

    [SerializeField]
    private Transform origin;

    int Laserdamage;
    float damagemultiplier = 1.25f;
    int maxLaserDamage = 25;
    int maxLaserDamageTime = 25;
    bool colliding;

    IEnumerator DamageTick()
    {
        while(colliding)
        {
            //player.damage(Laserdamage);
            yield return new WaitForSeconds(.1f);
        }

        yield break;
   
    }
    void Laser()
    {
        laser.SetPosition(0, origin.position);
        RaycastHit hit;
        if (StaticLaser)
        {
            if(Physics.Raycast(origin.position, origin.forward, out hit))
            {
                if (hit.collider)
                {
                    colliding = true;
                }
                else
                    colliding = false;
            }
 
        }
        else
        {

        }


    }

    void start()
    {
        laser = GetComponent<LineRenderer>();
       // laser.SetWidth(.2f, .2f);

    }
    void update()
    {
        Laser();
    }
}
