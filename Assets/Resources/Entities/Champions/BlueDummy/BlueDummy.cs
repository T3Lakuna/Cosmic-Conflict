using UnityEngine;

public class BlueDummy : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(0, 0, 0, 0, 10000, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, this.GetComponent<Animator>(), "BlueDummy", MatchManager.Instance.missingSprite, new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }));
		this.team = Team.Blue;
	}
}
