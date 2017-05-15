using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Detonator))]
[AddComponentMenu("Detonator/Fireball")]
public class DetonatorFireball : DetonatorComponent
{
	private float _baseSize = 1f;
	private float _baseDuration = 3f;
	private Color _baseColor = new Color(1f, .423f, 0f, .5f);
	private Vector3 _baseVelocity = new Vector3(60f, 60f, 60f);
//	private float _baseDamping = 0.1300004f;
	private float _scaledDuration;

	private GameObject _fireballA;
	private DetonatorBurstEmitter _fireballAEmitter;
	public Material fireballAMaterial;
	
	private GameObject _fireballB;
	private DetonatorBurstEmitter _fireballBEmitter;
	public Material fireballBMaterial;
	
	private GameObject _fireShadow;
	private DetonatorBurstEmitter _fireShadowEmitter;
	public Material fireShadowMaterial;
	
	public bool drawFireballA = true;
	public bool drawFireballB = true;
	public bool drawFireShadow = true;
	
	override public void Init()
	{
		//make sure there are materials at all
		FillMaterials(false);
		BuildFireballA();
		BuildFireballB();
		BuildFireShadow();
	}
	
	//if materials are empty fill them with defaults
	public void FillMaterials(bool wipe)//Si no hi ha cap material els omple o en cas de voler netejar
	{
		if (!fireballAMaterial || wipe)
		{
			fireballAMaterial = MyDetonator().fireballAMaterial;
		}
		if (!fireballBMaterial || wipe)
		{
			fireballBMaterial = MyDetonator().fireballBMaterial;
		}
		if (!fireShadowMaterial || wipe)
		{
			if (Random.value > 0.5)
			{
				fireShadowMaterial = MyDetonator().smokeAMaterial;
			}
			else
			{
				fireShadowMaterial = MyDetonator().smokeBMaterial;
			}
		}
	}
	
	private Color _detailAdjustedColor;
	
	//Build these to look correct at the stock Detonator size of 10m... then let the size parameter
	//cascade through to the emitters and let them do the scaling work... keep these absolute.
    public void BuildFireballA()
    {
		_fireballA = new GameObject("FireballA");
		_fireballAEmitter = (DetonatorBurstEmitter)_fireballA.AddComponent<DetonatorBurstEmitter>();//Representa les caracteristiques de _fireballA
		_fireballA.transform.parent = this.transform;//Modifica la posicio de la bola de foc a la del jugador
		_fireballA.transform.localRotation = Quaternion.identity;//modifica la  rotacio
		_fireballAEmitter.material = fireballAMaterial;//afegeix el material a l'emisio
		_fireballAEmitter.useWorldSpace = MyDetonator().useWorldSpace;
		_fireballAEmitter.upwardsBias = MyDetonator().upwardsBias;//Alçament parcial
    }
	
	public void UpdateFireballA()
	{
		_fireballA.transform.localPosition = Vector3.Scale(localPosition,(new Vector3(size, size, size)));//Modifica la posicio de la flama
		_fireballAEmitter.color = color;//Dona un color a l'emisio
		_fireballAEmitter.duration =  duration * .5f;//La duracio de l'ona explosiva
		_fireballAEmitter.durationVariation =  duration * .5f;//La duracio de l'ona explosiva
		_fireballAEmitter.count = 2f;//el numero d'explosions
		_fireballAEmitter.timeScale = timeScale;//el temps
		_fireballAEmitter.detail = detail;//la quantitat de detall
		_fireballAEmitter.particleSize = 14f;//el tamany de les particules
		_fireballAEmitter.sizeVariation = 3f;//la variacio
		_fireballAEmitter.velocity = velocity;//velocitat de la bola de foc
		_fireballAEmitter.startRadius = 4f;//radi de l'ona
		_fireballAEmitter.size = size;		//tamany
		_fireballAEmitter.useExplicitColorAnimation = true;

		//make the starting colors more intense, towards white
		Color fadeWhite = new Color(1f, 1f, 1f, .5f);
		Color fadeRed = new Color(.6f, .15f, .15f, .3f);
		Color fadeBlue = new Color(.1f, .2f, .45f, 0f);
		//Creacio de 5 colors diferents alhora d'emetre l'explosio
		_fireballAEmitter.colorAnimation[0] = Color.Lerp(color, fadeWhite, .8f);//Transicio de colors durant l'explosio
		_fireballAEmitter.colorAnimation[1] = Color.Lerp(color, fadeWhite, .5f);//Transicio de colors durant l'explosio
		_fireballAEmitter.colorAnimation[2] = color;
		_fireballAEmitter.colorAnimation[3] = Color.Lerp(color, fadeRed, .7f);//Transicio de colors durant l'explosio
		_fireballAEmitter.colorAnimation[4] = fadeBlue;
		//El temps de retard minima i maxima del'explosio
		_fireballAEmitter.explodeDelayMin = explodeDelayMin;
		_fireballAEmitter.explodeDelayMax = explodeDelayMax;
	}
	
	public void BuildFireballB()
    {
		_fireballB = new GameObject("FireballB");//Creacio del gameobject encarregat de crear l'explosio
		_fireballBEmitter = (DetonatorBurstEmitter)_fireballB.AddComponent<DetonatorBurstEmitter>();//Representara les caracteristiques de l'explosio b
		_fireballB.transform.parent = this.transform;//Modifica la posicio de la bola de foc a la del jugador
		_fireballB.transform.localRotation = Quaternion.identity;//Modifica la rotacio
		_fireballBEmitter.material = fireballBMaterial;//Li afageix un material tenint una textura particular
		_fireballBEmitter.useWorldSpace = MyDetonator().useWorldSpace;
		_fireballBEmitter.upwardsBias = MyDetonator().upwardsBias;//Alçament parcial
    }
	
	public void UpdateFireballB()
{
		_fireballB.transform.localPosition = Vector3.Scale(localPosition,(new Vector3(size, size, size)));//Modifica la posicio de la flama
		_fireballBEmitter.color = color;//El color de l'emissio
		_fireballBEmitter.duration =  duration * .5f;//La duracio de l'ona explosiva
		_fireballBEmitter.durationVariation = duration * .5f;//La duracio de l'ona explosiva
		_fireballBEmitter.count = 2f;//El numero d'explosions
		_fireballBEmitter.timeScale = timeScale;//el temps
		_fireballBEmitter.detail = detail;//la quantitat de detall
		_fireballBEmitter.particleSize = 10f;//el tamany de les particules
		_fireballBEmitter.sizeVariation = 6f;//la variacio
		_fireballBEmitter.velocity = velocity;//velocitat de la bola de foc
		_fireballBEmitter.startRadius = 4f;//radi de l'ona
		_fireballBEmitter.size = size;//Modifica el tamany de l'emissio
		_fireballBEmitter.useExplicitColorAnimation = true;

		//make the starting colors more intense, towards white
		Color fadeWhite = new Color(1f, 1f, 1f, .5f);
		Color fadeRed = new Color(.6f, .15f, .15f, .3f);
		Color fadeBlue = new Color(.1f, .2f, .45f, 0f);
		//Creacio de 5 colors diferents alhora d'emetre l'explosio
		_fireballBEmitter.colorAnimation[0] = Color.Lerp(color, fadeWhite, .8f);//Transicio de colors durant l'explosio
		_fireballBEmitter.colorAnimation[1] = Color.Lerp(color, fadeWhite, .5f);//Transicio de colors durant l'explosio
		_fireballBEmitter.colorAnimation[2] = color;
		_fireballBEmitter.colorAnimation[3] = Color.Lerp(color, fadeRed, .7f);//Transicio de colors durant l'explosio
		_fireballBEmitter.colorAnimation[4] = fadeBlue;
		//El temps de retard minima i maxima del'explosio
		_fireballBEmitter.explodeDelayMin = explodeDelayMin;
		_fireballBEmitter.explodeDelayMax = explodeDelayMax;
	}
	
	public void BuildFireShadow()
    {
		_fireShadow = new GameObject("FireShadow");//Creacio del gameobject
		_fireShadowEmitter = (DetonatorBurstEmitter)_fireShadow.AddComponent<DetonatorBurstEmitter>();
		_fireShadow.transform.parent = this.transform;
		_fireShadow.transform.localRotation = Quaternion.identity;
		_fireShadowEmitter.material = fireShadowMaterial;
		_fireShadowEmitter.useWorldSpace = MyDetonator().useWorldSpace;
		_fireShadowEmitter.upwardsBias = MyDetonator().upwardsBias;
    }
	
	public void UpdateFireShadow()
	{
		_fireShadow.transform.localPosition = Vector3.Scale(localPosition,(new Vector3(size, size, size)));
		
			//move slightly towards the main camera so it sorts properly
		_fireShadow.transform.LookAt(Camera.main.transform);
		_fireShadow.transform.localPosition = -(Vector3.forward * 1f);
		
		_fireShadowEmitter.color = new Color(.1f, .1f, .1f, .6f);
		_fireShadowEmitter.duration = duration * .5f;
		_fireShadowEmitter.durationVariation = duration * .5f;
		_fireShadowEmitter.timeScale = timeScale;
		_fireShadowEmitter.detail = 1; //don't scale up count
		_fireShadowEmitter.particleSize = 13f;
		_fireShadowEmitter.velocity = velocity;
		_fireShadowEmitter.sizeVariation = 1f;
		_fireShadowEmitter.count = 4;
		_fireShadowEmitter.startRadius = 6f;
		_fireShadowEmitter.size = size;
		_fireShadowEmitter.explodeDelayMin = explodeDelayMin;
		_fireShadowEmitter.explodeDelayMax = explodeDelayMax;
	}

    public void Reset()//Reseteja les opcions del fireball
    {
		FillMaterials(true);
		on = true;
		size = _baseSize;
		duration = _baseDuration;
		explodeDelayMin = 0f;
		explodeDelayMax = 0f;
		color = _baseColor;
		velocity = _baseVelocity;
    }

    override public void Explode()//Metode que provocara l'explosio
    {
		if (detailThreshold > detail) return;
		
		if (on)
		{
			UpdateFireballA();
			UpdateFireballB();
			UpdateFireShadow();
			if (drawFireballA)	_fireballAEmitter.Explode();
			if (drawFireballB) _fireballBEmitter.Explode();
			if (drawFireShadow)	_fireShadowEmitter.Explode();
		}
    }

}
