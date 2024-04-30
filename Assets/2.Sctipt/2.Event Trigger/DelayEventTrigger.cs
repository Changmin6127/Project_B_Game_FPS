namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class DelayEventTrigger : BaseEventTrigger   //Data Field
    {
        private float deltaTime = 0;
        private bool isActive = false;

        [SerializeField]
        private float delayTime = 0;
        [SerializeField]
        private bool isStartActive = false;
    }

    public partial class DelayEventTrigger : BaseEventTrigger   //Function Field
    {
        private void Start()
        {
            if (isStartActive)
                Active();
        }
        public override void Active()
        {
            base.Active();

            deltaTime = 0;
            isActive = true;
        }
        public void Stop()
        {
            deltaTime = 0;
            isActive = false;
            Finish();
        }
        public override void Finish()
        {
            base.Finish();
        }

        private void Update()
        {
            if (isActive)
            {
                deltaTime += Time.deltaTime;

                if (deltaTime > delayTime)
                {
                    isActive = false;
                    deltaTime = 0;
                    Finish();
                }
            }
        }
    }
}