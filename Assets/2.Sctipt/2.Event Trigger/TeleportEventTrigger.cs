namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class TeleportEventTrigger : BaseEventTrigger    //Data Field
    {
        [SerializeField]
        private Transform targetTransform = null;
        [SerializeField]
        [Header("타겟 transform이 null일때 targetposition사용")]
        private Vector3 targetPosition = Vector3.zero;
    }

    public partial class TeleportEventTrigger : BaseEventTrigger    //Override Function Field
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

    public partial class TeleportEventTrigger : BaseEventTrigger    //Property Function Field
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TeleportObject teleportObject = collision.GetComponent<TeleportObject>();

            if (teleportObject != null)
            {
                Active();
                if (targetTransform != null)
                    teleportObject.SetTransform(targetTransform.position);
                else
                    teleportObject.SetTransform(targetPosition);
                Finish();
            }
        }
    }
}