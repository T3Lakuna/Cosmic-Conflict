using System.Diagnostics;

public class BlueDummy : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(0, 0, 0, 0, 9999, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "BlueDummy", MatchManager.Instance.missingSprite, new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }));
		this.team = Team.Blue;
	}
}
