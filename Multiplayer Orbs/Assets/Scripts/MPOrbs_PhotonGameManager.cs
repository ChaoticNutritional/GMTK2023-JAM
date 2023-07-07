using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;

public class MPOrbs_PhotonGameManager : MonoBehaviourPunCallbacks
{

    public static MPOrbs_PhotonGameManager instance;
    public int playersConnected;
    public int playerID;
    public bool colorChosen = false;
    //public bool spawnReady;

    private void Start()
    {
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
        DontDestroyOnLoad(gameObject);
        instance = this;
        //playerID = photonView.Owner.ActorNumber;
        //print(playerID);
    }
    #region Photon Callbacks

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Photon Callbacks


    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


        //    LoadArena();
        //}
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

       if (PhotonNetwork.IsMasterClient)
       {
            PhotonNetwork.DestroyPlayerObjects(other);
       }
    }


    #endregion

    #region Private Methods

    public void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("Loading lobby for ", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("SampleScene");
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Application.Quit();
    }

    #endregion
}
