using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public int increaseheight;
    public int movementSpeed;
    public float times = 2;
    public bool vertical;
    public float height = 30f;
    float timeSinceStart;
    private Vector3 startPos;

    public bool timeMultiplier;
    void Start()
    {
        startPos = transform.position;
        height /= 2;
        timeSinceStart = (3 * times) / 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(!vertical)
        {
            if(timeMultiplier)
            {
                Vector3 nextPos = transform.position;
                nextPos.x = startPos.x + height + height * Mathf.Sin(((Mathf.PI * 2) / times) * timeSinceStart);
                timeSinceStart += Time.deltaTime;
                transform.position = nextPos;
            }
            else
            {
                transform.position = (startPos + new Vector3(Mathf.Sin(Time.time), 0.0f, 0.0f) * (float)increaseheight) ;
                Vector3 pos;
                pos = transform.position;
               
            }



        }
        else
        {
            if (timeMultiplier)
            {
                Vector3 nextPos = transform.position;
                nextPos.y = startPos.y + height + height * Mathf.Sin(((Mathf.PI * 2) / times) * timeSinceStart);
                timeSinceStart += Time.deltaTime;
                transform.position = nextPos;
            }
            else
            {
                transform.position = (startPos + new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f));

            }

        }
    }
}
