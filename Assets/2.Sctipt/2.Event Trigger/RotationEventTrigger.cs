namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class RotationEventTrigger : BaseEventTrigger   //Data Field
    {
        private bool isPerformance = false;
        private bool reverse = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Vector3 originRotation;
        private Vector3 startRotation;
        private Vector3 prevDestination;

        [SerializeField]
        private UnityEvent reverseActive = null;
        [SerializeField]
        private UnityEvent reverseFinish = null;
        [SerializeField]
        private Transform targetTransform = null;
        [SerializeField]
        private AnimationCurve animationCurve = null;
        [SerializeField]
        private bool freezeOriginRotation = false;   
        [SerializeField]
        private bool startActive = false;
        [SerializeField]
        private float time = 1;
        [SerializeField]
        [Header("Vector3 값 만큼 회전")]
        private Vector3 destination = Vector3.zero;
        [SerializeField]
        private Transform guideTransform;

    }

    public partial class RotationEventTrigger : BaseEventTrigger   //Main Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            originRotation = targetTransform.eulerAngles;

            if (startActive)
                Active();
        }

        private void Update()
        {
            if (isPerformance)
                Performance();
        }
    }

    public partial class RotationEventTrigger : BaseEventTrigger   //Override Function Field
    {
        public override void Active()
        {
            base.Active();
            if (freezeOriginRotation)
                originRotation = targetTransform.localEulerAngles;
            reverse = false;
            startRotation = targetTransform.localEulerAngles;
            prevDestination = targetTransform.localEulerAngles;
            prevDestination += destination;

            if (guideTransform != null)
                prevDestination = guideTransform.localEulerAngles;

            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {

            isPerformance = false;
            base.Finish();
        }
    }

    public partial class RotationEventTrigger : BaseEventTrigger   //Property Function Field
    {
        public void ReverseActive()
        {
            reverseActive?.Invoke();
            reverse = true;
            startRotation = targetTransform.localEulerAngles;
            prevDestination = originRotation;
            deltaTime = 0;
            isPerformance = true;
        }

        public void ReverseFinish()
        {
            reverseFinish?.Invoke();
        }

        private void Performance()
        {
            deltaTime += Time.deltaTime / time;
            targetTransform.localEulerAngles = Vector3.Lerp(startRotation, prevDestination, animationCurve.Evaluate(deltaTime));

            if (deltaTime > animationCurveEndTime)
            {
                isPerformance = false;
                deltaTime = 0;
                if (reverse == false)
                    Finish();
                else
                    ReverseFinish();
            }
        }
    }
}