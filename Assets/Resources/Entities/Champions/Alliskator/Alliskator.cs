using System;
using System.Collections;
using UnityEngine;
public class Alliskator : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(89, 3.5f, 0, 0, 1100, 105, 6.5f, 0.75f, 655, 22, 6, 0.5f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.8f, 0.04f, 30, 0, 0, 0, 0, 0, 0, 0, 10, 0, 7, this.GetComponent<Animator>(), "Alliskator", UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Alliskator/Icon"),
			new Ability(0, this, "Slash and Mash", "Periodically, Alliskator's next basic attack strikes twice and grants him a burst of movement speed.", 0, null, () => { }),
			new Ability(8, this, "Heavy Strike", "Alliskator attacks with massive force, dealing extra damage.", 45, UnityEngine.Resources.Load<UnityEngine.Sprite>(null),
				() => { this.basicAttackActions.Add((target) => { Ability.DealDamage(true, this, target, Ability.DamageType.Physical, this.damage.CurrentValue, 0); }); }
			),
			new Ability(10, this, "Smack Smack Attack", "Alliskator whales on an enemy, stunning them after the second attack", 40, null,
				() => {
					Entity firstHitEntity = null;
					this.basicAttackActions.Add((target) => { firstHitEntity = target; });
					this.bufferedBasicAttackActions.Add((target) => {
						if (target == firstHitEntity) {
							Ability.DealDamage(true, this, target, Ability.DamageType.Physical, this.damage.CurrentValue + 50, 0);
							new StatusEffect(StatusEffect.StatusEffectType.immobilize, 1, target);
						}
					});
				}
			),
			new Ability(12, this, "Eye on the Prey", "Alliskator slows an enemy, causing his next auto attack agaist that target to stun them.", 55, null,
				() => {
					Entity target = this.player.FuzzyMouseTargetEntity(true, false, false, true, false, false);
					if (!target || target.team == this.team || Vector3.Distance(target.transform.position, this.transform.position) > 20) {
						this.resource += this.tertiaryAbility.cost;
						this.tertiaryAbility.CurrentCooldown = 0;
						return;
					}

					target.speed.PercentageBonusValue -= 0.25f;
					this.StartCoroutine(this.TertiaryPartTwo(target));

					this.basicAttackActions.Add((stunTarget) => {
						if (stunTarget == target) { new StatusEffect(StatusEffect.StatusEffectType.immobilize, 1, stunTarget); }
					});
				}
			),
			new Ability(100, this, "Power Up", "Alliskator increases his movement speed and damage for a while.", 100, UnityEngine.Resources.Load<UnityEngine.Sprite>(null),
				() => {
					this.speed.BonusValue += 10;
					this.vitality.BonusValue += this.vitality.CurrentValue * 0.1f;
					Ability.Heal(this, Ability.HealthType.Health, 0, 0, 0.2f);
					this.StartCoroutine(this.UltimatePartTwo());
				}
			)
		);

		this.StartCoroutine(this.PassiveCoroutine());
	}

	private IEnumerator PassiveCoroutine() {
		Action<Entity> passiveAction = (target) => { this.basicAttackAbility.CurrentCooldown = 0.1f; };
		Action<Entity> passiveBufferedAction = (target) => {
			new StatusEffect(StatusEffect.StatusEffectType.immobilize, 1, target);
			this.speed.BonusValue += 5;
			this.StartCoroutine(this.PassivePartTwo());
		};

		while (true) {
			yield return new WaitForSeconds(10);
			if (!this.basicAttackActions.Contains(passiveAction)) { this.basicAttackActions.Add(passiveAction); } // Ensure that only one is active at a time (otherwise you'll one-shot people).
			if (!this.bufferedBasicAttackActions.Contains(passiveBufferedAction)) { this.bufferedBasicAttackActions.Add(passiveBufferedAction); } // Ensure that only one is active at a time (otherwise you'll one-shot people).
		}
	}

	private IEnumerator PassivePartTwo() {
		yield return new WaitForSeconds(3);
		this.speed.BonusValue -= 5;
	}

	private IEnumerator TertiaryPartTwo(Entity target) {
		yield return new WaitForSeconds(3);
		target.speed.PercentageBonusValue += 0.25f;
	}

	private IEnumerator UltimatePartTwo() {
		yield return new WaitForSeconds(5);
		this.speed.BonusValue -= 10;
		this.vitality.BonusValue -= this.vitality.CurrentValue * 0.1f;
	}
}
