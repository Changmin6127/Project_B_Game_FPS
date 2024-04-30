using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public partial class SpreadsheetManager : MonoBehaviour //Data Field
{
    private SpreadsheetData receiveData;

   const string URL = "https://script.google.com/macros/s/AKfycbwZIpG9IagDWezvS2m1CZwVXJ50LoZVeP7pfS25iYFi-mEYThfqH4NOYmt5OUwmr2GxfA/exec";
}

public partial class SpreadsheetManager : MonoBehaviour //Function Field
{
    public void OpenCheck(string _user, System.Action<bool> _response)
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "request");
        form.AddField("user", _user);

        StartCoroutine(OpenCheckPost(form, _response));
    }

    IEnumerator OpenCheckPost(WWWForm form, System.Action<bool> _response)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                receiveData = JsonUtility.FromJson<SpreadsheetData>(www.downloadHandler.text);
                if (receiveData.result == "OK")
                    _response?.Invoke(true);
                else
                    _response?.Invoke(false);
            }
            else
            {
                _response?.Invoke(false);
                Debug.Log("통신실패"); //이때 다른 시트로 변경해주어도됨
            }
        }
    }



    public void Twip_Donation(string _twitchId, int _point)
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "donation");
        form.AddField("twitchId", _twitchId);
        form.AddField("point", _point);

        StartCoroutine(UserDonationPost(form));
    }

    public void Toonation_Donation(string _id,  int _point)
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "donation");
        form.AddField("id", _id);
        form.AddField("point", _point);

        StartCoroutine(UserDonationPost(form));
    }

    IEnumerator UserDonationPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                receiveData = JsonUtility.FromJson<SpreadsheetData>(www.downloadHandler.text);
                if (receiveData.result == "OK")
                {
                    Debug.Log("통신성공");
                    Debug.Log(receiveData.message);
                }
            }
            else
            {
                Debug.Log("통신실패"); //이때 다른 시트로 변경해주어도됨
            }
        }
    }
}

[System.Serializable]
public class SpreadsheetData
{
    public string result;
    public string message;
}