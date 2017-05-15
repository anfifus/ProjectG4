using UnityEngine;
using System.Collections;

/*
	DetonatorBurstEmitter is an interface for DetonatorComponents to use to create particles
	
	- Handles common tasks for Detonator... almost every DetonatorComponent uses this for particles
	- Builds the gameobject with emitter, animator, renderer
	- Everything incoming is automatically scaled by size, timeScale, color
	- Enable oneShot functionality

	You probably don't want to use this directly... though you certainly can.
*/

public class DetonatorBurstEmitter : DetonatorComponent
{
    private ParticleEmitter _particleEmitter;
    private ParticleRenderer _particleRenderer;
    private ParticleAnimator _particleAnimator;

	private float _baseDamping = 0.1300004f;
	private float _baseSize = 1f;
	private Color _baseColor = Color.white;
	
	public float damping = 1f;
	public float startRadius = 1f;
	public float maxScreenSize = 2f;
	public bool explodeOnAwake = false;
	public bool oneShot = true;
	public float sizeVariation = 0f;
	public float particleSize = 1f;
	public float count = 1;
	public float sizeGrow = 20f;
	public bool exponentialGrowth = true;
	public float durationVariation = 0f;
	public bool useWorldSpace = true;
	public float upwardsBias = 0f;
	public float angularVelocity = 20f;
	public bool randomRotation = true;
	
	public ParticleRenderMode renderMode;
	
	//TODO make this based on some var
	/*
	_sparksRenderer.particleRenderMode = ParticleRenderMode.Stretch;
	_sparksRenderer.lengthScale = 0f;
	_sparksRenderer.velocityScale = 0.7f;
	*/
	
	public bool useExplicitColorAnimation = false;
	public Color[] colorAnimation = new Color[5];
	
	private bool _delayedExplosionStarted = false;
	private float _explodeDelay;
	
	public Material material;
	
	//unused
	override public void Init() 
	{
		print ("UNUSED");
	} 
	
    public void Awake()
    {
        _particleEmitter = (gameObject.AddComponent<EllipsoidParticleEmitter>()) as ParticleEmitter;
        _particleRenderer = (gameObject.AddComponent<ParticleRenderer>()) as ParticleRenderer;
        _particleAnimator = (gameObject.AddComponent<ParticleAnimator>()) as ParticleAnimator;

		_particleEmitter.hideFlags = HideFlags.HideAndDontSave;//No es mostra dins l'escena i no es guarda dins de la jerarquia
		_particleRenderer.hideFlags = HideFlags.HideAndDontSave;//No es mostra dins l'escena i no es guarda dins de la jerarquia
		_particleAnimator.hideFlags = HideFlags.HideAndDontSave;//No es mostra dins l'escena i no es guarda dins de la jerarquia

		_particleAnimator.damping = _baseDamping;//Amortiment

        _particleEmitter.emit = false;//Particules emitides cada frame
		_particleRenderer.maxParticleSize = maxScreenSize;//Tamany maxim de les particules
        _particleRenderer.material = material;//El material que utilitzara la particula
		_particleRenderer.material.color = Color.white; //workaround for this not being settable elsewhere
		_particleAnimator.sizeGrow = sizeGrow;//La quantitat de tamany que incrementa desde que s'utilitza
		
		if (explodeOnAwake)
		{
			Explode();
		}
    }
	
	private float _emitTime;
	private float speed = 3.0f;
	private float initFraction = 0.1f;
	static float epsilon = 0.01f;
	
	void Update () 
	{
		//do exponential particle scaling once emitted
		if (exponentialGrowth)//Calcula el creixement un cop emes la particula ??
		{
			float elapsed = Time.time - _emitTime;
			float oldSize = SizeFunction(elapsed - epsilon);
			float newSize = SizeFunction(elapsed);
			float growth = ((newSize / oldSize) - 1) / epsilon;
			_particleAnimator.sizeGrow = growth;
		}
		else
		{
			_particleAnimator.sizeGrow = sizeGrow;
		}
		
		//delayed explosion
		if (_delayedExplosionStarted)//Condicio en cas de voler una explosio retardada
		{
			_explodeDelay = (_explodeDelay - Time.deltaTime);//Resta de temps en segons del temps que volem que tardi en explotar menys el temps en segons de l'ultim frame
			if (_explodeDelay <= 0f)//Si el temps es 0 o menor llavors explosionara
			{
				Explode();
			}
		}
	}
	
	private float SizeFunction (float elapsedTime) 
	{
		float divided = 1 - (1 / (1 + elapsedTime * speed));
		return initFraction + (1 - initFraction) * divided;
	}
	
    public void Reset()
    {
		size = _baseSize;
		color = _baseColor;
		damping = _baseDamping;
    }

	
	private float _tmpParticleSize; //calculated particle size... particleSize * randomized size (by sizeVariation)
	private Vector3 _tmpPos; //calculated position... randomized inside sphere of incoming radius * size
	private Vector3 _tmpDir; //calculated velocity - randomized inside sphere - incoming velocity * size
	private Vector3 _thisPos; //handle on this gameobject's position, set inside
	private float _tmpDuration; //calculated duration... incoming duration * incoming timescale
	private float _tmpCount; //calculated count... incoming count * incoming detail
	private float _scaledDuration; //calculated duration... duration * timescale
	private float _scaledDurationVariation; 
	private float _scaledStartRadius; 
	private float _scaledColor; //color with alpha adjusted according to detail and duration
	private float _randomizedRotation;
	private float _tmpAngularVelocity; //random angular velocity from -angularVelocity to +angularVelocity, if randomRotation is true;
	
    override public void Explode()
    {
		if (on)
		{
			_particleEmitter.useWorldSpace = useWorldSpace;//Les particules no es mouen quan el emisor es mou 
			
			_scaledDuration = timeScale * duration;
			_scaledDurationVariation = timeScale * durationVariation;
			_scaledStartRadius = size * startRadius;

			_particleRenderer.particleRenderMode = renderMode;
			
			if (!_delayedExplosionStarted)
			{
				_explodeDelay = explodeDelayMin + (Random.value * (explodeDelayMax - explodeDelayMin));
			}
			if (_explodeDelay <= 0) 
			{
				Color[] modifiedColors = _particleAnimator.colorAnimation;
				
				if (useExplicitColorAnimation)//Color
				{
					modifiedColors[0] = colorAnimation[0];
					modifiedColors[1] = colorAnimation[1];
					modifiedColors[2] = colorAnimation[2];
					modifiedColors[3] = colorAnimation[3];
					modifiedColors[4] = colorAnimation[4];
				}
				else //auto fade
				{//Color
					modifiedColors[0] = new Color(color.r, color.g, color.b, (color.a * .7f));
					modifiedColors[1] = new Color(color.r, color.g, color.b, (color.a * 1f));
					modifiedColors[2] = new Color(color.r, color.g, color.b, (color.a * .5f));
					modifiedColors[3] = new Color(color.r, color.g, color.b, (color.a * .3f));
					modifiedColors[4] = new Color(color.r, color.g, color.b, (color.a * 0f));
				}
				_particleAnimator.colorAnimation = modifiedColors;//Els colors que hem creat anirant ciclant durant el temps de duracio de les particules
				_particleRenderer.material = material;//Asignacio del material
				_particleAnimator.force = force;//La força aplicada a las particules cada frame
				_tmpCount = count * detail;
				if (_tmpCount < 1) _tmpCount = 1;
				
				if (_particleEmitter.useWorldSpace == true)
				{
					_thisPos = this.gameObject.transform.position;
				}
				else
				{
					_thisPos = new Vector3(0,0,0);
				}

				for (int i = 1; i <= _tmpCount; i++)//COMPROVAR amb molt mes deteniment
				{
					_tmpPos =  Vector3.Scale(Random.insideUnitSphere, new Vector3(_scaledStartRadius, _scaledStartRadius, _scaledStartRadius));// 
					_tmpPos = _thisPos + _tmpPos;
									
					_tmpDir = Vector3.Scale(Random.insideUnitSphere, new Vector3(velocity.x, velocity.y, velocity.z)); 
					_tmpDir.y = (_tmpDir.y + (2 * (Mathf.Abs(_tmpDir.y) * upwardsBias)));
					
					if (randomRotation == true)
					{
						_randomizedRotation = Random.Range(-1f,1f);
						_tmpAngularVelocity = Random.Range(-1f,1f) * angularVelocity;
						
					}
					else
					{
						_randomizedRotation = 0f;
						_tmpAngularVelocity = angularVelocity;
					}
					
					_tmpDir = Vector3.Scale(_tmpDir, new Vector3(size, size, size));
					
					 _tmpParticleSize = size * (particleSize + (Random.value * sizeVariation));
					
					_tmpDuration = _scaledDuration + (Random.value * _scaledDurationVariation);
					_particleEmitter.Emit(_tmpPos, _tmpDir, _tmpParticleSize, _tmpDuration, color, _randomizedRotation, _tmpAngularVelocity);
				}
					
				_emitTime = Time.time;
				_delayedExplosionStarted = false;
				_explodeDelay = 0f;
			}
			else
			{
				//tell update to start reducing the start delay and call explode again when it's zero
				_delayedExplosionStarted = true;
			}
		}
    }

}
