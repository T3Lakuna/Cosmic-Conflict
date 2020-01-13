using UnityEngine;

public class MeleeMinion : Minion {
	public static MeleeMinion CreateMeleeMinion(Team team, GameObject[] path) {
		MeleeMinion meleeMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).AddComponent<MeleeMinion>();
		meleeMinion.SetupMinion(15, 1, 900, 15, 5, team, path, "Melee Minion");
		return meleeMinion;
	}
}
