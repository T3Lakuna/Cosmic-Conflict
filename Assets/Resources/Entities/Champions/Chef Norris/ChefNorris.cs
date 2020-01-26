using UnityEngine;

public class ChefNorris : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(60, 3, 0, 0, 750, 60, 3, 0.05f, 400, 20, 2, 0.1f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.7f, 0.05f, 35, 0, 0, 0, 0, 0, 0, 0, 25, 0, 5, this.GetComponent<Animator>(), "Chef Norris", UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/Icon"),
			new Ability(0, this, "Good Eats", "Chef Norris gain speed for 4 seconds when he casts an ability.", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/PassiveIcon"), () => { }),
			new Ability(10, this, "Spicy Chicken Teriyaki", "Chef Norris feeds spicy chicken teriyaki to an ally, granting them fervor and movement speed.", 70, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/PrimaryIcon"),
				() => {
					Entity target = this.player.FuzzyMouseTargetEntity(false, true, true, true, false, false);
					if (!target || Vector3.Distance(target.transform.position, this.transform.position) > 50) {
						this.resource += this.primaryAbility.cost;
						this.primaryAbility.CurrentCooldown = 0;
						return;
					}

					this.StartCoroutine(Ability.StatChange(target, 6, Stat.StatId.Fervor, 0, 0.2f));
					this.StartCoroutine(Ability.StatChange(this, 4, Stat.StatId.Speed, 0, 0.2f)); // Passive
				}),
			new Ability(8, this, "Steak And Eggs", "Chef Norris feeds steak and eggs to an ally, granting them a shield at the cost of movement speed.", 60, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/SecondaryIcon"),
				() => {
					Entity target = this.player.FuzzyMouseTargetEntity(false, true, true, true, false, false);
					if (!target || Vector3.Distance(target.transform.position, this.transform.position) > 50) {
						this.resource += this.secondaryAbility.cost;
						this.secondaryAbility.CurrentCooldown = 0;
						return;
					}

					Ability.Heal(target, Ability.HealthType.Shield, 0, 0, 0.1f);
					this.StartCoroutine(Ability.StatChange(target, 3, Stat.StatId.Speed, 0, -0.1f));
					this.StartCoroutine(Ability.StatChange(this, 4, Stat.StatId.Speed, 0, 0.2f)); // Passive
				}),
			new Ability(12, this, "Double Decker Cheeseburger", "Chef Norris feeds a double decker cheeseburger to an ally, granting them damage or magic (based on their primary damage type) and fervor.", 100, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/TertiaryIcon"),
				() => {
					Entity target = this.player.FuzzyMouseTargetEntity(false, true, true, true, false, false);
					if (!target || Vector3.Distance(target.transform.position, this.transform.position) > 50) {
						this.resource += this.tertiaryAbility.cost;
						this.tertiaryAbility.CurrentCooldown = 0;
						return;
					}

					if (target.damage.CurrentValue > target.magic.CurrentValue) {
						this.StartCoroutine(Ability.StatChange(target, 5, Stat.StatId.Damage, 0, 0.2f));
					} else {
						this.StartCoroutine(Ability.StatChange(target, 5, Stat.StatId.Magic, 0, 0.2f));
					}
					this.StartCoroutine(Ability.StatChange(this, 4, Stat.StatId.Speed, 0, 0.2f)); // Passive
				}),
			new Ability(100, this, "All You Can Eat Buffet", "Chef Norris serves a full course meal to an ally, empowering them with damage or magic (based on their primary damage type), fervor, speed, and regeneration.", 120, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/UltimateIcon"),
				() => {
					Entity target = this.player.FuzzyMouseTargetEntity(false, true, true, true, false, false);
					if (!target || Vector3.Distance(target.transform.position, this.transform.position) > 50) {
						this.resource += this.ultimateAbility.cost;
						this.ultimateAbility.CurrentCooldown = 0;
						return;
					}

					if (target.damage.CurrentValue > target.magic.CurrentValue) {
						this.StartCoroutine(Ability.StatChange(target, 5, Stat.StatId.Damage, 0, 0.2f));
					} else {
						this.StartCoroutine(Ability.StatChange(target, 5, Stat.StatId.Magic, 0, 0.2f));
					}
					this.StartCoroutine(Ability.StatChange(target, 10, Stat.StatId.Fervor, 0, 0.2f));
					this.StartCoroutine(Ability.StatChange(target, 10, Stat.StatId.Speed, 0, 0.2f));
					this.StartCoroutine(Ability.StatChange(target, 10, Stat.StatId.Regeneration, 0, 0.2f));
					this.StartCoroutine(Ability.StatChange(this, 4, Stat.StatId.Speed, 0, 0.2f)); // Passive
				}
			)
		);
	}
}