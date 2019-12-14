using UnityEngine;

public class TestChamp : Champion {
	public void Start() {
		this.player = MatchManager.Instance.localPlayer;
		this.SetupChampion(30, 5, 30, 5, 550, 15, 3, 0.5, 300, 20, 3, 0.5, 20, 3, 20, 3, 0, 0, 0, 0, 0, 0, 3, 0, 10, 1, 0, 0, 0, 0, 0, 0, 500, 0, this.player.team, "TestChamp", this.player.uniqueUserId, new Ability(0, this, null, "TestPassive", "Test.", 0, Resources.Load<Sprite>("Champions/TestChamp/PassiveAbilityIcon"), () => { Debug.Log("TestChamp passive."); }), new Ability(0, this, null, "TestPrimary", "Test.", 0, Resources.Load<Sprite>("Champions/TestChamp/PrimaryAbilityIcon"), () => { Debug.Log("TestChamp primary."); }), new Ability(0, this, null, "TestSecondary", "Test.", 0, Resources.Load<Sprite>("Champions/TestChamp/SecondaryAbilityIcon"), () => { Debug.Log("TestChamp secondary."); }), new Ability(0, this, null, "TestTertiary", "Test.", 0, Resources.Load<Sprite>("Champions/TestChamp/TertiaryAbilityIcon"), () => { Debug.Log("TestChamp tertiary."); }), new Ability(0, this, null, "TestUltimate", "Test.", 0, Resources.Load<Sprite>("Champions/TestChamp/UltimateAbilityIcon"), () => { Debug.Log("TestChamp ultimate."); }));
	}
}
