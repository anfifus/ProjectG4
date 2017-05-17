using UnityEngine;
using System.IO;
//using System.IO.Directory;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class LlistaEnemics : MonoBehaviour {
	[SerializeField]List<Transform>Enemies;
	int numEnemy = 1;//Numero total d'enemics a crear
	float posX = 0f;
	float posZ = 0f;
	List<GameObject>ListEnemy = new List<GameObject>();
    public GameObject puntOriEne;
	void Awake()
	{
		
	
			string dir2 ="Prefabs/FZeroRacers";//Li passem el directori amb totes les naus
		AssetBundle bundle = new AssetBundle();
		List<GameObject> objs = new List<GameObject>();
		foreach (GameObject o in Resources.LoadAll(dir2,typeof(GameObject))) {
			objs.Add (o);
		}
		int randomNum = Random.Range(0,objs.Count-1);//Agafem un numero que sigui -1 al total de fitxers
		GameObject NauEnemiga = Instantiate(objs.ToArray().GetValue(randomNum)as GameObject);
		NauEnemiga.name = "Nau enemiga";

		puntOriEne.AddComponent<MovimentNau>().personalPos(posX, posZ,puntOriEne.transform);//GameObject el qual fara el moviment
		NauEnemiga.transform.parent = puntOriEne.transform;//Li passem el transform del punt origen de l'enemic
		NauEnemiga.transform.position = puntOriEne.transform.position;//Li passem la posicio del punt origen de l'enemic
		NauEnemiga.transform.localRotation = puntOriEne.transform.rotation;//Li passem la rotacio local del punt origen de l'enemic
		NauEnemiga.transform.localScale = new Vector3(1, 1, 1);//Escala tenint com a referencia el gameobject pare

		ListEnemy.Add(NauEnemiga);
				
       
    }


	// Update is called once per frame
	void Update () {
		foreach(var enemy in ListEnemy)
		{
			//Thread t1 = new Thread (new ThreadStart (enemy.GetComponent<MovimentNau> ().IA));
		}
	}


}