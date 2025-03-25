using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PopupSign : MonoBehaviour
{
    public GameObject signupPopup;
    public GameObject ErrorPopup;
   
    public TextMeshProUGUI errorText;

    public TMP_InputField signIdInput;
    public TMP_InputField signNameInput;
    public TMP_InputField signPassInput;
    public TMP_InputField signPcInput;

    private string savePath => Application.persistentDataPath;
    public void ShowSignupPopup()
    {
        signupPopup.SetActive(true);
    }
    public void CloseSignupPopup()
    {
        signupPopup.SetActive(false);
    }

    public void ShowErrorPopup(string error)
    {
        ErrorPopup.SetActive(true);
        errorText.text = error;
    }

    public void CloseErrorPopup()
    {
        ErrorPopup.SetActive(false);
    }

    public void OnClickSignup()
    {
        string id = signIdInput.text;
        string name = signNameInput.text;
        string pass = signPassInput.text;
        string pc = signPcInput.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(pc))
        {
            ShowErrorPopup("��� ������ �Է����ּ���.");
            return;
        }
        if (pass != pc)
        {
            ShowErrorPopup("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        UserData newUser = new UserData(id, pass, name);

        string json = JsonUtility.ToJson(newUser,true);
        string filePath = Path.Combine(savePath, id + ".json");
        File.WriteAllText(filePath, json);

        Debug.Log("ȸ������ �Ϸ�: " + filePath);
        CloseSignupPopup();
    }
}
