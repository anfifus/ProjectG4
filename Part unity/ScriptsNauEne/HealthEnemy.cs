using UnityEngine;
using System.Collections;
using System;

public class HealthEnemy : MonoBehaviour { 

    // Use this for initialization
    public GameObject enemic;
	public GameObject itemVida;
	//public GameObject Camara;

    void Start () {

    }
    
	void OnCollisionEnter(Collision theCollision)//Metode especial que detecta la col·lisio
    {
		
		if (theCollision.gameObject == itemVida) {//Si hi ha col·lisio del gameobject al itemVida

            GameObject.Destroy(itemVida);


        }
    }
   
    // Update is called once per frame
    void Update () {
		
	}

    
}
