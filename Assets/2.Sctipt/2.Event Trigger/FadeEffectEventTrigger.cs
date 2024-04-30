namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;

    public partial class FadeEffectEventTrigger : BaseEventTrigger  //Data Field    
    {
        public enum Fade { None, FadeIn, FadeOut }

        private bool isPerformance = false;
        private float deltaTime = 0;
        private Fade fade = Fade.None;

        [SerializeField]
        private Image fadeImage = null;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private UnityEvent fadeInActiveEvent = null;
        [SerializeField]
        private UnityEvent fadeInFinishEvent = null;
        [SerializeField]
        private UnityEvent fadeOutActiceEvent = null;
        [SerializeField]
        private UnityEvent fadeOutFinishEvent = null;
    }

    public partial class FadeEffectEventTrigger : BaseEventTrigger  //Override Function Field
    {
        public override void Active()
        {
            base.Active();

            if (fade == Fade.FadeIn)
                fadeInActiveEvent?.Invoke();
            else if (fade == Fade.FadeOut)
                fadeOutActiceEvent?.Invoke();
        }
        public override void Finish()
        {
            base.Finish();

            if (fade == Fade.FadeIn)
                fadeInFinishEvent?.Invoke();
            else if (fade == Fade.FadeOut)
                fadeOutFinishEvent?.Invoke();
        }
    }

    public partial class FadeEffectEventTrigger : BaseEventTrigger  //Property Function Field
    {
        public void ActiveFadeOut()
        {
            fade = Fade.FadeOut;
            deltaTime = 0;
            isPerformance = true;
            Active();
        }

        public void ActiveFadeIn()
        {
            fade = Fade.FadeIn;
            deltaTime = 0;
            isPerformance = true;
            Active();
        }

        public void SetFadeSpeed(float value)
        {
            speed = value;
        }
    }

    public partial class FadeEffectEventTrigger : BaseEventTrigger  //Main Function Field
    {
        private void Update()
        {
            if (isPerformance)
            {
                deltaTime += Time.deltaTime * speed;

                if (fade == Fade.FadeIn)
                {
                    float prevValue = 1;
                    prevValue -= deltaTime;

                    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, prevValue);

                    if (prevValue < 0)
                    {
                        isPerformance = false;
                        deltaTime = 0;
                        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
                        Finish();
                    }

                }
                else if (fade == Fade.FadeOut)
                {
                    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, deltaTime);

                    if (deltaTime > 1)
                    {
                        isPerformance = false;
                        deltaTime = 0;
                        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
                        Finish();
                    }
                }

            }
        }
    }
}