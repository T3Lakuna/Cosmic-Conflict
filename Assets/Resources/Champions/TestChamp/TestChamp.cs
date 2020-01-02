using System;
using System.Collections;
using UnityEngine;

public class TestChamp : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(30, 5, 30, 5, 550, 15, 3, 0.5, 300, 20, 3, 0.5, 20, 3, 20, 3, 0, 0, 0, 0, 0, 0, 3, 0, 10, 1, 0, 0, 0, 0, 0, 1, 500, 0, "TestChamp", Resources.Load<Sprite>("Champions/TestChamp/Icon"),
			new Ability(0, this, null, "TestPassive", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/PassiveIcon"), () => {
				// Used with primary to easily test leveling up.

				if (this.currency >= this.level * Entity.ExperiencePerLevelPerLevel - this.experience) {
					this.currency -= (int) (this.level * Entity.ExperiencePerLevelPerLevel - this.experience);
					this.experience += this.level * Entity.ExperiencePerLevelPerLevel - this.experience;
				}
			}),
			new Ability(0, this, null, "TestPrimary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/PrimaryIcon"), () => {
				// Can be used with passive to test leveling up, or to test items.

				this.currency += 1000;
			}),
			new Ability(3, this, null, "TestSecondary", "Test.", 100, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/SecondaryIcon"), () => {
				// Example skillshot ability.

				AbilityObject abilityObject = Ability.CreateAbilityObject("Champions/TestChamp/SecondaryModel", true, this, this.transform.position, Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, this.entityRenderer.bounds.size.y), 30, 30, 3, () => { }, () => { });
				abilityObject.collisionAction = () => { Ability.DealDamage(abilityObject.collidedEntity, Ability.DamageType.True, this.damage.CurrentValue * 0.5, 0); };
				Debug.Log("Hello!");
			}),
			new Ability(0, this, null, "TestTertiary", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/TertiaryIcon"), () => {
				// Example two-part ability (adding shield, then removing it later).

				this.magicalShield += this.magic.CurrentValue * 10;
				this.physicalShield += this.damage.CurrentValue * 10;
				this.shield += this.magic.CurrentValue * 5 + this.damage.CurrentValue * 5;
				this.StartCoroutine(this.TertiaryRemoveShield());
			}),
			new Ability(0, this, null, "TestUltimate", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/UltimateIcon"), () => {
				// Example point-click ability.

				Collider targetCollider = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
				if (!targetCollider) { return; }
				Entity targetEntity = targetCollider.GetComponent<Entity>();
				if (!targetEntity) { return; }
				if (targetEntity.team == this.team) { return; }
				AbilityObject abilityObject = Ability.CreateAbilityObject("Champions/TestChamp/TertiaryModel", true, this, this.transform.position, targetEntity.transform.position, 10, 50, 10, () => { }, () => { });
				abilityObject.collisionAction = () => { Ability.DealDamage(abilityObject.collidedEntity, Ability.DamageType.Magical, 0, 10); };
				abilityObject.updateAction = () => { abilityObject.target = targetEntity.transform.position; };
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