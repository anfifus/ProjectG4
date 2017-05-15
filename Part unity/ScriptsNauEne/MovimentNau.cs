using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovimentNau : MonoBehaviour {

    float oldDistance = 0F;
    float distancia = 0F;

    int currentPathObj;
	float maxSteer = 15f;
	float impuls = 0;
	float aeroFactor = 1;
	Vector3 posicioPunt;
	private float posX;
	private float posZ;
	//Totes les caracteristiques dels sensors que serveixen per evitar el mur
	public float sensorLength = 3f;
    public float sensorLengthRight = 8f;
    public float sensorLengthLeft = 8f;
	public float frontSensorStart = 1;
	public float frontSideSensorStart = 1;
	public float SideSensorStart = 1;
	public float frontSensorAngle = 30;
	//-----------------------------------
	//List<Transform>NausEnemigues = GameObject.Find("Enemies").GetComponentsInChildren<Transform>();
	List<Transform> PathNau;//Les posicions per on passara la  nau
	int posicioActual = 0;//Posicio referent al path
	int flag ;
    int cont = 1;
    float distFromPath = 20;
	float avoidObstacles = 0;
    bool rotate = true;
    float speed = 5f;
    Vector3 inicial;
    Vector3 destination;
    Transform enemic;
    Rigidbody control;
    float posY;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3( PathNau[posicioActual].position.x,transform.position.y, PathNau[posicioActual].position.z);
        destination = transform.position;//Posicio inicial de l'enemic
        control = transform.GetComponent<Rigidbody>();
        posY = enemic.position.y;
    }
	/// <summary>
	/// Crea enemics
	/// </summary>
	/// <param name="personalPos">Create enemy.</param>
	public void personalPos(float posX,float posZ,Transform enemic)//Col·locacio de l'enemic al circuit
	{
		getPath ();
        this.enemic = enemic;
 

	}
	/// <summary>
	/// Aconsegueix el punt de l'origen de l'enemic i servira per poder donar orientacio a l'enemic.
	/// <param name="getPath">Gets the path.</param>
	/// </summary>
	private void getPath ()//Creem el path
	{
        var pathParent = GameObject.Find("Path");
        
        var paths = pathParent.GetComponentsInChildren<Transform>();
		PathNau = new List<Transform> ();
		foreach (Transform path in paths) {
            if (path != pathParent.transform)//Si el path no correspont el contenidor de recorregut
            {
                PathNau.Add(path);//Aconsegueix cada path dintre del contenidor
            }
		}
	}
		
	// Update is called once per frame
	void FixedUpdate () {
        if (flag == 0) {        
            moviment();
            }
        Sensors();
    }

	void moviment()
	{ 
        
        if (speed < 10)//Si la velocitat es menor a 10 incrementa en cas contrari no ho fara
        {
            speed += 0.1f;
        }

        if (cont >= PathNau.Count)//Si el contador de recorregut de la nau es major o igual al numero maxim a recorre llavors el baixara a zero
        {
            cont = 0;
        }
        else
        {
            
            distancia = Vector3.Distance(enemic.position, PathNau[cont].position);//Aconsegueix la distancia entre la nau i el seguent punt
            
            if (distancia > 4f)//Si la distancia es major a 4
            {
                var posicionar = PathNau[cont].position - enemic.position;//Obtindrem la posicio entre el seguent punt i la nau
                posicionar.y = 0;//Posicio de la nau respecta l'eix y de l'altura 0 per no sobrepassar a la barrera
                var rotacio = Quaternion.LookRotation(posicionar);//Creara una rotacio tenint referencia el punt seguent i la seva posicio
                control.MoveRotation(Quaternion.Slerp(enemic.rotation, rotacio, Time.fixedDeltaTime * speed ));//Mou tenint en compte la rotacio de la nau segons la velocitat

                control.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);//Mou de posicio segons la velocitat

            }
            else
            {
                cont++;//Incrementa la posicio dintre del cami a anar
            }
        }
			
    }

    /// <summary>
    /// Canvia l'angle quan es mou
    /// </summary>
    /// <param name="position"></param>

    void Sensors(){
  
        Vector3 ori;//Vector origen
		RaycastHit hit ;//Crea un raig que dona informacio
         flag = 0;
        if(cont >= PathNau.Count)
        {
            cont = 0;
        }
         distancia = Vector3.Distance(enemic.position, PathNau[cont].position);//Aconsegueix la distancia entre la nau i el seguent punt
        

        Vector3 angleDret = Quaternion.AngleAxis (frontSensorAngle,enemic.up)*enemic.forward;//Aconseguim l'angle dret
		Vector3 angleEsquerra = Quaternion.AngleAxis (-frontSensorAngle, enemic.up) * enemic.forward;//Utilitzem enemic en comptes de transform perque transform els seus axis son incorrectes

		    //Sensor braking

		    ori = enemic.position;//Posicio origen de l'enemic
		    ori += enemic.forward * frontSensorStart;//posicio de sortida del raig

		    if (Physics.Raycast (ori, enemic.forward, out hit, sensorLength)) {
                Debug.DrawLine(ori,hit.point,Color.grey);
		    } 
		    

		//Sensor al mig i al davant dret


		//---------------------
		ori = enemic.position;
		ori += enemic.forward * frontSideSensorStart;//Col·loca a la part dreta de la nau el raig

       
            if (Physics.Raycast(ori, angleDret, out hit, sensorLengthRight) && distancia > 4)//Phisics.Raycast permet condicionar el fet quan el ragi detecti una superficie
            {//Aquest cas detecta segons l'angle de davant dret i a una distancia superior a 4				                
                    flag++;//Permet fer que no topi contra un mur
                    var posicio = (-transform.right+transform.position) - transform.position;//Rectifica la posicio cap a l'esquerra
                    var rotacio = Quaternion.LookRotation(posicio);//Crea una rotacio amb la nova posicio
                    control.MoveRotation(Quaternion.Slerp(enemic.transform.rotation, rotacio, Time.fixedDeltaTime * 10));//Rota el rigidbody tenint en compte la rotació actual de l'enemic i la nova
                    Debug.DrawLine(ori, hit.point, Color.black);         
            }
            else
            {
                flag = 0;
            }
         

            //Part esquerra frontal
            ori = enemic.position;
            ori -= enemic.forward * frontSideSensorStart;//Col·loca a la part esquerra de la nau el raig

        if (Physics.Raycast(ori, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(ori, hit.point, Color.red);
            }
            else
            {
                if (Physics.Raycast(ori, angleEsquerra, out hit, sensorLengthLeft) && distancia > 4)
                {
              		Debug.DrawLine(ori, hit.point, Color.green);
                    //Debug.Log("Gira a la dreta");
                    flag++;
					var rot = (transform.right+transform.position) - transform.position;//Rectifica la posicio cap a la dreta
					var rotacio = Quaternion.LookRotation(rot);//Crea una rotacio amb la nova posicio
					control.MoveRotation(Quaternion.Slerp(enemic.transform.rotation, rotacio, Time.fixedDeltaTime * 10));//Rota el rigidbody tenint en compte la rotació actual de l'enemic i la nova              
                }
                else
                {
                 flag = 0;
                }                
            }
        }
}