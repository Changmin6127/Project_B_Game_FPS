namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class AnimationEventTrigger : BaseEventTrigger   //Data Field
    {
        [SerializeField]
        private bool startAnimation = false;
        [SerializeField]
        private Animation thisAnimation = null;
        [SerializeField]
        private UnityEvent soundEvent = null;
    }

    public partial class AnimationEventTrigger : BaseEventTrigger   //Function Field
    {
        private void Start()
        {
            if (startAnimation)
                Active();
        }

        public override void Active()
        {
            base.Active();

            thisAnimation.Play();
        }

        public override void Finish()
        {
            base.Finish();
        }

        public void SoundActive()
        {
            soundEvent?.Invoke();
        }
    }
}