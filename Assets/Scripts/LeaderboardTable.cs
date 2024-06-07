using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreList;
    private List<Transform> highscoreTransformList;

    public class HighscoreEntry
    {
        public float time;
        public string name;
    }
    private void Awake()
    {
        entryContainer = transform.Find("Score Container");
        entryTemplate = entryContainer.Find("Score Template");

        List<HighscoreEntry> highscoreList = new List<HighscoreEntry>();

        string fileContent = System.IO.File.ReadAllText(Application.dataPath + "/scores.json");
        Debug.Log(fileContent);
        string[] tempList = fileContent.Split("}");
        for (int i = 0; i < tempList.Count()-1; i++)
        {
            int pFrom = tempList[i].IndexOf("\"time\":") + "\"time\":".Length;
            int pTo = tempList[i].IndexOf(",");
            string timeStr = tempList[i].Substring(pFrom, pTo-pFrom);
            pFrom = tempList[i].IndexOf("\"name\":\"") + "\"name\":\"".Length;
            pTo = tempList[i].LastIndexOf("\"");
            string name = tempList[i].Substring(pFrom, pTo-pFrom);
            HighscoreEntry entry = new HighscoreEntry();
            entry.time = float.Parse(timeStr);
            entry.name = name;
            highscoreList.Add(entry);
            Debug.Log(entry.time + " by " + entry.name);
        }

        // Sort by fastest time
        for (int i = 0; i < highscoreList.Count; i++)
        {
            for(int j = i+1; j < highscoreList.Count; j++)
            {
                if (highscoreList[j].time < highscoreList[i].time)
                {
                    HighscoreEntry temp = highscoreList[i];
                    highscoreList[i] = highscoreList[j];
                    highscoreList[j] = temp;
                }
            }
        }

        highscoreTransformList = new List<Transform>();
        foreach (HighscoreEntry entry in highscoreList)
        {
            CreateEntry(entry, entryContainer, highscoreTransformList);
        }
    }

    private void CreateEntry(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 70f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        float time = highscoreEntry.time;
        string name = highscoreEntry.name;

        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rank.ToString();
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name.ToString();
        entryTransform.Find("Time").GetComponent<TextMeshProUGUI>().text = time.ToString();
        transformList.Add(entryTransform);
    }
}
