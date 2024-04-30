namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class StartActiveEventTrigger : MonoBehaviour     //Data Field
    {
        [SerializeField]
        private UnityEvent activeEvent = null;
    }

    public partial class StartActiveEventTrigger : MonoBehaviour     //Function Field
    {
        private void Start()
        {
            activeEvent?.Invoke();
        }
    }
}