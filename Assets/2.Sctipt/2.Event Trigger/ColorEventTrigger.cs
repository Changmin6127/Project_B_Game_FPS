namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class ColorEventTrigger : BaseEventTrigger     //Data Field
    {
        private bool isActive = false;
        private float deltaTime = 0;
        private float animationCurveEndTime;


        [SerializeField]
        private Image colorEffect = null;
        [SerializeField]
        private Color originColor;
        [SerializeField]
        private Color destinationColor;
        [SerializeField]
        private AnimationCurve animationCurve = null;
        [SerializeField]
        private bool startActive = false;
        [SerializeField]
        private float performanceTime = 3;
    }

    public partial class ColorEventTrigger : BaseEventTrigger     //Override Function Field
    {
        public override void Active()
        {
            deltaTime = 0;
            isActive = true;
            base.Active();
        }

        public override void Finish()
        {
            isActive = false;
            deltaTime = 0;
            base.Finish();
        }
    }

    public partial class ColorEventTrigger : BaseEventTrigger     //Main Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;

            if (startActive)
                Active();
        }

        private void Update()
        {
            if (isActive)
            {
                deltaTime += Time.deltaTime / performanceTime;

                colorEffect.color = Color.Lerp(originColor, destinationColor, animationCurve.Evaluate(deltaTime));

                if (deltaTime > animationCurveEndTime)
                {
                    isActive = false;
                    deltaTime = 0;
                    Finish();
                }
            }
        }
    }
}