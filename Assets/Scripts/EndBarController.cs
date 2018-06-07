using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBarController : MonoBehaviour {
    GameManagerController gmc;
	// Use this for initialization
	void Start () {
        gmc = GameObject.Find("GameManager").GetComponent<GameManagerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Brick")
        {
            gmc.takeDamage();
        }
        Destroy(col.gameObject);
        
    }
}
