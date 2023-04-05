using System;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

namespace a
{
    public class Player : MonoBehaviourPun
    {
        private void Start()
        {
            if (photonView.IsMine)
            {
                GameManager.Instance.SetPlayer(this);
                SetStartPosition();
            }
        }

        private void SetStartPosition()
        {
            photonView.RPC("SetStartPositionRpc", RpcTarget.AllBufferedViaServer, PhotonNetwork.CurrentRoom.PlayerCount);
        }

        [PunRPC]
        public void SetStartPositionRpc(byte index)
        {
            transform.position = Vector3.right * index;
        }

        public void MoveRight()
        {
            photonView.RPC("MoveRightRpc", RpcTarget.AllBufferedViaServer);
        }

        public void MoveLeft()
        {
            photonView.RPC("MoveLeftRpc", RpcTarget.AllBufferedViaServer);
        }

        [PunRPC]
        public void MoveRightRpc()
        {
            transform.Translate(Vector3.right);
        }

        [PunRPC]
        public void MoveLeftRpc()
        {
            transform.Translate(Vector3.left);
        }

        private void OnTriggerEnter(Collider other)
        {
            photonView.RPC("ChangeColorRpc", RpcTarget.AllBufferedViaServer);
        }
        
        [PunRPC]
        public void ChangeColorRpc()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}