namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class AbleEventTrigger : BaseEventTrigger    //Data Field
    {

    }

    public partial class AbleEventTrigger : BaseEventTrigger    //Override Function Field
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

    public partial class AbleEventTrigger : BaseEventTrigger    //Function Field
    {
        private void OnEnable()
        {
            Active();
        }

        private void OnDisable()
        {
            Finish();
        }
    }
}