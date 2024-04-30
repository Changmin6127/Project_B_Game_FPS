namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class PopupEventTrigger : BaseEventTrigger  //Data Field
    {
 
        private float textPopupAlpha = 0;
        private float imagePopupAlpha = 0;


        private bool isPerformance = false;
        private float deltaTime = 0;
        private float popupAlpha = 1;

        [SerializeField] private float activeTime = 1;
        [SerializeField] private float textPopupFinishAlpha = 1;
        [SerializeField] private float imagePopupFinishAlpha = 1;

        [SerializeField] private Image popupImge = null;
        [SerializeField] private Text popupText = null;
    }

    public partial class PopupEventTrigger : BaseEventTrigger  //Function Field
    {
        public void Active(string _popupText)
        {
            popupText.text = _popupText;
            base.Active();
            deltaTime = 0;
            //popupAlpha = 1;
            textPopupAlpha = textPopupFinishAlpha;
            imagePopupAlpha = imagePopupFinishAlpha;
            isPerformance = true;
        }
        public override void Active()
        {
            base.Active();
            deltaTime = 0;
            //popupAlpha = 1;
            textPopupAlpha = textPopupFinishAlpha;
            imagePopupAlpha = imagePopupFinishAlpha;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
        }

        private void Update()
        {
            if (isPerformance)
            {
                deltaTime += Time.deltaTime;

                if (popupImge != null)
                {
                    if (deltaTime < 1 && deltaTime < imagePopupFinishAlpha)
                    {
                        popupImge.color = new Color(
                            popupImge.color.r
                            , popupImge.color.g
                            , popupImge.color.b
                            , deltaTime);
                    }

                    if (deltaTime > activeTime) //2
                    {
                        popupAlpha -= Time.deltaTime;
                        popupImge.color = new Color(
                            popupImge.color.r
                            , popupImge.color.g
                            , popupImge.color.b
                            , imagePopupAlpha);
                    }
                }

                if (popupText)
                {
                    if (deltaTime < 1 && deltaTime < textPopupFinishAlpha)
                    {
                        popupText.color = new Color(
                            popupText.color.r
                            , popupText.color.g
                            , popupText.color.b
                            , deltaTime);
                    }

                    if (deltaTime > activeTime) //2
                    {
                        popupText.color = new Color(
                            popupText.color.r
                            , popupText.color.g
                            , popupText.color.b
                            , textPopupAlpha);
                    }

                }

                if (deltaTime > activeTime)//2
                {
                    //popupAlpha -= Time.deltaTime;
                    textPopupAlpha -= Time.deltaTime;
                    imagePopupAlpha -= Time.deltaTime;
                }
                if (deltaTime > activeTime + 1)//3
                {
                    isPerformance = false;
                    //popupAlpha = 1;
                    textPopupAlpha = textPopupFinishAlpha;
                    imagePopupAlpha = imagePopupFinishAlpha;
                    deltaTime = 0;
                    Finish();
                }
            }
        }
    }
}