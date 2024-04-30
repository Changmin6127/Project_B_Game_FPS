namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class MatchGroupEventTrigger : BaseEventTrigger     //Data Field
    {
        private int matchCount = 0;

        [SerializeField]
        private UnityEvent matchClearEvent = null;
        [SerializeField]
        private List<MatchEventTrigger> matchEventTriggers = new List<MatchEventTrigger>();
    }

    public partial class MatchGroupEventTrigger : BaseEventTrigger     //Override Function Field
    {
        public override void Active()
        {
            base.Active();

            matchCount++;
            if (matchCount == matchEventTriggers.Count)
                matchClearEvent?.Invoke();
        }

        public override void Finish()
        {
            base.Finish();
            matchCount--;
        }
    }
}