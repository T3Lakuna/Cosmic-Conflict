using UnityEngine;

public class CannonMinion : Minion {
	public static Minion CreateCannonMinion(Team team, GameObject[] path) {
		Minion cannonMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).GetComponent<Minion>();
		cannonMinion.SetupMinion(25, 5, 600, 20, 25, team, path, "Cannon Minion");
		return cannonMinion;
	}
}
