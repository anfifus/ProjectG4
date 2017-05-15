using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardaVariables : MonoBehaviour {
    public static GuardaVariables guardaVariables;
    [SerializeField]private int id;
	void Awake()
    {
        if (guardaVariables == null)//Comprova que no hi hagi cap valor
        {
            guardaVariables = this;//Guarda la variable
            DontDestroyOnLoad(gameObject);//Fa que l'object no es destrueixi quan hi hagi canvi d'escena
        }
        else
        {
                Destroy(gameObject);//Destrueix l'object en cas d'existir
        }
			
    }

    public void setId(int idJugador)//Permet modificar el valor que contindra la id del jugador
    {
        id = idJugador;
    }
    public int getId()//Retorna el valor de la id del jugador
    {
        return id;
    }
}
