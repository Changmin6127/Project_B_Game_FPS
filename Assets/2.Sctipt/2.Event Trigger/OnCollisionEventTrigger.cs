using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class OnCollisionEventTrigger : MonoBehaviour  //Data Field
{
    [SerializeField]
    private UnityEvent enterEvent;
    [SerializeField]
    private UnityEvent exitEvent;
}

public partial class OnCollisionEventTrigger : MonoBehaviour  //Function Field
{
    private void OnCollisionEnter(Collision collision)
    {
        enterEvent?.Invoke();
    }
    private void OnCollisionExit(Collision collision)
    {
        exitEvent?.Invoke();
    }
}