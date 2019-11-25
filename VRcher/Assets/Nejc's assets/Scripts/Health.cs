using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health = 2;

    // Start is called before the first frame update
    public void Damage(int dam)
    {
        health -= dam;
        if(health <= 0){
            Destroy(gameObject);
        }
        
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
