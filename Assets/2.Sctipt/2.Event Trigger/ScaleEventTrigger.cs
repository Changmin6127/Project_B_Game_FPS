namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class ScaleEventTrigger : MonoBehaviour  //Data Field
    {
        private bool isActive = false;
        private float deltaTime = 0;
        private float endAnimationCurveTime = 0;
        private Vector3 startScale;
        private Vector3 finishScale;
        private Vector3 originScale;
        private Vector3 destinationScale;
        private UnityEvent destinationEvent;

        [SerializeField]
        private Transform targetTransform;
        [SerializeField]
        private AnimationCurve animationCurve;
        [SerializeField]
        private float time = 1;
        [SerializeField]
        private bool isSubstitute = false;
        [SerializeField]
        private bool isInitializeScale = false;
        [SerializeField]
        private Vector3 initializeScale;
        [SerializeField]
        private Vector3 changeScale;
        [SerializeField]
        private UnityEvent activeEvent;
        [SerializeField]
        private UnityEvent finishEvent;
        [SerializeField]
        private UnityEvent reverseActiveEvent;
        [SerializeField]
        private UnityEvent reverseFinishEvent;
    }

    public partial class ScaleEventTrigger : MonoBehaviour  //Main Function Field
    {
        private void Start()
        {
            if (isInitializeScale)
                originScale = initializeScale;
            else
                originScale = targetTransform.localScale;

            if (isSubstitute)
                destinationScale = changeScale;
            else
                destinationScale = originScale + changeScale;
            endAnimationCurveTime = animationCurve[animationCurve.length - 1].time;
        }

        private void Update()
        {
            if (isActive)
                PerformanceProgress();
        }
    }
    public partial class ScaleEventTrigger : MonoBehaviour  //Property Function Field
    {
        public void Deactive()
        {
            isActive = false;
        }
        public void Active()
        {
            activeEvent?.Invoke();
            startScale = originScale;
            finishScale = destinationScale;
            destinationEvent = finishEvent;
            deltaTime = 0;
            isActive = true;
        }
        public void ReverseActive()
        {
            reverseActiveEvent?.Invoke();
            startScale = destinationScale;
            finishScale = originScale;
            destinationEvent = reverseFinishEvent;
            deltaTime = 0;
            isActive = true;
        }
        private void PerformanceProgress()
        {
            deltaTime += Time.deltaTime / time;

            targetTransform.localScale = Vector3.Lerp(startScale, finishScale, animationCurve.Evaluate(deltaTime));

            if (deltaTime > endAnimationCurveTime)
            {
                isActive = false;
                destinationEvent?.Invoke();
            }
        }
    }
}