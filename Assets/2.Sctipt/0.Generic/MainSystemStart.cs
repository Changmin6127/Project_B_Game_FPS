using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class MainSystemStart : MonoBehaviour    //Data Field
{
    public enum ProjectState { None, Udahee, JJanga }
    [SerializeField] private ProjectState projectState = ProjectState.None;
}

public partial class MainSystemStart : MonoBehaviour    //Function Field
{
    private void Awake()
    {
        MainSystem.Instance.Initialize();
        MainSystem.Instance.DataManager.Character = projectState;
        MainSystem.Instance.SpreadsheetManager.OpenCheck(projectState.ToString(), ReceiveOpenCheck);
    }

    private void ReceiveOpenCheck(bool _value)
    {
        if (_value)
        {
            Debug.Log("서버 통신 성공");
            GetComponent<SceneLoad>().Active(projectState.ToString());
        }
        else
        {
            Debug.Log("서버 통신 실패");
#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}