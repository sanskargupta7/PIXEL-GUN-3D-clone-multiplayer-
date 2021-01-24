using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TakingDamage : MonoBehaviourPunCallbacks
{

    [SerializeField]
    Image healthBar;



    private float health;
    public float startHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;

        healthBar.fillAmount = health/startHealth;

    }

   
    [PunRPC]                    //calls for all remote clients in the room
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        Debug.Log(PhotonNetwork.NickName + " " + health);

        healthBar.fillAmount = health / startHealth;

        if (health<=0f)
        {
            //Die
            Die();
        }


    }


    void Die()
    {

        if (photonView.IsMine)
        {
            PixelGunGameManager.instance.LeaveRoom();
        }


    }

    //what will happen here is that when the player dies he will leave the room and his gameobject is destroyed....then on OnLeftRoom will get the gameLauncherScene loaded for the killed player. 
}
