namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class EnableEventTrigger : MonoBehaviour //Data Field
    {
        [SerializeField]
        private UnityEvent enableEvent;
        [SerializeField]
        private UnityEvent disableEvent;
    }

    public partial class EnableEventTrigger : MonoBehaviour //Function Field
    {
        private void OnEnable()
        {
            enableEvent?.Invoke();
        }

        private void OnDisable()
        {
            disableEvent?.Invoke();
        }
    }
}