using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherController : MonoBehaviour {
    public float speed;
    public float turnSpeed;
    bool rotationCount = true;
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down *speed* Time.deltaTime);
        
        float randomLimit = Random.Range(0.2f, 0.7f);
        float randomRotation = Random.Range(10, 80);
        if(rotationCount == false)
        {
            transform.Rotate(Vector3.forward * -(randomRotation) * Time.deltaTime);
            if (transform.rotation.z <= -(randomLimit))
            {
                rotationCount = true;
            }
        }
        else
        {
            transform.Rotate(Vector3.back * -(randomRotation) * Time.deltaTime);
            if (transform.rotation.z >= randomLimit)
            {
                rotationCount = false;
            }
        }
       
        if (transform.position.x <= -2.1 )
        {
            rotationCount = true;
        }
        if( transform.position.x >= 2.1)
        {
            rotationCount = false;
        }
        
    }
    
}
