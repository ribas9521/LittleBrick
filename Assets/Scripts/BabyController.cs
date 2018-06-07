using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour {

    GameManagerController gmc;
    public float speed;
    public Sprite full, few, beaten, punished;
    public bool canIWalk = true;
    public AudioClip peninha, gotinha, tijolo;
    AudioSource audio;
   
	// Use this for initialization
	void Start () {
        gmc = GameObject.Find("GameManager").GetComponent<GameManagerController>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canIWalk)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.2f, 2.2f),
                                                transform.position.y, transform.position.z);
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    Debug.Log("righ click");
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    Debug.Log("left click");
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
            }
        }
    }
    public void changeSprite(int life)
    {
        SpriteRenderer babyTexture = GetComponentInChildren<SpriteRenderer>();
        if(life == 3)
        {
            babyTexture.sprite = full;
        }
        else if(life == 2)
        {
            babyTexture.sprite = few;
        }
        else if(life == 1)
        {
            babyTexture.sprite = beaten;
        }
        else if(life == 0)
        {
            babyTexture.sprite = punished;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag =="Feather")
        {
            audio.clip = peninha;
            audio.Play();
            Destroy(col.gameObject);
            gmc.addScore(2);
        }
        else if(col.gameObject.tag == "Drop")
        {
            audio.clip = gotinha;
            audio.Play();
            Destroy(col.gameObject);
            gmc.addScore(1);
        }
        else if(col.gameObject.tag == "Brick")
        {
            audio.clip = tijolo;
            audio.Play();
            Destroy(col.gameObject);
            gmc.takeDamage();
        }
    }
}
