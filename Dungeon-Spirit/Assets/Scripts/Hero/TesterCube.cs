using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterCube : MonoBehaviour
{
    public Material validMat;
    public Material invalidMat;
    public bool isColliding = false;

    void OnEnable()
    {
        Debug.Log(gameObject.name + "'s position: " + transform.position);

        if (Physics.CheckSphere(transform.position, .5f, 3))
        {
            isColliding = true;
        }
    }

    void OnDisable()
    {
        isColliding = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .4f);
    }

    public GameObject GetTileBelow()
    {
        if (isColliding) return null;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity) && (hit.collider.CompareTag("FloorTile")))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
}
