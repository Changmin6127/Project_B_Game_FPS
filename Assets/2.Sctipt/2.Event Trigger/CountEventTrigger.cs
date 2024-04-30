namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class CountEventTrigger : BaseEventTrigger  //Data Field
    {
        private int count = 0;

        [SerializeField]
        private int maxTouchNumber = 1;
        [SerializeField]
        private UnityEvent maxTouchEvent = null;
    }

    public partial class CountEventTrigger : BaseEventTrigger  //Override Function Field
    {
        public override void Active()
        {
            base.Active();
        }

        public override void Finish()
        {
            base.Finish();
        }
    }

    public partial class CountEventTrigger : BaseEventTrigger  //Property Function Field
    {
        public void Initialize()
        {
            count = 0;
        }

        public void TouchCountUp()
        {
            count++;
            if (count >= maxTouchNumber)
            {
                maxTouchEvent?.Invoke();
                Active();
            }
        }

        public void TouchCountDown()
        {
            count--;
            if (count < 0)
            {
                count = 0;
                Finish();
            }
        }
    }
}