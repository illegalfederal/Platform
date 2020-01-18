using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    Transform playerTransform;
    public float maxX, minX, maxY, minY;
    //public GameObject playerObject;
    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        //playerTransform = playerObject.transform;
        //print(playerTransform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, minX, maxX),
            Mathf.Clamp(playerTransform.position.y, minY, maxY), transform.position.z);
    }
}
