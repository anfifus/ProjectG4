using UnityEngine;
using System.Collections;
using System;
//using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;

public class GuanyarPartida : MonoBehaviour {
	public List<Transform> path;
	public Transform pathGroup;
	private int currentPath = 0;
	private float distFromPath = 150f;
	private DateTime timeIni;
	private DateTime timeFin;
	private int lap = 1;
    private string url = "http://anfifus.000webhostapp.com/ProjectUnity/insertIntoBDVictories.php";
    //private string url = "http://anfifus.260mb.net/ProjectUnity/insertIntoBDVictories.php";//sql202.260mb.net
    [SerializeField]private int totalLap = 1;
    //[SerializeField]private GameObject guardar;
    // Use this for initialization
    void Start () {
		timeIni = DateTime.Now;//Aconsegueix el temps inicial
	}
	
	// Update is called once per frame
	void Update () {
		getPath ();//Aconsegueix el path
		getSteer ();
	}
	private void getPath()
	{
		var paths = pathGroup.GetComponentInChildren<Transform> ();//Aconsegueix els punts que formen el cami
		path = new List<Transform> ();//Creacio d'una llista pels punts
		foreach (Transform sub in paths) {
			if (sub != pathGroup) {//Afegim tots els punts menys el contenidor
				path.Add (sub);

			}
		}
	}



	private void getSteer()
	{
		Vector3 steerVector = transform.InverseTransformPoint (new Vector3(path[currentPath].position.x,transform.position.y,path[currentPath].position.z));//Aconseguim la posicio dels punts a la y del vehicle
		if (steerVector.magnitude <= distFromPath) {//Si la distancia es menor o igual al limit establert
			currentPath++;//Incrementa el path anant el seguent
            Debug.Log("Lap arribat: "+currentPath);
            Debug.Log("Lap limit: " + path.Count);
            if (currentPath >= path.Count)//Di el punt en el qual es troba el vehicle es major o igual al total vol dir que ha arribat al final 
				{
					currentPath = 0;//Reinicia el contador
						if (lap == totalLap)//Total lap representa el num maxim de voltes a fer 
						{
							timeFin = DateTime.Now;//Aconsegueix l'ultim temps
							TimeSpan tempsTrigat = timeFin.TimeOfDay - timeIni.TimeOfDay;//Restem el temps final - l'inicial i sabrem la duracio
														
                           provaPHP(tempsTrigat);//Metode on s'enviara el temps trigat                            
                }
				lap++;
			}
		}
	}

    private void provaPHP(TimeSpan tempsTrigat)
    {
        int hores = tempsTrigat.Hours;//Aconseguim les hores del temps trigat
		int min = tempsTrigat.Minutes;//Aconseguim les minuts del temps trigat
		int sec = tempsTrigat.Seconds;//Aconseguim les segons del temps trigat
        Debug.LogError("Funciona: " + hores + "," + min + "," + sec);

        var form = new WWWForm();//Inciem un wwwform
        var guardaId = GameObject.Find("Guardador");//Trobem el gameobject guardador que contenia la id
        int id = guardaId.GetComponent<GuardaVariables>().getId();//Aconseguim la id de l'usuari
		print("La id final es: "+id);
        form.AddField("id", id);//Enviem la id
        form.AddField("Hores", hores);//Enviem les hores tardades
        form.AddField("Minuts", min);//Enviem els minuts tardats
        form.AddField("Segons", sec);//Enviem els segons tardats
        var PasInfo = new WWW(url, form);//Li passem els valors al fitxer php que volem utilitzar
   
		Destroy (guardaId);//Destruim el gameobject guardador per si volem fer una altre partida
        SceneManager.LoadScene("Puntuacio");//Carrega la seguent escena
    }
}
