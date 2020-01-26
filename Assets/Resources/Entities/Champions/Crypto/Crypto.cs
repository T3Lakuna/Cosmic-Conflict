using System;
using System.Collections;
using UnityEngine;

public class Crypto : Champion {
	private AbilityObject tertiaryClone;
	private Ability tertiaryPartOne;
	private Ability tertiaryPartTwo;

	private new void Start() {
		base.Start();

		this.tertiaryPartOne = new Ability(18, this, "Holo-Translocator", "Crypto creates a hologram of its exact likeness. Crypto can then re-cast the ability to swap places with the clone.", 90, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/TertiaryIcon"),
			() => {
				this.tertiaryClone = Ability.CreateAbilityObject("Entities/Champions/Crypto/Model", false, false, false, false, this, this.transform.position, this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, null, this.speed.CurrentValue, 50, 10);
				Destroy(this.tertiaryClone.GetComponent<Entity>());
				this.tertiaryAbility = this.tertiaryPartTwo;
			}
		);
		this.tertiaryPartTwo = new Ability(0, this, this.tertiaryPartOne.Name, this.tertiaryPartOne.Description, 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/TertiaryIcon"),
			() => {
				if (this.tertiaryClone) {
					UnityEngine.Vector3 temp = this.transform.position;
					this.transform.position = this.tertiaryClone.transform.position;
					this.movementTarget = this.transform.position;
					this.tertiaryClone.transform.position = temp;
					this.tertiaryClone.target = this.tertiaryClone.transform.position;
				}
				this.tertiaryAbility = this.tertiaryPartOne;
			}
		);

		this.SetupChampion(40, 2, 50, 5, 850, 100, 3, 0.5f, 450, 18, 2, 0.5f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.7f, 0, 35, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, this.GetComponent<UnityEngine.Animator>(), "Crypto", UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Icon"),
			new Ability(10, this, "Vulnerability Breach", "Crypto periodically finds the weaknesses in its enemies, allowing its next attack on an enemy to deal bonus damage based on its missing health.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/PassiveIcon"), () => { }),
			new Ability(8, this, "Firewall Breaker", "Crypto loads its blade with a virus that will slow the next enemy hit.", 45, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/PrimaryIcon"),
				() => {
					this.basicAttackActions.Add((target) => {
						this.StartCoroutine(Ability.StatChange(target, 3, Stat.StatId.Speed, 0, -0.5f));
					});
				}
			),
			new Ability(20, this, "EMP", "Crypto releases an EMP which explodes on contact, dealing damage and silencing enemies caught in the blast.", 90, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/SecondaryIcon"),
				() => {
					AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/Crypto/SecondaryModel", true, true, false, false, this, this.transform.position, this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, null, 30, 30, 10);
					Ability aoeAbility = new Ability(0, this, this.secondaryAbility.Name, this.secondaryAbility.Description, 0, null, () => { });
					aoeAbility.Action = () => {
						Ability.DealDamage(false, this, aoeAbility.target, Ability.DamageType.Magical, this.magic.CurrentValue, 0);
						new StatusEffect(StatusEffect.StatusEffectType.silence, 1, aoeAbility.target);
					};
					abilityObject.collisionAction = () => { Ability.DoInArea(abilityObject.collidedEntity.transform.position, 20, false, aoeAbility); };
				}
			),
			this.tertiaryPartOne,
			new Ability(120, this, "Blackout", "Crypto releases a burst of energy, damaging and silencing enemies around it.", 100, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/UltimateIcon"),
				() => {
					Ability aoeAbility = new Ability(0, this, this.ultimateAbility.Name, this.ultimateAbility.Description, 0, null, () => { });
					aoeAbility.Action = () => {
						Ability.DealDamage(false, this, aoeAbility.target, Ability.DamageType.Magical, this.magic.CurrentValue * 2, 0);
						new StatusEffect(StatusEffect.StatusEffectType.silence, 2, aoeAbility.target);
					};
					Ability.DoInArea(this.transform.position, 30, false, aoeAbility);
				}
			)
		);

		this.StartCoroutine(this.PassiveCoroutine());
	}

	private IEnumerator PassiveCoroutine() {
		Action<Entity> passiveAction = (target) => {
			Ability.DealDamage(false, this, target, Ability.DamageType.Magical, 0, 0.25f - (target.health / target.vitality.CurrentValue)); // Maximum damage is 25% of target's maximum health.
		};

		while (true) {
			yield return new WaitForSeconds(15);
			if (!this.basicAttackActions.Contains(passiveAction)) { this.basicAttackActions.Add(passiveAction); }
		}
	}
}
