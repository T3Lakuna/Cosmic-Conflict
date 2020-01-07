public class Armstrong : Champion {
	
	private System.Collections.Generic.Dictionary<Champion, int> _marks;
	private void Start() {
		this._marks = new System.Collections.Generic.Dictionary<Champion, int>();
		foreach (Champion champion in MatchManager.Instance.champions) {
			// Add
		}

		this.SetupChampion(80, 4, 0, 0, 800, 100, 4, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.9, 0.05, 35, 0, 0, 0, 25, 0, 0, 0, 30, 0, "Captain Armstrong", UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/Icon"),
						   new Ability(0, this, "Focus Fire", "Armstrong applies marks to targets that he basic attacks. When an enemy has 3 marks, they take additional damage from Captain Armstrong's attacks.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/PassiveIcon"), () => {
																																																																													  
																																																																												  }), 
						   new Ability(20,this,"Rocket Fire","Captain Armstrong loads his plasma rifle with rockets ammo causing his auto attacks to do explosions damaging everyone around",0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/PrimaryIcon"), () => {
								   
							   } ),
						   new Ability(10,this,"Grenade Toss","Captain Armstrong throws a grenade at targeted location that will explode after a few seconds or when Armstrong detonates it",0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champion/Armstrong/SecondaryIcon"),
							   () =>
							   {
								   AbilityObject abilityObject = Ability.CreateAbilityObject(
									   //"Entities/Champions/TestChamp/SecondaryModel", false, false, false, this,
								//	   Tools.PositionOnMapAt(
									//	   this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, this.entityRenderer.bounds.size.y, null, 50, 45, 3);
							   }),
						   new Ability(0,this,"Military Training","Captain Armstrong's training always him to take better aim granting him bonus critical damage and bonus critical chance",0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champion/Armstrong/TertiaryIcon"),
							   () =>
							   {
								   
							   }),
						   new Ability(120,this,"Tactical Vision","captain Armstrong activate his visor granting him bonus attack range and fervor",0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champion/Armstrong/UltimateIcon"),
							   () =>
							   {
								   
							   })
						  );
	}
	
}