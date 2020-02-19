using System;
using System.Collections;
using UnityEngine;

public class Armstrong : Champion {
	private System.Collections.Generic.Dictionary<Entity, int> marks;
	private AbilityObject secondaryObject;
	private Ability secondaryPartOne;
	private Ability secondaryPartTwo;

	private new void Start() {
		base.Start();
		this.marks = new System.Collections.Generic.Dictionary<Entity, int>();
		foreach (Champion champion in MatchManager.Instance.champions) {
			if (champion.team != this.team) { this.marks.Add(champion, 0); }
		}
		Action<Entity> passiveAction = (target) => {
			if (this.marks.ContainsKey(target)) {
				this.marks[target]++;
				this.StartCoroutine(this.PassivePartTwo(target));
				if (this.marks[target] >= 3) { Ability.DealDamage(false, this, target, Ability.DamageType.Physical, 0, 0.1f); }
			}
		};
		Action explodeAction = () => {
			Ability aoeAbility = new Ability(0, this, this.secondaryPartOne.Name, this.secondaryPartOne.Description, 0, null, () => { });
			aoeAbility.Action = () => { Ability.DealDamage(false, this, aoeAbility.target, Ability.DamageType.Physical, this.damage.CurrentValue, 0); };
			Ability.DoInArea(this.secondaryObject.transform.position, 20, false, aoeAbility);
		};
		this.secondaryPartOne = new Ability(10, this, "Grenade Toss", "Captain Armstrong throws a grenade at a target location that will explode after a few seconds or when Armstrong detonates it.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/SecondaryIcon"),
			() => {
				this.secondaryObject = Ability.CreateAbilityObject("Entities/Champions/Armstrong/SecondaryModel", false, false, false, false, this, this.transform.position, this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, null, 20, 30, 5);
				this.secondaryObject.timeoutAction = explodeAction;
				this.secondaryAbility = this.secondaryPartTwo;
			}
		);
		this.secondaryPartTwo = new Ability(0, this, this.secondaryPartOne.Name, this.secondaryPartOne.Description, 0, this.secondaryPartOne.Icon, () => {
			if (this.secondaryObject) {
				explodeAction.Invoke();
				Tools.Destroy(this.secondaryObject.gameObject);
			}
			this.secondaryAbility = this.secondaryPartOne;
		});

		this.SetupChampion(78, 4, 0, 0, 800, 75, 4, 0.5f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.9f, 0.05f, 35, 0, 0, 0, 0.25f, 0, 0, 0, 30, 0, 5, this.GetComponent<Animator>(), "Captain Armstrong", UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/Icon"),
			new Ability(0, this, "Focus Fire", "Armstrong applies marks to targets that he basic attacks. When an enemy has 3 marks, they take additional damage from Captain Armstrong's attacks.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/PassiveIcon"),
				() => {
					if (!this.basicAttackActions.Contains(passiveAction)) { this.basicAttackActions.Add(passiveAction); } // Ensure that the passive action is always on basic attacks, but only one copy of it (otherwise you'd one-shot people).
				}
			),
			new Ability(20, this, "Rocket Fire", "Captain Armstrong loads his plasma rifle with rocket ammo, causing his basic attacks to deal area-of-effect damage.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/PrimaryIcon"),
				() => { this.StartCoroutine(this.PrimaryCoroutine()); }
			),
			this.secondaryPartOne,
			new Ability(0, this, "Military Grade Ammo", "Armstrong uses his best ammo for battle, passively granting him critical strike chance.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/TertiaryIcon"), () => { }),
			new Ability(30, this, "Sharpshooting", "Armstrong gains attack speed and range for a while.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Armstrong/UltimateIcon"),
				() => {
					this.StartCoroutine(Ability.StatChange(this, 10, Stat.StatId.Range, 10, 0));
					this.StartCoroutine(Ability.StatChange(this, 10, Stat.StatId.Fervor, 0, 0.5f));
				}
			)
		);
	}

	private IEnumerator PassivePartTwo(Entity target) {
		yield return new WaitForSeconds(5);
		this.marks[target]--;
	}

	private IEnumerator PrimaryCoroutine() {
		Ability aoeAbility = new Ability(0, this, this.primaryAbility.Name, this.primaryAbility.Description, 0, null, () => { });
		aoeAbility.Action = () => { Ability.DealDamage(false, this, aoeAbility.target, Ability.DamageType.Physical, this.damage.CurrentValue * 0.5f, 0); };
		Action<Entity> primaryAction = (target) => { Ability.DoInArea(target.transform.position, 5, false, aoeAbility); };
		DateTime castTime = DateTime.Now;

		while (TimeSpan.FromSeconds(6).TotalSeconds > (DateTime.Now - castTime).TotalSeconds) {
			yield return null; // Do every frame while active.
			if (!this.basicAttackActions.Contains(primaryAction)) { this.basicAttackActions.Add(primaryAction); }
		}
		this.basicAttackActions.Remove(primaryAction); // Remove effect when ability ends.
	}
}