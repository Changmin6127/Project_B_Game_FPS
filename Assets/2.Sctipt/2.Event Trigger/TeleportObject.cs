namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class TeleportObject : MonoBehaviour     //Data Field
    {
        [SerializeField]
        private Transform targetTransform = null;
    }

    public partial class TeleportObject : MonoBehaviour     //Function Field
    {
        public void SetTransform(Vector3 value)
        {
            targetTransform.position = value;
        }
    }
}