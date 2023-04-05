using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Start");
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "CatsScene")
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log("OnConnectedToMaster");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(string.Empty);
        Debug.Log("OnJoinRandomFailed");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("CatsScene");
            Debug.Log("CatsScene");
        }

        Debug.Log("OnJoinedRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient)
        // {
        //     PhotonNetwork.LoadLevel("NetworkGame");
        // }

        Debug.Log("OnPlayerEnteredRoom");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log(newMasterClient.UserId);
    }
}