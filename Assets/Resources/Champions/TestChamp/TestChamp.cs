using UnityEngine;

namespace Resources.Champions.TestChamp {
	public class TestChamp : Champion {
		public void Start() {
			this.SetupChampion(30, 5, 30, 5, 550, 15, 3, 0.5, 300, 20, 3, 0.5, 20, 3, 20, 3, 0, 0, 0, 0, 0, 0, 3, 0, 10, 1, 0, 0, 0, 0, 0, 0, 500, 0, "TestChamp",
				new Ability(0, this, null, "TestPassive", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/PassiveAbilityIcon"), () => {
					// Debug.Log("TestChamp passive.");
				}),
				new Ability(0, this, null, "TestPrimary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/PrimaryAbilityIcon"), () => {
					Debug.Log("TestChamp primary.");
				}),
				new Ability(8, this, null, "TestSecondary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/SecondaryAbilityIcon"), () => {
					Debug.Log("TestChamp secondary.");
					AbilityObject abilityObject = Ability.CreateAbilityObject("Champions/TestChamp/SecondaryAbilityObject", true, this, this.transform.position, Tools.PositionOnMapAt(this.player.mousePosition, this.entityRenderer.bounds.size.y), 30, 30, 3, () => { }, () => { });
					abilityObject.collisionAction = () => { abilityObject.collidedEntity.Die(); };
				}),
				new Ability(0, this, null, "TestTertiary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/TertiaryAbilityIcon"), () => {
					Debug.Log("TestChamp tertiary.");
				}), new Ability(0, this, null, "TestUltimate", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/TestChamp/UltimateAbilityIcon"), () => {
					Debug.Log("TestChamp ultimate.");
				})
			);
		}
	}
}