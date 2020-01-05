using static MatchManager;

public class Inhibitor : Structure {
	public Team inspectorTeam;

	private void Start() { this.SetupStructure(0, 0, 0, 0, 1000, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, this.inspectorTeam); }
}
