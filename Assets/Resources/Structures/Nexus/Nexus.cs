public class Nexus : Structure {
	public Team inspectorTeam;
	private void Start() { this.SetupStructure(0, 0, 0, 0, 1000, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, this.inspectorTeam); }

	private void Update() {
		base.Update();
		// TODO: Make enemy team win if this is dead.
	}
}
