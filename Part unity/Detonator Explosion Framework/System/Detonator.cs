using UnityEngine;
using System.Collections;

/*

	Detonator - A parametric explosion system for Unity
	Created by Ben Throop in August 2009 for the Unity Summer of Code
	
	Simplest use case:
	
	1) Use a prefab
	
	OR
	
	1) Attach a Detonator to a GameObject, either through code or the Unity UI
	2) Either set the Detonator's ExplodeOnStart = true or call Explode() yourself when the time is right
	3) View explosion :)
	
	Medium Complexity Use Case:
	
	1) Attach a Detonator as above 
	2) Change parameters, add your own materials, etc
	3) Explode()
	4) View Explosion
	
	Super Fun Use Case:
	
	1) Attach a Detonator as above
	2) Drag one or more DetonatorComponents to that same GameObject
	3) Tweak those parameters
	4) Explode()
	5) View Explosion
	
	Better documentation is included as a PDF with this package, or is available online. Check the Unity site for a link
	or visit my site, listed below.
	
	Ben Throop
	@ben_throop
*/

[AddComponentMenu("Detonator/Detonator")]
public class Detonator : MonoBehaviour {

	private static float _baseSize = 30f;
	private static Color _baseColor = new Color(1f, .423f, 0f, .5f);
	private static float _baseDuration = 3f;
	/*
		_baseSize reflects the size that DetonatorComponents at size 1 match. Yes, this is really big (30m)
		size below is the default Detonator size, which is more reasonable for typical useage. 
		It wasn't my intention for them to be different, and I may change that, but for now, that's how it is.
	*/
	public float size = 10f; 
	public Color color = Detonator._baseColor;
	public bool explodeOnStart = true;
	public float duration = Detonator._baseDuration;
	public float detail = 1f; 
	public float upwardsBias = 0f;
	
	public float destroyTime = 7f; //sorry this is not auto calculated... yet.
	public bool useWorldSpace = true;
	public Vector3 direction = Vector3.zero;
	
	public Material fireballAMaterial;
	public Material fireballBMaterial;
	public Material smokeAMaterial;
	public Material smokeBMaterial;
	public Material shockwaveMaterial;
	public Material sparksMaterial;
	public Material glowMaterial;
	public Material heatwaveMaterial;
		
    private Component[] components;

	private DetonatorFireball _fireball;
	private DetonatorSparks _sparks;
	private DetonatorShockwave _shockwave;
	private DetonatorSmoke _smoke;
	private DetonatorGlow _glow;
	private DetonatorLight _light;
	private DetonatorForce _force;
	private DetonatorHeatwave _heatwave;
	public bool autoCreateFireball = true;
	public bool autoCreateSparks = true;
	public bool autoCreateShockwave = true;
	public bool autoCreateSmoke = true;
	public bool autoCreateGlow = true;
	public bool autoCreateLight = true;
	public bool autoCreateForce = true;
	public bool autoCreateHeatwave = false;
	
	void Awake() //S'utilitza per inicialitzar variables o parametres abans de que s'inici el joc
	{
		FillDefaultMaterials();
		
        components = this.GetComponents(typeof(DetonatorComponent));//Comprova el component de la detonacio
		foreach (DetonatorComponent dc in components)//Aconsegueix els components de la detonacio
		{
			if (dc is DetonatorFireball)//Comprovacio dels component que pot tenir
			{
				_fireball = dc as DetonatorFireball;
			}
			if (dc is DetonatorSparks)//Comprovacio dels component que pot tenir
			{
				_sparks = dc as DetonatorSparks;
			}
			if (dc is DetonatorShockwave)//Comprovacio dels component que pot tenir
			{
				_shockwave = dc as DetonatorShockwave;
			}
			if (dc is DetonatorSmoke)//Comprovacio dels component que pot tenir
			{
				_smoke = dc as DetonatorSmoke;
			}
			if (dc is DetonatorGlow)//Comprovacio dels component que pot tenir
			{
				_glow = dc as DetonatorGlow;
			}
			if (dc is DetonatorLight)//Comprovacio dels component que pot tenir
			{
				_light = dc as DetonatorLight;
			}
			if (dc is DetonatorForce)//Comprovacio dels component que pot tenir
			{
				_force = dc as DetonatorForce;
			}
			if (dc is DetonatorHeatwave)//Comprovacio dels component que pot tenir
			{
				_heatwave = dc as DetonatorHeatwave;
			}
		}
		
		if (!_fireball && autoCreateFireball)//En cas de no tenir fireball i tenir una opcio per defecte
		{
			//Deprecated _fireball = gameObject.AddComponent("DetonatorFireball") as DetonatorFireball;
            _fireball = gameObject.AddComponent<DetonatorFireball> () as DetonatorFireball;

            _fireball.Reset();
		}
		
		if (!_smoke && autoCreateSmoke)//En cas de no tenir smoke i tenir una opcio per defecte
		{
			_smoke = gameObject.AddComponent< DetonatorSmoke>() as DetonatorSmoke;
			_smoke.Reset();
		}
		
		if (!_sparks && autoCreateSparks)//En cas de no tenir sparks i tenir una opcio per defecte activada
		{
			_sparks = gameObject.AddComponent<DetonatorSparks>() as DetonatorSparks;
			_sparks.Reset();
		}
		
		if (!_shockwave && autoCreateShockwave)//En cas de no tenir shockwave i tenir una opcio per defecte activada
		{
			_shockwave = gameObject.AddComponent<DetonatorShockwave>() as DetonatorShockwave;
			_shockwave.Reset();
		}
		
		if (!_glow && autoCreateGlow)//En cas de no tenir glow i tenir una opcio per defecte activada
		{
			_glow = gameObject.AddComponent<DetonatorGlow>() as DetonatorGlow;
			_glow.Reset();
		}
		
		if (!_light && autoCreateLight)//En cas de no tenir light i tenir un per defecte activada
		{
			_light = gameObject.AddComponent<DetonatorLight>() as DetonatorLight;
			_light.Reset();
		}
		
		if (!_force && autoCreateForce)//En cas de no tenir force i tenir una opcio per defecte activada
		{
			_force = gameObject.AddComponent<DetonatorForce>() as DetonatorForce;
			_force.Reset();
		}

		if (!_heatwave && autoCreateHeatwave && SystemInfo.supportsImageEffects)//En cas de no tenir heatwave i tenir una opcio per defecte activada
		{
			_heatwave = gameObject.AddComponent<DetonatorHeatwave>() as DetonatorHeatwave;
			_heatwave.Reset();
		}
		
		components = this.GetComponents(typeof(DetonatorComponent));
	}

	void FillDefaultMaterials()//Creacio dels materials per l'explosio, fum, raig, etc.
	{
		if (!fireballAMaterial) fireballAMaterial = DefaultFireballAMaterial();
		if (!fireballBMaterial) fireballBMaterial = DefaultFireballBMaterial();
		if (!smokeAMaterial) smokeAMaterial = DefaultSmokeAMaterial();
		if (!smokeBMaterial) smokeBMaterial = DefaultSmokeBMaterial();
		if (!shockwaveMaterial) shockwaveMaterial = DefaultShockwaveMaterial();
		if (!sparksMaterial) sparksMaterial = DefaultSparksMaterial();
		if (!glowMaterial) glowMaterial = DefaultGlowMaterial();
		if (!heatwaveMaterial) heatwaveMaterial = DefaultHeatwaveMaterial();

	}
	
	void Start()
	{
		if (explodeOnStart)//Entra si es verdader llavors s'inicia l'explosio
		{
			UpdateComponents();
			this.Explode();
		}
	}
	
	private float _lastExplosionTime = 1000f;
	void Update () 
    {
		if (destroyTime > 0f)
		{
			if (_lastExplosionTime + destroyTime <= Time.time /*&& gameObject.tag != GameObject.FindGameObjectWithTag("Player") */)//Part modificada
			{
				Destroy(gameObject);
			}
		}
	}
	
	private bool _firstComponentUpdate = true;

	void UpdateComponents()
	{
		if (_firstComponentUpdate)
		{
			foreach (DetonatorComponent component in components)//Part important on s'inicialitzen tots els efectes tant personalitzats com per defecte
			{
				component.Init();
				component.SetStartValues();
			}
			_firstComponentUpdate = false;
		}
		
		if (!_firstComponentUpdate)
		{
			float s = size / _baseSize;//tamany dividit pel tamany base
			
			Vector3 sdir = new Vector3(direction.x * s, direction.y * s, direction.z * s);//La direccio de l'explosio
			
			float d = duration / _baseDuration;//Duracio de l'explosio
			
			foreach (DetonatorComponent component in components)
			{
				if (component.detonatorControlled)
				{
					component.size = component.startSize * s;//Tamany de l'explosio
					component.timeScale = d;//Temps que tarda en arribar un tamany
					component.detail = component.startDetail * detail;//?
					component.force = new Vector3(component.startForce.x * s + sdir.x, component.startForce.y * s + sdir.y, component.startForce.z * s + sdir.z );//Potencia de l'explosio?
					component.velocity = new Vector3(component.startVelocity.x * s + sdir.x, component.startVelocity.y * s + sdir.y, component.startVelocity.z * s + sdir.z );//Velocitat de l'explosio
					
					//take the alpha of detonator color and consider it a weight - 1=use all detonator, 0=use all components
					component.color = Color.Lerp(component.startColor, color, color.a);
				}
			}
		}
	}
	
	private Component[] _subDetonators;
	
	 public void Explode() 
	{
		_lastExplosionTime = Time.time;
	
		foreach (DetonatorComponent component in components)
		{
			UpdateComponents();
			component.Explode();
		}
	}
	
	public void Reset() 
	{
		size = 10f; //this is hardcoded because _baseSize up top is not really the default as much as what we match to
		color = _baseColor;
		duration = _baseDuration;
		FillDefaultMaterials();
	}
	

	//Default Materials
	//The statics are so that even if there are multiple Detonators in the world, they
	//don't each create their own default materials. Theoretically this will reduce draw calls, but I haven't really
	//tested that.
	public static Material defaultFireballAMaterial;
	public static Material defaultFireballBMaterial;
	public static Material defaultSmokeAMaterial;
	public static Material defaultSmokeBMaterial;
	public static Material defaultShockwaveMaterial;
	public static Material defaultSparksMaterial;
	public static Material defaultGlowMaterial;
	public static Material defaultHeatwaveMaterial;
	//----------Creacio dels materials----------------------
	public static Material DefaultFireballAMaterial()
	{
		if (defaultFireballAMaterial != null) return defaultFireballAMaterial;//Si el material ja ha sigut creat
        defaultFireballAMaterial = new Material(Shader.Find("Particles/Additive"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultFireballAMaterial.name = "FireballA-Default";//Afegeix un nom al material
        Texture2D tex = Resources.Load("Detonator/Textures/Fireball") as Texture2D;//Creacio de la textura
		defaultFireballAMaterial.SetColor("_TintColor", Color.white);//Dona un color al material
		defaultFireballAMaterial.mainTexture = tex;//Assignacio de la textura al material
		defaultFireballAMaterial.mainTextureScale = new Vector2(0.5f, 1f);//Redimensio de la textura
		return defaultFireballAMaterial;//retorna el material nou
	}

	public static Material DefaultFireballBMaterial()
	{
		if (defaultFireballBMaterial != null) return defaultFireballBMaterial;//Si el material ja ha sigut creat
		defaultFireballBMaterial =  new Material(Shader.Find("Particles/Additive"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultFireballBMaterial.name = "FireballB-Default";//Afegeix un nom al material
		Texture2D tex = Resources.Load("Detonator/Textures/Fireball") as Texture2D;//Creacio de la textura
		defaultFireballBMaterial.SetColor("_TintColor", Color.white);//Dona un color al material
		defaultFireballBMaterial.mainTexture = tex;//Assignacio de la textura al material
		defaultFireballBMaterial.mainTextureScale = new Vector2(0.5f, 1f);//Redimensio de la textura
		defaultFireballBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);//La textura al desplaçar-se
		return defaultFireballBMaterial;//retorna el material nou
	}
	
	public static Material DefaultSmokeAMaterial()
	{
		if (defaultSmokeAMaterial != null) return defaultSmokeAMaterial;//Si el material ja ha sigut creat
		defaultSmokeAMaterial = new Material(Shader.Find("Particles/Alpha Blended"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultSmokeAMaterial.name = "SmokeA-Default";//Afegeix un nom al material
		Texture2D tex = Resources.Load("Detonator/Textures/Smoke") as Texture2D;//Creacio de la textura
		defaultSmokeAMaterial.SetColor("_TintColor", Color.white);//Dona un color al material
		defaultSmokeAMaterial.mainTexture = tex;//Assignacio de la textura al material
		defaultSmokeAMaterial.mainTextureScale = new Vector2(0.5f, 1f);//Redimensio de la textura
		return defaultSmokeAMaterial;//retorna el material nou
	}
		
	public static Material DefaultSmokeBMaterial()
	{
		if (defaultSmokeBMaterial != null) return defaultSmokeBMaterial;//Si el material ja ha sigut creat
		defaultSmokeBMaterial = new Material(Shader.Find("Particles/Alpha Blended"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultSmokeBMaterial.name = "SmokeB-Default";//Afegeix un nom al material
		Texture2D tex = Resources.Load("Detonator/Textures/Smoke") as Texture2D;//Creacio de la textura
		defaultSmokeBMaterial.SetColor("_TintColor", Color.white);//Dona un color al material
		defaultSmokeBMaterial.mainTexture = tex;//Assignacio de la textura al material
		defaultSmokeBMaterial.mainTextureScale = new Vector2(0.5f, 1f);//Redimensio de la textura
		defaultSmokeBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);//La textura al desplaçar-se
		return defaultSmokeBMaterial;//retorna el material nou
	}
	
	public static Material DefaultSparksMaterial()
	{
		if (defaultSparksMaterial != null) return defaultSparksMaterial;//Si el material ja ha sigut creat
		defaultSparksMaterial = new Material(Shader.Find("Particles/Additive"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultSparksMaterial.name = "Sparks-Default";//Afegeix un nom al material
		Texture2D tex = Resources.Load("Detonator/Textures/GlowDot") as Texture2D;//Creacio de la textura
		defaultSparksMaterial.SetColor("_TintColor", Color.white);//Dona un color al material
		defaultSparksMaterial.mainTexture = tex;//Assignacio de la textura al material
		return defaultSparksMaterial;//retorna el material nou
	}
	
	public static Material DefaultShockwaveMaterial()
	{	
		if (defaultShockwaveMaterial != null) return defaultShockwaveMaterial;//Si el material ja ha sigut creat
		defaultShockwaveMaterial = new Material(Shader.Find("Particles/Additive"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultShockwaveMaterial.name = "Shockwave-Default";//Afegeix un nom al material
		Texture2D tex = Resources.Load("Detonator/Textures/Shockwave") as Texture2D;//Creacio de la textura
		defaultShockwaveMaterial.SetColor("_TintColor", new Color(0.1f,0.1f,0.1f,1f));//Dona un color al material
		defaultShockwaveMaterial.mainTexture = tex;//Assignacio de la textura al material
		return defaultShockwaveMaterial;//retorna el material nou
	}
	
	public static Material DefaultGlowMaterial()
	{
		if (defaultGlowMaterial != null) return defaultGlowMaterial;//Si el material ja ha sigut creat
		defaultGlowMaterial = new Material(Shader.Find("Particles/Additive"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
		defaultGlowMaterial.name = "Glow-Default";//Afegeix un nom al material
		Texture2D tex = Resources.Load("Detonator/Textures/Glow") as Texture2D;//Creacio de la textura
		defaultGlowMaterial.SetColor("_TintColor", Color.white);//Dona un color al material
		defaultGlowMaterial.mainTexture = tex;//Assignacio de la textura al material
		return defaultGlowMaterial;//retorna el material nou
	}
	
	public static Material DefaultHeatwaveMaterial()
	{
        //Unity Pro Only
        if (SystemInfo.supportsImageEffects)//Si funcionen els efectes d'imatge
        {
			if (defaultHeatwaveMaterial != null) return defaultHeatwaveMaterial;//Si el material ja ha sigut creat
			defaultHeatwaveMaterial = new Material(Shader.Find("HeatDistort"));//Crea un nou material si no ha sigut creat anteriorment i a mes a mes li afegeix un efecte
			defaultHeatwaveMaterial.name = "Heatwave-Default";//Afegeix un nom al material
			Texture2D tex = Resources.Load("Detonator/Textures/Heatwave") as Texture2D;//Creacio de la textura
			defaultHeatwaveMaterial.SetTexture("_BumpMap", tex);//Modificacio de la textura al material
			return defaultHeatwaveMaterial;//retorna el material nou
        }
        else
        {
			return null;//Si no funciona els efectes d'imatges no fa referencia a res
        }
	}
	//----------Fi de la creacio dels materials----------------------
}
