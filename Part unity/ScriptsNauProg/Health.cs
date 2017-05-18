using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour { 

    // Use this for initialization
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public GameObject healthBar;
    public GameObject jugador;
    public GameObject enemic;
	public GameObject itemVida;
	//public GameObject Camara;

    void Start () {
		cur_Health = max_Health;//Inicialitzem vida actual igualant-la a la vida maxima
    }
    
	void OnCollisionEnter(Collision theCollision)//Metode especial que detecta la col·lisio
    {
		if(theCollision.gameObject == enemic)//Si hi ha col·lisio del jugador a l'enemic
        {
			if(cur_Health > 0)//Si la vida no ha arribat a 0 o es igual a 0
				decreaseHealth();//Metode disminueix vida
        }
        
		if (theCollision.gameObject == itemVida) {//Si hi ha col·lisio del gameobject al itemVida
			
			increaseHealth ();//Metode augmenta la vida

		}
    }
    public void decreaseHealth()
    {
		cur_Health = cur_Health - 10;//Disminueix la vida per deu en aquest cas
		float calc_Health = cur_Health / max_Health;//Dividim la vida actual per la maxima degut al desplaçament de la barra de vida. Tot aixo es per fer l'efecte de moviment, el qual, es de 0 a 1
		setHealthBar(calc_Health);//Metode on es modificara la quantitat de vida de forma visual
    }
	public void increaseHealth()
	{
		if (cur_Health < 50) {//Si la vida actual es menor a 50
			cur_Health = cur_Health + 50;//S'omple 50 de vida
		} else {
			cur_Health = max_Health;//S'omple la vida actual per la quan tenia completa
		}
		float calc_Health = cur_Health / max_Health;//Dividim la vida actual per la maxima degut al desplaçament de la barra de vida. Tot aixo es per fer l'efecte de moviment, el qual, es de 0 a 1
		setHealthBar(calc_Health);//Entra el metode on es modificara la quantitat de vida de forma visual
		GameObject.Destroy (itemVida);//Destrueix l'objecte itemVida
	}
	/// <summary>
	/// Modificacio de la vida
	/// </summary>
	/// <param name="myHealth">My health.</param>
    private void setHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);//Nomes es modifica la barra de vida desplaçant cap a l'esquerra pertant el valor rebut per la funcio sera per modificar l'escalat de les x
        if (myHealth <= 0)
        {
            jugador.AddComponent<Detonator>().Explode();//Obliga a crear un efecte d'explosio
			SceneManager.LoadScene ("Menu");
        }
        
    }
    // Update is called once per frame
    void Update () {
		
	}

    
}
