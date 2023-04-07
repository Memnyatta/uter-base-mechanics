using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBounds : MonoBehaviour
{
    public void OnDrawGizmosSelected()
    {
        var r = GetComponent<Renderer>();
        if (r == null)
            return;
        var bounds = r.bounds;
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x,bounds.max.y,transform.position.z), bounds.extents * 2);
    }
}
