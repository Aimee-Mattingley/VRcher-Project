using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    Collider m_ObjectCollider;
    private AudioSource GameOverAudio;
    public GameObject GameMaster;
    private int towerHealth;
    // Start is called before the first frame update
    void Start()
    {
        towerHealth = 4;
        GameOverAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(towerHealth <= 0){
            GameMaster.GetComponent<GameManagerMain>().towerDead();
            m_ObjectCollider.isTrigger = true;
            GameOverAudio.Play();
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
