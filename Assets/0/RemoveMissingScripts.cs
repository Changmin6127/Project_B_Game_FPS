using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts : EditorWindow
{
    [MenuItem("Tools/Remove Missing Scripts")]
    public static void Remove()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        int count = 0;

        foreach (var obj in allObjects)
        {
            // 해당 오브젝트의 컴포넌트 목록을 가져옴
            Component[] components = obj.GetComponents<Component>();

            // 컴포넌트가 Missing인지 확인하고 제거
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    count++;
                    Debug.Log("Removed Missing Script from: " + obj.name);
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(obj);
                    break;
                }
            }
        }

        Debug.Log("Removed Missing Scripts from " + count + " GameObjects.");
    }
}
