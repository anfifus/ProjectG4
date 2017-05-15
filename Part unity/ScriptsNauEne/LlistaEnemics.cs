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
    public GameObject enemic;
	void Awake()
	{
		
		int cont = 0;//Iniciador de enemics creats
		do 
		{
			
			string dir2 = @"Assets\Resources\FZeroRacers";//Li passem el directori amb totes les naus
			//DirectoryInfo dirInfo = new DirectoryInfo (dir);
			DirectoryInfo dirInfoMesh = new DirectoryInfo (dir2);//Creem el manipulador de directoris 

			FileInfo[] filesMesh = dirInfoMesh.GetFiles("*.dae");//Aconseguim tots els fitxers que acabin amb extensio .dae
			int randomNum = Random.Range(0,filesMesh.Length-1);//Agafem un numero que sigui -1 al total de fitxers

			FileInfo mesh = filesMesh[randomNum];//Aconseguim el fitxer

			string fileMesh = mesh.Name.Replace(".dae","");//Reemplacem l'extensio .dae i li posem un blanc 
			GameObject prova = Instantiate( Resources.Load(@"FZeroRacers\"+fileMesh,typeof(GameObject))as GameObject);//Llavors del FZeroRacers carreguem el fitxer amb el mateix nom i el convertim a un gameobject         
            
			prova.name = "La prova";//GameObject nomes estetic
            
            prova.transform.parent = enemic.transform;
            enemic.AddComponent<MovimentNau>().personalPos(posX, posZ,enemic.transform);//GameObject el qual fara el moviment
            prova.transform.position = enemic.transform.position;
            prova.transform.localRotation = enemic.transform.rotation;
            prova.transform.localScale = new Vector3(1, 1, 1);//Escala tenint com a referencia el gameobject pare
            
            ListEnemy.Add(prova);
			cont++;
            
        }
		while(cont < numEnemy);
       
    }


	// Update is called once per frame
	void Update () {
		foreach(var enemy in ListEnemy)
		{
			//Thread t1 = new Thread (new ThreadStart (enemy.GetComponent<MovimentNau> ().IA));
		}
	}


}