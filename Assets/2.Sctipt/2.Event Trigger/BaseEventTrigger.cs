namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class BaseEventTrigger : MonoBehaviour   //Data Field
    {
        [SerializeField]
        protected UnityEvent activeEvent = null;
        [SerializeField]
        protected UnityEvent finishEvent = null;
    }

    public partial class BaseEventTrigger : MonoBehaviour   //Function Field
    {
        public virtual void Active()
        {
            activeEvent?.Invoke();
        }

        public virtual void Finish()
        {
            finishEvent?.Invoke();
        }
    }
}