using System;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherManager : MonoBehaviour
{
    public static OtherManager Instance;

    public TextMeshProUGUI nameInputText;
    public TextMeshProUGUI bestScoreText;

    public string playerName;
    public string bestPlayerName;
    public int bestScore;



    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        LoadBestScoreAndName();
        if (bestPlayerName != "" && bestScore != 0) {
            bestScoreText.text = "Best Score: " + bestPlayerName + " : " + bestScore;
        }
        else {
            bestScoreText.text = "Best Score: " + "not set";
            bestPlayerName = "";
            bestScore = 0;
        }
    }

    // Menu UI //
    public void StartButton()
    {
        playerName = nameInputText.text;

        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Save and Load stuff //
    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveBestScoreAndName()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = bestPlayerName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadBestScoreAndName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
        }
    }
}
