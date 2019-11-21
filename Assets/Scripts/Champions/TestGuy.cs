using UnityEngine;

namespace Champions {
	public class TestGuy : Champion {
		private void Start() {
			this.SetupComplexEntity(500, 50, 5, 0.5,
				250, 25, 2.5, 0.5, 50, 10,
				50, 10, 0, 0, 0, 0,
				0, 0, 1, 0.1, 300, 0, 0,
				0, 0, 0, 0, 0, 500, 0,
				new Team(),
				new Ability("Passive", "Passive description", () => { Debug.Log("TestGuy passive"); }),
				new Ability("Primary", "Primary description", () => { Debug.Log("TestGuy primary"); }),
				new Ability("Secondary", "Secondary description", () => { Debug.Log("TestGuy secondary"); }),
				new Ability("Tertiary", "Tertiary description", () => { Debug.Log("TestGuy tertiary"); }),
				new Ability("Ultimate", "Ultimate description", () => { Debug.Log("TestGuy ultimate"); }));
		}
	}
}