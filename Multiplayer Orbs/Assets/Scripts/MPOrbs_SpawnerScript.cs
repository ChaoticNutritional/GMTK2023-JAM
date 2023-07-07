using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MPOrbs_SpawnerScript : MonoBehaviourPun
{
    public GameObject playerOrbPrefab;
    public static MPOrbs_SpawnerScript instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //MPOrbs_PhotonGameManager.instance.spawnReady = true;
        print(PhotonNetwork.LocalPlayer.ActorNumber);
        SpawnOrb(PhotonNetwork.LocalPlayer.ActorNumber - 1);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SpawnOrb(int playerNum)
    {
        //if(playerNum > transform.childCount)
        //{
        //    Debug.LogError("More players than allowed from this object! Trying to spawn player " + playerNum.ToString());
        //    return;
        //}
        //else
        {
            PhotonNetwork.Instantiate(playerOrbPrefab.name, transform.GetChild((playerNum % 4)).position, Quaternion.identity);
        }
    }
}
