﻿using System.Diagnostics;

public class BlueDummy : Champion {
	private new void Start() {
		base.Start();

		this.SetupChampion(0, 0, 0, 0, 100, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "BlueDummy", new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }), new Ability(0, this, null, "Dummy", "Dummy", 0, null, () => { }));
		this.team = Team.Blue;
	}
}