using System;
using System.Collections;
using UnityEngine;

public class TestChamp : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(30, 5, 30, 5, 550, 15, 3, 0.5, 300, 20, 3, 0.5, 20, 3, 20, 3, 0, 0, 0, 0, 0, 0, 0.75, 0, 10, 1, 0, 0, 0, 0, 0, 1, 30, 0, "TestChamp", Resources.Load<Sprite>("Entities/Champions/TestChamp/Icon"),
			new Ability(0, this, "TestPassive", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/PassiveIcon"), () => {
				// Used with primary to easily test leveling up.

				if (this.currency >= this.level * Entity.ExperiencePerLevelPerLevel - this.experience) {
					this.currency -= (int) (this.level * Entity.ExperiencePerLevelPerLevel - this.experience);
					this.experience += this.level * Entity.ExperiencePerLevelPerLevel - this.experience;
				}
			}),
			new Ability(0, this, "TestPrimary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/PrimaryIcon"), () => {
				// Can be used with passive to test leveling up, or to test items.

				this.currency += 1000;
			}),
			new Ability(3, this, "TestSecondary", "Test.", 100, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/SecondaryIcon"), () => {
				// Example skillshot ability.

				AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/SecondaryModel", true, false, false, this, this.transform.position, Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, this.entityRenderer.bounds.size.y), null, 30, 30, 3);
				abilityObject.collisionAction = () => { Ability.DealDamage(abilityObject.collidedEntity, Ability.DamageType.True, this.damage.CurrentValue * 0.5, 0); };
			}),
			new Ability(0, this, "TestTertiary", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/TertiaryIcon"), () => {
				// Example two-part ability (adding shield, then removing it later).

				this.magicalShield += this.magic.CurrentValue * 10;
				this.physicalShield += this.damage.CurrentValue * 10;
				this.shield += this.magic.CurrentValue * 5 + this.damage.CurrentValue * 5;
				this.StartCoroutine(this.TertiaryRemoveShield());
			}),
			new Ability(0, this, "TestUltimate", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/UltimateIcon"), () => {
				// Example point-click area-of-effect ability.

				Collider targetCollider = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
				if (!targetCollider) { this.resource += 50; return; } // Return mana cost when not casting! (No target.)
				Entity targetEntity = targetCollider.GetComponent<Entity>();
				if (!targetEntity) { this.resource += 50; return; } // Return mana cost when not casting! (No target entity.)
				if (targetEntity.team == this.team) { this.resource += 50; return; } // Return mana cost when not casting! (Targeted ally.)
				if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 10) { this.resource += 50; return; } // Return mana cost when not casting! (Out of range.)
				AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/TertiaryModel", true, false, true, this, this.transform.position, targetEntity.transform.position, targetEntity, 10, 10, 10);
				Ability aoeAbility = new Ability(0, this, "TestUltimate AoE", "Test.", 0, null, () => { });
				aoeAbility.Action = () => { Ability.DealDamage(aoeAbility.target, Ability.DamageType.Magical, 0, 15); };
				abilityObject.collisionAction = () => { Ability.DoInArea(abilityObject.collidedEntity.transform.position, 10, false, aoeAbility); };
			})
		);
	}

	private IEnumerator TertiaryRemoveShield() {
		// Example two-part ability (part two).

		yield return new WaitForSeconds(3);
		this.magicalShield = Math.Max(this.magicalShield - this.magic.CurrentValue * 10, 0);
		this.physicalShield = Math.Max(this.physicalShield - this.damage.CurrentValue * 10, 0);
		this.shield = Math.Max(this.shield - (this.magic.CurrentValue * 5 + this.damage.CurrentValue * 5), 0);
	}
}