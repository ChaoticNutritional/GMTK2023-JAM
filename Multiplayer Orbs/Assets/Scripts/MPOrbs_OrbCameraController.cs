using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MPOrbs_OrbCameraController : MonoBehaviour
{
    public GameObject objectToFollow;

    // Start is called before the first frame update
    void Start()
    {
        if(objectToFollow.GetPhotonView().IsMine == false && PhotonNetwork.IsConnected == true)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = objectToFollow.transform.position + Vector3.back * 10f;
    }
}
