using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData userData;

    public PopupBank popupBank;

    private string saveFilePath;

    private void Awake()
    {        
        if (Instance == null)
        {
            Instance = this;
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "UserData.json");
    }
    private void Start()
    {
        
        LoadUserData();
        Refresh();
    }
    public void Refresh()
    {
        if (popupBank != null)
        {
            popupBank.UpdateText(userData.name, userData.cash, userData.balance);
        }
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData, true);
        string filePath = Path.Combine(Application.persistentDataPath, userData.id + ".json");
        File.WriteAllText(filePath, json);
        Debug.Log("저장 완료: " + filePath);
    }
    public void LoadUserData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("불러오기완료" + saveFilePath);
        }
        

    }
}

