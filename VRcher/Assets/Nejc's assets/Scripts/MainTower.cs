using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    private AudioSource GameOverAudio;
    
    public GameObject GameMaster;
    private int towerHealth;
    
    public Rigidbody rb;

    void Start()
    {
        towerHealth = 8;
        GameOverAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(towerHealth <= 0){
            GameMaster.GetComponent<GameManagerMain>().towerDead();
            rb.detectCollisions = false;
        }
        Debug.Log(towerHealth);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag  == "Enemy")
        {
            towerHealth -= 1;
        }
    }

}
