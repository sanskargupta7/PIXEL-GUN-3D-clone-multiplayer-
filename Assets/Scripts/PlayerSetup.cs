using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{


    [SerializeField]
    GameObject FPSCamera;

    [SerializeField]
    TextMeshProUGUI playerNameText;
    
    
    
    // Start is called before the first frame update
    void Start()
    {


        if(photonView.IsMine)     //if this is true then i am the master and only my player is running
        {

            transform.GetComponent<MovementController>().enabled = true;
            FPSCamera.GetComponent<Camera>().enabled = true;
        }
        else                      //if false then both scripts are inactive
        {

            transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;

        }

        SetPlayerUI();

    }

    void SetPlayerUI()
    {

        if(playerNameText!=null)
        {

            playerNameText.text = photonView.Owner.NickName;

        }
    }

    
}
