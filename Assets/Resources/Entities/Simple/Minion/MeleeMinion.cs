using UnityEngine;

public class MeleeMinion : Minion {
	public static Minion CreateMeleeMinion(Team team, GameObject[] path) {
		Minion meleeMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).GetComponent<Minion>();
		meleeMinion.SetupMinion(15, 1, 400, 10, 5, team, path, "Melee Minion");
		return meleeMinion;
	}
}
