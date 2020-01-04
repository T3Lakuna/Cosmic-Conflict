using UnityEngine;

public abstract class Structure : Entity {
	private Vector3 position;

	public void SetupStructure(double damageBase, double damageScaling, double magicBase, double magicScaling, double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, Team team) {
		this.SetupEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, team);
		this.position = this.transform.position;
	}

	public void Update() {
		base.Update();
		this.transform.position = this.position;
	}
}