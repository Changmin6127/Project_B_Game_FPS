using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour 
{

    private static T _Instance = null;					    //Instance Object 
    private static object _lock = new object();				//Lock Object
    private static bool applicationIsQuitting = false;		//Class Flag Control Object

    public static T Instance
    {	//Instance Function Begin
        get
        {//Getter Begin
            if (applicationIsQuitting)
            { //if Begin
#if UNITY_EDITOR
                //Exception Code	
                Debug.LogWarning("[Singleton] Instance" + typeof(T) + "Is Already");
#endif
                return null;
            }//if End

            //Lock
            lock (_lock)
            {	//Lock Begin
                if (null == _Instance)
                {	//Instance Begin
                    _Instance = (T)FindObjectOfType(typeof(T));

                    //Exception Create Two Instance 
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {	//Exception Begin 
#if UNITY_EDITOR
                        Debug.LogError("[Singleton] Already Instance : ERROR more then 2");
#endif
                        return _Instance;
                    }//Exception End 

                    if (null == _Instance)
                    {	//Instance Begin
                        GameObject singleton = new GameObject();
                        _Instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
#if UNITY_EDITOR
                        Debug.Log("[Singleton] An Instance of " + typeof(T).ToString());
#endif

                    }
#if UNITY_EDITOR
                    else
                    {//Instance End and Else Begin

                        Debug.Log("[Singleton] Using Instance Already Created: " + _Instance.gameObject.name);

                    }//Else End 
#endif
                }//Instance End 

                return _Instance;		//Return Instance Getter
            }//Lock End
        }//Getter End 
    }//Instance Function End 

    public void OnDestroy()
    {	//Class Destroy Begin
        applicationIsQuitting = true;	//Flag Check
    }//Class Destroy End

    public void OnDestroyInstance()
    {
        Destroy(_Instance);
    }
}

