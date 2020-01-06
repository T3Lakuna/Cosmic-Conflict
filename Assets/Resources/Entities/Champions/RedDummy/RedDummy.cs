public class RedDummy : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(0, 0, 0, 0, 9999, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "RedDummy", MatchManager.Instance.missingSprite, new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, "Dummy", "Dummy", 0, null, () => { }));
		this.team = Team.Red;
	}
}
