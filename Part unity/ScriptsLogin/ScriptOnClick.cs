using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptOnClick : MonoBehaviour {
    public Button btn;
    public InputField user;
    public InputField pass;
	public Text error;
    private string url = "http://anfifus.000webhostapp.com/ProjectUnity/login.php";
    private int id;
    private string nom;
    private string contra;
    private Button btnEvent;
    public GameObject guardador;
    // Use this for initialization
    void Start () {
        btnEvent = btn.GetComponent<Button>();
        btnEvent.onClick.AddListener(TaskOnClick);
        
        //Debug.LogError("L'id es:" + id);
    }
	
	// Update is called once per frame
	void TaskOnClick() {
        
        //btnEvent.gameObject.AddComponent<EnviamentDades>();
        StartCoroutine(enviarDades());

    }

    private IEnumerator enviarDades()
    {
        nom = user.text;
        contra = pass.text;

        var form = new WWWForm();
        /*form.AddField("nom", "anfifus");
        form.AddField("pass", "a1991123456");*/
		form.AddField("nom", nom);
        form.AddField("pass", contra);
        var enviar = new WWW("http://anfifus.000webhostapp.com/ProjectUnity/login.php", form);

        yield return enviar;//Permet esperar i recollir un resultat
		if (enviar.error != null)//En cas de ser null vol dir que no ha donat error
        {
            if (error.text != "")
                error.text = error.text.Remove (0);
            
			    error.text = "Errors: "+ enviar.error;
        }
        else
        {
			if (enviar.text == "NULL\n") {//Amb aquest resultat vol dir que no existeix el nom d'usuari o el password
                if(error.text != "")
                   error.text = error.text.Remove(0);
                
                   error.text += "Errors: No s'ha trobat cap usuari";
			} 
			else {
					id = int.Parse (enviar.text);//Converteix el string rebut per un enter
					print ("Id: " + id);//Mostra l'enter per consola
	            
					var var1 = guardador.GetComponent<GuardaVariables> ();//Agafa el script GuardaVariable i l'assigne
					var1.setId (id);//Envia la id rebuda a la variable
					print ("ID: " + var1.getId ());//Mostra per consola la id guardada dins de la variable
					SceneManager.LoadScene ("Practica");//Canvia d'escena concretament a una anomenada Practica
			}
        }
    }
}
