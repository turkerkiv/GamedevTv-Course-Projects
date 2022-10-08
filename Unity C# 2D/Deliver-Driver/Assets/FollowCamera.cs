using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //This thing's position must be same with car
    [SerializeField] GameObject carObjectToFollow;
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position= carObjectToFollow.transform.position+ new Vector3(0,0,-20);
    }
}
