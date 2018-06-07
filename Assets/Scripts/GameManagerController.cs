using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerController : MonoBehaviour {
    public int score =0;
    Text scoreText;
    public GameObject spot1g, spot2g, spot3g, spot4g;
    public GameObject feather, drop, brick;
    public int life;
    public Image life1, life2, life3;
    public bool gameOver;
    float spawnerSpeed;
    BabyController bc;
    public GameObject gameOverScreen;
    AudioSource audio;
    public AudioClip music, gameOverMusic;
    public GameObject nameInput;
    
    // Use this for initialization
    void Start () {
        scoreText = GameObject.Find("Canvas/Score").GetComponent<Text>();
        
        bc = GameObject.Find("Baby").GetComponent<BabyController>();
        life = 3;
        rain();
        gameOver = false;
        spawnerSpeed = 4;
        audio = GetComponent<AudioSource>();
        audio.clip = music;
        audio.Play();
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void verifySpeed()
    {
        if(score % 5 == 0)
        {
            spawnerSpeed -= 0.2f;
        }
    }
    public void addScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        verifySpeed();
    }
    public void rain() {
        StartCoroutine(spot1());
        StartCoroutine(spot2());
        StartCoroutine(spot3());
        StartCoroutine(spot4());
    }
    public void takeDamage()
    {
        life--;
        if(life == 2)
        {
            life3.color = new Color32(0, 0, 0, 0);
        }
        if (life == 1)
        {
            life2.color = new Color32(0, 0, 0, 0);
        }
        if (life == 0)
        {
            life1.color = new Color32(0, 0, 0, 0);
        }
        if(life == -1)
        {            
            GameOver();
        }
        bc.changeSprite(life);
    }
    public void GameOver()
    {
        PlayerPrefs.DeleteKey("record");
        audio.clip = gameOverMusic;
        audio.Play();
        bc.canIWalk = false;
        gameOver = true;
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
        if (!PlayerPrefs.HasKey("record"))
        {
            nameInput.SetActive(true);
            PlayerPrefs.SetInt("record", score);
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetInt("record") < score)
        {
            PlayerPrefs.SetInt("record", score);
            PlayerPrefs.Save();
        }
        GameObject.Find("Canvas/GameOver/Record").GetComponent<Text>().text = "Record: " + PlayerPrefs.GetInt("record");
        spot1g.SetActive(false);
        spot2g.SetActive(false);
        spot3g.SetActive(false);
        spot4g.SetActive(false);
        spawnerSpeed = 4;
        GameObject[] feathers = GameObject.FindGameObjectsWithTag("Feather");
        foreach(var f in feathers)
        {
            Destroy(f);
        }
        GameObject[] drops = GameObject.FindGameObjectsWithTag("Drop");
        foreach (var d in drops)
        {
            Destroy(d);
        }
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (var b in bricks)
        {
            Destroy(b);
        }
    }

    public void sendRecord()
    {
        RankingController rc= GameObject.Find("Canvas/Ranking").GetComponent<RankingController>();
        rc.sendRecord(nameInput.transform.Find("InputText").GetComponent<Text>().text, score);
        nameInput.SetActive(false);
        
    }
    public void restart()
    {
        audio.clip = music;
        audio.Play();
        score = 0;
        scoreText.text = "Score: " + score;
        life = 3;
        bc.changeSprite(life);
        bc.canIWalk = true;
        gameOver = false;
        gameOverScreen.SetActive(false);
        rain();
        spot1g.SetActive(true);
        spot2g.SetActive(true);
        spot3g.SetActive(true);
        spot4g.SetActive(true);
        life3.color = new Color32(255, 255, 255, 255);
        life2.color = new Color32(255, 255, 255, 255);
        life1.color = new Color32(255, 255, 255, 255);
    }
    public GameObject getRandomObj()
    {
        float r = Random.Range(0.1f, 0.6f)* 10;
        if (r <= 3)
        {
            return drop;
        }
        else if (r <= 5)
        {
            return feather;
        }
        else
            return brick;
    }
    public float getRandomTime()
    {
         return (Random.Range(0.1f, 0.5f) * 10) + spawnerSpeed;

    }

    IEnumerator spot1()
    {
        while (!gameOver)
        {
            Instantiate(getRandomObj(), spot1g.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(getRandomTime());
        }
    }
    IEnumerator spot2()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(getRandomTime());
            Instantiate(getRandomObj(), spot2g.transform.position, Quaternion.identity);
            
        }
    }
    IEnumerator spot3()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(getRandomTime());
            Instantiate(getRandomObj(), spot3g.transform.position, Quaternion.identity);
            
        }
    }
    IEnumerator spot4()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(getRandomTime());
            Instantiate(getRandomObj(), spot4g.transform.position, Quaternion.identity);
            
        }
    }
}
