using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, transform.right * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, transform.up * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, transform.forward * 2);
    }
}
