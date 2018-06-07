using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System.IO;
using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{


    RecordElement[] records;
    public Text score;
    public Text playerName;
    public GameObject rankingScreen;
    bool rankingScreenActive = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(getRecords("https://lit-plateau-96770.herokuapp.com/records"));
    }

    IEnumerator getRecords(string url)
    {
        using (WWW www = new WWW(url))
        {


            yield return www;
            var recordList = Record.FromJson(www.text);
            records = recordList.Records;
            displayRecords();
        }
    }

    public void displayRecords()
    {
        float posY = 525f;
        for(int i = 0; i< records.Length; i++)
        {
            
            Instantiate(playerName, rankingScreen.transform);
            Instantiate(score, rankingScreen.transform);
            playerName.text = records[i].PlayerName;
            score.text = records[i].Score.ToString();
            score.rectTransform.position = new Vector3(score.rectTransform.position.x, posY, 0f);                
            playerName.rectTransform.position = new Vector3(playerName.rectTransform.position.x, posY, 0f);
            posY -= 100;
            
        }
    }

    public void sendRecord(string name, int score)
    {
        StartCoroutine(Upload(name, score));
    }
    public void toggleRankingScreen()
    {
        rankingScreenActive = !rankingScreenActive;
        rankingScreen.SetActive(rankingScreenActive);
    }

    IEnumerator Upload(string name, int score)
    {
        {
            if(name == "")
            {
                name = "IMBECILnomeEmBranco";
            }
            WWWForm form = new WWWForm();
            form.AddField("name", name);
            form.AddField("score", score);

            using (UnityWebRequest www = UnityWebRequest.Post("https://lit-plateau-96770.herokuapp.com/records", form))
            {
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
    }
}

