using UnityEngine;

public class Crypto : Champion {
	// Start is called before the first frame update
	private new void Start() {
		base.Start();
		this.SetupChampion(70, 2.5, 0, 0, 850, 100, 6, .5, 455, 18, 6, .5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .7, .04, 40, 0,
		  0, 0, 0, 0, 0, 0, 10, 0, 4, this.GetComponent<Animator>(), "Crypto",
		  UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Icon"),
		  new Ability(10, this, "Vulnerability Breach", "Crypto finds the weakness in his enemies when their at their weakest allowing his next attack on the enemy to do bonus damage based missing health", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Passive"),
			() => {

			}),
		  new Ability(8, this, "Fire Wall Breaker", "Crypto loads his blade with a virse that will slow the enemy hit ", 45, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Primary"), () => {
			  Entity targetEntity = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider.GetComponent<Entity>();
			  AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/TertiaryModel",
				false, true, false, true, this, this.transform.position, targetEntity.transform.position, targetEntity, 10000, 10, 100);
			  abilityObject.collisionAction = () => {
				  Ability.DealDamage(false, this, targetEntity, Ability.DamageType.Magical, 200, 0);
				  Ability.ApplyStatusEffect(targetEntity,Ability.StatusEffectType.Slow,1,10);
			  };


		  }),
		  new Ability(20, this, "EMP", "Crypto lobes an EMP exploding on contact dealing damage and slowing and silenceing everyone in the area", 90, UnityEngine.Resources.Load<UnityEngine.Sprite>("ok look im tired and i messed up my leg so ill do the art later"),
			() =>
			{
				Ability aoeAbility = new Ability(0,this,"EMP","Blam", 0,null, () => { });
				aoeAbility.Action = () =>
				{
					Ability.DealDamage(false, this, aoeAbility.target, Ability.DamageType.Magical, 200, 0);
					Ability.ApplyStatusEffect(aoeAbility.target,Ability.StatusEffectType.Silence,1,100);
				};
				AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/SecondaryModel", true, false, false, false, this, this.transform.position, Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point), null, 30, 30, 3);
				abilityObject.collisionAction = () =>
				{
					Ability.DoInArea(abilityObject.collidedEntity.transform.position,10,false, aoeAbility);
				};

			}),
		  new Ability(18, this, "Holo-Trans-Locator", "Crypto creates a hologram while going invisable Crypto can recast the ability to swap places with the clone", 90, UnityEngine.Resources.Load<UnityEngine.Sprite>("Same thing i said in his w"),
			() =>
			{
				AbilityObject abilityObect = Ability.CreateAbilityObject("Entities/Champion/Crypto/Model",false,true,false,false,this, this.transform.position,Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point),null,40,80,5);
			}),
		  new Ability(120, this, "Black Out",
			"Crypto unleashes a burst of energy all around the area dealing damaging and silenceing everyone inside the area", 100, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/dang"),
			() => {
				Ability aoeAbiility = new Ability(0,this,"Black Out","Lights Out ;)",0,null, () => {});
				aoeAbiility.Action = () =>
				{
					Ability.DealDamage(false,this, aoeAbiility.target,Ability.DamageType.Magical,250,0);
					Ability.ApplyStatusEffect(aoeAbiility.target,Ability.StatusEffectType.Silence,1.5,100);

				};
				Ability.DoInArea(this.transform.position,100,false,aoeAbiility);

			})
		);
	}
	private new void Update(){
		base.Update();
	}
}
