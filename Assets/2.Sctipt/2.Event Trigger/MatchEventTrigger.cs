namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class MatchEventTrigger : BaseEventTrigger   //Data Field
    {
        [SerializeField]
        private int matchNumber = 0;
        [SerializeField]
        private MatchGroupEventTrigger matchGroupEventTrigger = null;
    }

    public partial class MatchEventTrigger : BaseEventTrigger   //Function Field
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            MatchObject matchObject = collision.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Active();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Active();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            MatchObject matchObject = collision.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Finish();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Finish();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MatchObject matchObject = other.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Active();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Active();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            MatchObject matchObject = other.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Finish();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Finish();
                }
            }
        }
    }
}