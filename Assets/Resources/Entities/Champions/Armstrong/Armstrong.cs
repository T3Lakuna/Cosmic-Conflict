﻿using UnityEngine;

public class Armstrong : Champion {

	private System.Collections.Generic.Dictionary<Champion, int> _marks;
	private new void Start() {
		this._marks = new System.Collections.Generic.Dictionary<Champion, int>();
		foreach (Champion champion in MatchManager.Instance.champions) {
			// TODO
		}

		this.SetupChampion(78, 4, 0, 0, 800, 75, 4, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.9, 0.05, 35, 0, 0, 0, 25, 0, 0, 0, 30, 0, 5, this.GetComponent<Animator>(), "Captain Armstrong", UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/Icon"),
						   new Ability(0, this, "Focus Fire", "Armstrong applies marks to targets that he basic attacks. When an enemy has 3 marks, they take additional damage from Captain Armstrong's attacks.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/PassiveIcon"), () => {

						   }),
						   new Ability(20, this, "Rocket Fire", "Captain Armstrong loads his plasma rifle with rocket ammo, causing his basic attacks to deal area-of-effect damage.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/PrimaryIcon"), () => {

						   }),
						   new Ability(10, this, "Grenade Toss", "Captain Armstrong throws a grenade at targeted location that will explode after a few seconds or when Armstrong detonates it.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/SecondaryIcon"),
							   () => {
								   AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/SecondaryModel", false, false, false, false, this, this.transform.position, this.player.RaycastOnLayer(MatchManager.Instance.allGameLayerMasks).point, null, 30, 30, 10);
							   }),
						   new Ability(0, this, "Military Training", "Captain Armstrong's training taught him to take better aim, granting him bonus critical damage and bonus critical strike chance.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/TertiaryIcon"),
							   () => {

							   }),
						   new Ability(120, this, "Tactical Vision", "Captain Armstrong activates his visor, granting him bonus range and fervor.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/UltimateIcon"),
							   () => {

							   })
						  );
	}
}