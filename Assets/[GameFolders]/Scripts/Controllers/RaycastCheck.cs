using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public LayerMask stopMask;
    public float CheckDistanceForward()
    {
        RaycastHit hitInfo;

        Debug.DrawRay(transform.position, transform.forward * 2);
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2, stopMask))
        {
            Debug.Log("HiçGirmiyor");
            float distance = Vector3.Distance(transform.position, hitInfo.point);
            if (distance <= 1)
                return 0;
            else
                return distance - 1;
        }
        return 1;
    }
}
