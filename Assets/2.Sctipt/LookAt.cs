namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class LookAt : MonoBehaviour //Data Field
    {
        [SerializeField]
        private Transform target = null;
    }

    public partial class LookAt : MonoBehaviour //Function Field
    {
        private void Update()
        {
            transform.LookAt(target);
        }
    }
}