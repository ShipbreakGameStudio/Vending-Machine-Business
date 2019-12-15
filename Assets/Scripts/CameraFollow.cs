using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position != gameObject.transform.position)
        {
            gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }
    }
}