using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;

    public Vector3 RelativePosition;

    public Vector3 startPosition;
    public Vector3 endPosition;


    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + RelativePosition;
    }

    // Update is called once per frame
    void Update()
    {
     if (Speed > 0 )
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, Speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                endPosition = startPosition;
                startPosition = transform.position;
            }
        }
     if(RotationSpeed > 0 )
        {
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }

    }
}
