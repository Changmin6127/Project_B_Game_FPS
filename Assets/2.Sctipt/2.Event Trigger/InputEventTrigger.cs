using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEventTrigger : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyCode;
    [SerializeField]
    private UnityEvent activeEvent;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
            activeEvent?.Invoke();
    }
}
