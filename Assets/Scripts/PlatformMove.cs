using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    //min movement
    public float min = 2f;
    //max movement
    public float max = 3f;

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 3;
    }

    // Update is called once per frame
    void Update()
    {
        //moves platform (ping pongs it)
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);

    }
}
