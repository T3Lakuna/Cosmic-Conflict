public class RedDummy : Champion {
	private void Start() {
		this.SetupChampion(0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "RedDummy", new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }));
		this.team = Team.Red;
	}
}
