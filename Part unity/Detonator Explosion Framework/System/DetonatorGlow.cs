using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Detonator))]
[AddComponentMenu("Detonator/Glow")]
public class DetonatorGlow : DetonatorComponent
{
	private float _baseSize = 1f;
	private float _baseDuration = 1f;
	private Vector3 _baseVelocity = new Vector3(0f, 0f, 0f);
	private Color _baseColor = Color.black;
	private float _scaledDuration;
	
	private GameObject _glow;
	private DetonatorBurstEmitter _glowEmitter;
	public Material glowMaterial;
		
	override public void Init()
	{
		//make sure there are materials at all
		FillMaterials(false);
		BuildGlow();
	}
	
	//if materials are empty fill them with defaults
	public void FillMaterials(bool wipe)//Si el material esta buit l'omple perque pugui brillar
	{
		if (!glowMaterial || wipe)
		{
			glowMaterial = MyDetonator().glowMaterial;
		}
	}
	
	//Build these to look correct at the stock Detonator size of 10m... then let the size parameter
	//cascade through to the emitters and let them do the scaling work... keep these absolute.
    public void BuildGlow()
    {
		_glow = new GameObject("Glow");//Crea objecte glow
		_glowEmitter = (DetonatorBurstEmitter)_glow.AddComponent<DetonatorBurstEmitter>();//Afegim el script DetonatorBurstEmitter
		_glow.transform.parent = this.transform;
		_glow.transform.localPosition = localPosition;
		_glowEmitter.material = glowMaterial;//Li passem el material que brilla
		_glowEmitter.exponentialGrowth = false;//Indica si hi haura un creixement 
		_glowEmitter.useExplicitColorAnimation = true;//Si haura o no autofade
		_glowEmitter.useWorldSpace = MyDetonator().useWorldSpace;//Quan esta activat lesparticules no es mouen quan l'emisor es mou
		
    }
	
	public void UpdateGlow()
	{
		//this needs
		_glow.transform.localPosition = Vector3.Scale(localPosition,(new Vector3(size, size, size)));//Modifica la posicio local
		
		_glowEmitter.color = color;//El color de les particules
		_glowEmitter.duration = duration;//la duracio
		_glowEmitter.timeScale = timeScale;//
		_glowEmitter.count = 1;
		_glowEmitter.particleSize = 65f;//el tamany de les particules
		_glowEmitter.randomRotation = false;//rotacio aleatoria
		_glowEmitter.sizeVariation = 0f;//variacio del tamany
		_glowEmitter.velocity = new Vector3(0f, 0f, 0f);//velocitat
		_glowEmitter.startRadius = 0f;//començament del radi
		_glowEmitter.sizeGrow = 0;//tamany que creixera la particula
		_glowEmitter.size = size;	//tamany de la particula
		_glowEmitter.explodeDelayMin = explodeDelayMin;//el temps de retras minim
		_glowEmitter.explodeDelayMax = explodeDelayMax;//el temps de retras maxim

		Color stage1 = Color.Lerp(color, (new Color(.5f, .1f, .1f, 1f)),.5f);//COMPROVAR Creacio del color?
		stage1.a = .9f;//Asignacio d'un nou component alpha
		
		Color stage2 = Color.Lerp(color, (new Color(.6f, .3f, .3f, 1f)),.5f);
		stage2.a = .8f;
		
		Color stage3 = Color.Lerp(color, (new Color(.7f, .3f, .3f, 1f)),.5f);
		stage3.a = .5f;
		
		Color stage4 = Color.Lerp(color, (new Color(.4f, .3f, .4f, 1f)),.5f);
		stage4.a = .2f;
		
		Color stage5 = new Color(.1f, .1f, .4f, 0f);
		
		_glowEmitter.colorAnimation[0] = stage1;//Asignacio del color a l'emisor
		_glowEmitter.colorAnimation[1] = stage2;
		_glowEmitter.colorAnimation[2] = stage3;
		_glowEmitter.colorAnimation[3] = stage4;
		_glowEmitter.colorAnimation[4] = stage5;
	}

	void Update () 
	{
		//others might be able to do this too... only update themselves before exploding?
	}

    public void Reset()//Reseteja les opcions del detonatorGlow
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

    override public void Explode()//S'encarrega de l'explosio i que emeti llum
    {
		if (detailThreshold > detail) return;
		
		if (on)
		{
			UpdateGlow();
			_glowEmitter.Explode();
		}
    }

}
