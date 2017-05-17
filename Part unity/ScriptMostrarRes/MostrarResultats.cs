
	
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

public class MostrarResultats : MonoBehaviour {
    public Text port;
    private List<String> llista = new List<String>();
	Puntuacions p;
	List<Puntuacions> ListP = new List<Puntuacions>();
	string url = "http://anfifus.000webhostapp.com/ProjectUnity/MostrarDadesPerUnity.php";
	// Use this for initialization
	void Start () {
		
        StartCoroutine(MetodeMostrar());
    }

    private IEnumerator MetodeMostrar()
    {
        //Permet aconseguir la localitzacio del fitxer
        WWW form = new WWW(url);
        //Para a la linea fins que obtingui el valor
        yield return form;
        //Si form no hi ha error permet obtenir els valors rebuts de la pagina
        if (form.error == null)
        {

            //Obte els resultats rebuts de la pagina
            var splitText = form.text.Split(';');//Divideix les dades de cada jugador
            int cont = 0;
			while(cont < splitText.Length - 1)//Recorre les diferentes puntuacions que han sigut separats per ;
            { 
				var contingut = splitText [cont].Split (':');//Divideix les diferents dades del jugador id, nom usuari, contrasenya i marcador

            

				Puntuacions p = new Puntuacions(contingut[0], contingut[1], contingut[2], contingut[3]);//Les dades les introduim a una classe anomenada puntuacions
               
                    ListP.Add(p);//Guardem la puntuacio a una llista
				cont++;
            }
		}
        else
        {
            port.text = form.error;//Mostra l'error al control Text
        }

    }
	void OnGUI()//Metode necessari per tractar les diferents gui
	{
		GUIStyle style = new GUIStyle ();//Crea un estil per la label 
		style.normal.textColor = Color.blue;//Indica el color del text
		//Creacio dels titols que permet identificar les dades
		GUI.Label (new Rect (10, 0 * 32, 128, 32), "Id usuari",style);
		GUI.Label (new Rect (128, 0 * 32, 128, 32), "Nom usuari",style);
		GUI.Label (new Rect (256, 0 * 32, 128, 32), "Contrasenya",style);
		GUI.Label (new Rect (384+100, 0 * 32, 128, 32), "Resultat marcador",style);
		//-----------------
		int cont = 1;
		foreach (Puntuacions p in ListP) {//Aconseguim les puntuacions una per una per poder mostrar-les per pantalla
			GUI.Label (new Rect (10, cont * 32, 128, 32), p.getIdJugador(),style);
			GUI.Label (new Rect (128, cont * 32, 128, 32), p.getNomUsuari(),style);
			GUI.Label (new Rect (256, cont * 32, 128+100, 32), p.getContrasenya(),style);
			GUI.Label (new Rect (384+100, cont * 32, 128, 32), p.getResultatMarcador(),style);
			cont++;
		}
		GUIStyle styleButton = new GUIStyle ();
		style.normal.textColor = Color.cyan;
		if (GUI.Button (new Rect (10, cont * 32, 128, 32), "Sortir", styleButton)) {
			sortir ();
		}

	}
	void sortir()
	{
		SceneManager.LoadScene ("Menu");
	}
}
