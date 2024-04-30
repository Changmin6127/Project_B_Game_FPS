﻿namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class PositionEventTrigger : BaseEventTrigger   //Data Field
    {
        private bool isInitialize = false;
        private bool isPerformance = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Vector3 originPosition;
        private Vector3 prevDestination;
        private Vector3 startPosition;

        private System.Action finishFunction;

        [SerializeField]
        private UnityEvent reverseActive = null;
        [SerializeField]
        private UnityEvent reverseFinish = null;
        [SerializeField]
        private UnityEvent initializeEvent;
        [SerializeField]
        private Transform targetTransform = null;
        [SerializeField]
        private AnimationCurve animationCurve = null;
        [SerializeField]
        private bool freezeOriginPosition = false;
        [SerializeField]
        private bool loop = false;
        [SerializeField]
        private bool startActive = false;
        [SerializeField]
        public float time = 1;
        [SerializeField]
        [Header("Transform값이 null일경우 Vector3로 이동")]
        private Transform destinationTarget = null;
        [SerializeField]
        [Header("Vector3 값 만큼 이동")]
        private Vector3 destination = Vector3.zero;

    }

    public partial class PositionEventTrigger : BaseEventTrigger   //Main Function Field
    {
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (isPerformance)
                Performance();
        }
    }

    public partial class PositionEventTrigger : BaseEventTrigger   //Override Function Field
    {
        public void PositionInitialize()
        {
            isPerformance = false;

            if (isInitialize)
                transform.position = originPosition;
        }

        public void Pause()
        {
            isPerformance = false;
        }
        public void Initialize()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            originPosition = targetTransform.position;

            if (startActive)
                Active();

            initializeEvent?.Invoke();

            isInitialize = true;
        }
        public override void Active()
        {
            base.Active();

            if (destinationTarget != null)
                prevDestination = destinationTarget.position;
            else
            {
                prevDestination = originPosition;
                prevDestination += destination;
            }
            startPosition = originPosition;
            if (freezeOriginPosition)
                startPosition = targetTransform.position;
            finishFunction = Finish;
            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
        }
    }

    public partial class PositionEventTrigger : BaseEventTrigger   //Property Function Field
    {
        public void ReverseActive()
        {
            reverseActive?.Invoke();
            prevDestination = originPosition;

            if (destinationTarget != null)
                startPosition = destinationTarget.position;
            else
            {
                startPosition = originPosition;
                startPosition -= destination;
            }

            if (freezeOriginPosition)
                startPosition = targetTransform.position;

            finishFunction = ReverseFinish;
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
            targetTransform.position = Vector3.Lerp(startPosition, prevDestination, animationCurve.Evaluate(deltaTime));

            if (deltaTime > animationCurveEndTime && loop == false)
            {
                isPerformance = false;
                deltaTime = 0;
                finishFunction?.Invoke();
            }

            if (loop)
            {
                prevDestination = targetTransform.localEulerAngles;
                prevDestination += destination;
            }
        }
    }
}