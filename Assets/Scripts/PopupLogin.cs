using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PopupLogin : MonoBehaviour
{
    public GameObject loginPopup;
    public GameObject popupBank;
    public GameObject errorPopup;
    public GameObject signPopup;

    public TextMeshProUGUI errorText;

    public TMP_InputField idInput;
    public TMP_InputField passInput;

    private string savePath => Application.persistentDataPath;
    public void ShowErrorPopup(string messege)
    {
        errorPopup.SetActive(true);
        errorText.text = messege;
    }
    public void CloseErrorPopup()
    {
        errorPopup.SetActive(false);
    }
    public void CloseLoginPopup()
    {
        loginPopup.SetActive(false);
    }
    public void ShowSignPopup()
    {
        signPopup.SetActive(true);
    }
    public void ShowPopupBank()
    {
        popupBank.SetActive(true);
    }

    public void OnClickLogin()
    {
        string id = idInput.text;
        string pass = passInput.text;


        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pass))
        {
            ShowErrorPopup("아아디와 비번을 입력하세요");
            return;

        }
        string filePath = Path.Combine(savePath, id + ".json");

        if (!File.Exists(filePath))
        {
            ShowErrorPopup("일치하는 정보가 없삼");
            return;
        }
        string json = File.ReadAllText(filePath);
        UserData loadedUser = JsonUtility.FromJson<UserData>(json);

        if (loadedUser.pass != pass)
        {
            ShowErrorPopup("비밀번호가 틀렸슈");
            return;
        }

        GameManager.Instance.userData = loadedUser;
        GameManager.Instance.Refresh();
        GameManager.Instance.SaveUserData();

        CloseLoginPopup();
        ShowPopupBank();
        Debug.Log("로그인성공");
    }
}
