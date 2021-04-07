using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScenario : MonoBehaviour
{
    [SerializeField] private Vector3 target = new Vector3(9.7f, 6, -20);
    [SerializeField] private float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

        if (this.transform.position.z <= -20)
        {
            transform.position = new Vector3(9.7f, 6, 142);
        }
    }
}
