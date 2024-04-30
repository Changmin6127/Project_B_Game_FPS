using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class OnTriggerEventTrigger : MonoBehaviour  //Data Field
{
    [SerializeField] private bool isTargetLayer = false;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField]
    private UnityEvent enterEvent;
    [SerializeField]
    private UnityEvent exitEvent;
}

public partial class OnTriggerEventTrigger : MonoBehaviour  //Function Field
{
    private void OnTriggerEnter(Collider other)
    {
        if (isTargetLayer && targetLayer != (targetLayer | (1 << other.gameObject.layer)))
            return;

        enterEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTargetLayer && targetLayer != (targetLayer | (1 << other.gameObject.layer)))
            return;

        exitEvent?.Invoke();
    }
}