using UnityEngine;

public class TestChamp : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(30, 5, 30, 5, 550, 15, 3, 0.5f, 300, 20, 3, 0.5f, 20, 3, 20, 3, 0, 0, 0, 0, 0, 0, 0.75f, 0, 10, 1, 0, 0, 0, 0, 0, 1, 30, 0, 5, this.GetComponent<Animator>(), "TestChamp", Resources.Load<Sprite>("Entities/Champions/TestChamp/Icon"),
			new Ability(0, this, "Test Passive", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/PassiveIcon"),
				() => {
					// Used with primary to easily test leveling up.

					if (this.currency >= this.level * Entity.ExperiencePerLevelPerLevel - this.experience) {
						this.currency -= (int) (this.level * Entity.ExperiencePerLevelPerLevel - this.experience);
						this.experience += this.level * Entity.ExperiencePerLevelPerLevel - this.experience;
					}
				}),
			new Ability(0, this, "Test Primary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/PrimaryIcon"),
				() => {
					// Can be used with passive to test leveling up, or to test items.

					this.currency += this.level * Entity.ExperiencePerLevelPerLevel / 2;
				}),
			new Ability(3, this, "Test Secondary", "Test.", 100, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/SecondaryIcon"),
				() => {
					// Example skillshot ability.

					AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/SecondaryModel", true, true, false, false, this, this.transform.position, Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point), null, 30, 30, 3);
					abilityObject.collisionAction = () => { Ability.DealDamage(false, this, abilityObject.collidedEntity, Ability.DamageType.True, this.damage.CurrentValue * 0.5f, 0); };
				}),
			new Ability(0, this, "Test Tertiary", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/TertiaryIcon"),
				() => {
					// Example shield and stat ability.

					this.StartCoroutine(Ability.Heal(this, Ability.HealthType.Shield, 3, this.damage.CurrentValue, 1 - (1 / this.magic.CurrentValue)));
					this.StartCoroutine(Ability.StatChange(this, 3, Stat.StatId.Damage, 0, 1)); // Double physical damage for three seconds.
				}),
			new Ability(0, this, "Test Ultimate", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Entities/Champions/TestChamp/UltimateIcon"),
				() => {
					// Example point-click area-of-effect ability.

					Entity target = this.player.FuzzyMouseTargetEntity(true, false, false, true, false, true);
					if (!target || Vector3.Distance(this.transform.position, target.transform.position) > 50) {
						this.resource += this.ultimateAbility.cost;
						this.ultimateAbility.CurrentCooldown = 0;
						return;
					}

					AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/TertiaryModel", false, true, false, true, this, this.transform.position, target.transform.position, target, 10, 50, 10);
					Ability aoeAbility = new Ability(0, this, this.ultimateAbility.Name, this.ultimateAbility.Description, 0, null, () => { });
					aoeAbility.Action = () => { Ability.DealDamage(false, this, aoeAbility.target, Ability.DamageType.Magical, 0, 50); }; // Deal half of the target's health in magical damage.
					abilityObject.collisionAction = () => { Ability.DoInArea(abilityObject.collidedEntity.transform.position, 10, false, aoeAbility); };
				}
			)
		);
	}
}
