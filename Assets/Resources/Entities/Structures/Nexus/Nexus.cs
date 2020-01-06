public class Nexus : Structure {
	public Team inspectorTeam;
	public Tower topProtector;
	public Tower bottomProtector;

	private void Start() { this.SetupStructure(0, 0, 0, 0, 1000, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, this.inspectorTeam); }

	private new void Update() {
		base.Update();
		if (this.topProtector.isActiveAndEnabled || this.bottomProtector.isActiveAndEnabled) {
			this.health = this.vitality.CurrentValue;
			this.gameObject.SetActive(true);
		}
		// TODO: Make enemy team win if this is dead.
	}
}
