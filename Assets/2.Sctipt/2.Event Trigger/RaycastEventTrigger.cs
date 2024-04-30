using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil;

public partial class RaycastEventTrigger : BaseEventTrigger //Data Field
{
    [SerializeField] private LayerMask raycastLayerMask; // 검출할 레이어 마스크
    [SerializeField] private float raycastDistance = 10f; // 레이 길이
}

public partial class RaycastEventTrigger : BaseEventTrigger //Function Field
{
    public override void Active()
    {
        base.Active();
    }

    private void Update()
    {
        // 레이를 쏴서 충돌 체크
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, raycastLayerMask))
        {
            Debug.Log("닿았음");
            Active();
        }

        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
    }
}
