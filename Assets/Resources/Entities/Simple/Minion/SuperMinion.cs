using UnityEngine;

public class SuperMinion : Minion {
	public static Minion CreateSuperMinion(Team team, GameObject[] path) {
		Minion superMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).GetComponent<Minion>();
		superMinion.SetupMinion(100, 10, 1000, 20, 5, team, path, "Super Minion");
		return superMinion;
	}
}
