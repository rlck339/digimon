using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static UnityEditor.ShaderData;

public class PopupBank : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI sendErrorText;

    public GameObject depositPanel;
    public GameObject withdrawPanel;
    public GameObject moneybutton;
    public GameObject popupError;
    public GameObject sendPanel;
    public GameObject sendError;
 

    public TMP_InputField depositInput;
    public TMP_InputField withdrawInput;
    public TMP_InputField sendInput;
    public TMP_InputField sendMoneyInput;

    private string savePath => Application.persistentDataPath;

    public void ShowDepositPanel()
    {
        depositPanel.SetActive(true);
        withdrawPanel.SetActive(false);
        moneybutton.SetActive(false);
    }

    public void ShowWithdrawPanle()
    {
        withdrawPanel.SetActive(true);
        depositPanel.SetActive(false);
        moneybutton.SetActive(false);
    }
    public void ClosePanels()
    {
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);
        moneybutton.SetActive(true);
    }
    public void ShowErrorPopup()
    {
        popupError.SetActive(true);
    }
    public void CloseErrorPopup()
    {
        popupError.SetActive(false);
    }
  

    public void UpdateText(string name, int cash, int balance)
    {
        nameText.text = name;
        balanceText.text = string.Format("�ܾ� : {0:N0}��",balance);
        cashText.text = string.Format("���� : {0:N0}��", cash);
    }
    public void Deposit(int amount)
    {
        if (GameManager.Instance.userData.cash >= amount)
        {
            GameManager.Instance.userData.cash -= amount;
            GameManager.Instance.userData.balance += amount;
            GameManager.Instance.SaveUserData();
            GameManager.Instance.Refresh();
        }
        else
        {
            ShowErrorPopup();
        }
    }

    
    public void Withdraw(int amount)
    {
        if (GameManager.Instance.userData.balance >= amount)
        {
            GameManager.Instance.userData.balance -= amount;
            GameManager.Instance.userData.cash += amount;
            GameManager.Instance.SaveUserData();
            GameManager.Instance.Refresh();
        }
        else
        {
            ShowErrorPopup();
        }
    }
  
    
    public void CustomDeposit()
    {
        int amount;
        if (int.TryParse(depositInput.text, out amount) && amount >= 0)
        {
            Deposit(amount);
            depositInput.text = ""; 
        }
    }

    
    public void CustomWithdraw()
    {
        int amount;
        if (int.TryParse(withdrawInput.text, out amount) && amount >= 0)
        {
            Withdraw(amount);
            withdrawInput.text = "";
        }
    }

    public void OnClickSend()
    {
        
        string userid = sendInput.text;
        string moneytext = sendMoneyInput.text;
        
        string filePath = Path.Combine(savePath, userid + ".json");

        if (!File.Exists(filePath))
        {
            ShowSendError("�г����� Ȯ���� �ּ���");
            return;

        }
       

        if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(moneytext))
        {
            ShowSendError("������ �Է��� �ּ���");
                return;
        }
        int amount;
        if (!int.TryParse(sendMoneyInput.text, out amount) || amount <= 0)
        {
            ShowSendError("�۱� �ݾ��� �ʹ� �����ϴ�");
            return;
        }

        string json = File.ReadAllText(filePath);
        UserData sendid = JsonUtility.FromJson<UserData>(json);

        if(GameManager.Instance.userData.balance < amount)
        {
            ShowSendError("�ݾ��� Ȯ���� �ּ���");
            return;
        }
        else
        {
            GameManager.Instance.userData.balance -= amount;
            sendid.balance += amount;        
            GameManager.Instance.SaveUserData();
            GameManager.Instance.Refresh();
           
            string newJson = JsonUtility.ToJson(sendid, true);
            File.WriteAllText(filePath, newJson);
           
            Debug.Log("�۱� �Ϸ�!");
            return;
        }
        

       


    }

    public void ShowSendError(string messege)
    {
        sendError.SetActive(true);
        sendErrorText.text = messege;
    }
    public void CloseSendError()
    {
        sendError.SetActive(false);
    }
    public void ShowSendPanel()
    {
        sendPanel.SetActive(true);
        moneybutton.SetActive(false);
    }
    public void ColseSendPanel()
    {
        sendPanel.SetActive(false);
        moneybutton.SetActive(true);
    }
    
}
