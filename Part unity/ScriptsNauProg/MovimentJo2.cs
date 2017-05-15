using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentJo2 : MonoBehaviour {

    private Rigidbody rigidBody;
    public float RollEffect = 50f;//Gir
    public float PitchEffect = 50f;
    private float speed = 0;
    private float speedRot = 5;
    private float rotationForce = 1;
    private Quaternion antigaRotacio;
    private float VelocRotacio = 0;
    private Vector3 rotacioDreta;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rotacioDreta = new Vector3(0,VelocRotacio,0);

    }
    //Hem de saber qual es el punt esquerra i dret per establir com a limit

    //Hem se saber si esta accelerant i girant llavors fer un gir continuu de 30 graus i quan deixi de presionar torna la save posicio natural
    // Update is called once per frame
    void FixedUpdate () {
        if(Input.GetKey(KeyCode.Z))
        {
            
            rigidBody.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);//Agafara la posicio actual del jugador i la sumara amb la posicio davantera afegint un temps i una velocitat
            
            if(speed <  35)
            {
                speed++;
            }
        }
        else
        {
            speed = 0;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //Go left
            if(VelocRotacio <= 150f)//Velocitat de rotacio maxima
            {
                VelocRotacio += 5f;//Incrementa la velocitat de rotacio
            }
            
            var posSeg = (-transform.right + transform.position) - transform.position;//Moviment que fara cap a l'esquerra
            var rotacio = Quaternion.LookRotation(posSeg);//La rotacio que fara observant la posicio senyalada
          
            rigidBody.MoveRotation(Quaternion.Slerp(transform.rotation,rotacio,Time.fixedDeltaTime*speedRot));//Rotacio que fara de forma realista
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //Go right
			if (VelocRotacio <= 150f)//Velocitat de rotacio maxima
            {
				VelocRotacio += 5f;//Incrementa la velocitat de rotacio
            }

			var posSeg = (transform.right + transform.position) - transform.position;//Moviment que fara cap a la dreta
			var rotacio = Quaternion.LookRotation(posSeg);//La rotacio que fara observant la posicio senyalada

			rigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, rotacio, Time.fixedDeltaTime * speedRot));//Rotacio que fara de forma realista

        }
       
    }
}
