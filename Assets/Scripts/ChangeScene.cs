using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    [System.Serializable]
    public class Entry
    {
        public float time;
        public string name;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Application...");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinLevel()
    {
        SceneManager.LoadScene(sceneName);
        Entry newEntry = new Entry();
        newEntry.time = TimerController.finalTime;
        newEntry.name = this.GetComponent<TMP_InputField>().text;
        String entryString = Newtonsoft.Json.JsonConvert.SerializeObject(newEntry);
        string saveFile = Application.dataPath + "/scores.json";
        System.IO.File.AppendAllText(saveFile, entryString);
    }
}
