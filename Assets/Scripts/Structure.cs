using Photon.Pun;
using UnityEngine;

public abstract class Structure : Entity {
	private Vector3 position;

	public void SetupStructure(float damageBase, float damageScaling, float magicBase, float magicScaling, float vitalityBase, float vitalityScaling, float regenerationBase, float regenerationScaling, float energyBase, float energyScaling, float enduranceBase, float enduranceScaling, float armorBase, float armorScaling, float nullificationBase, float nullificationScaling, float forceBase, float forceScaling, float pierceBase, float pierceScaling, float vampBase, float vampScaling, float fervorBase, float fervorScaling, float speedBase, float speedScaling, float tenacityBase, float tenacityScaling, float critBase, float critScaling, float efficiencyBase, float efficiencyScaling, float rangeBase, float rangeScaling, float entityHeight, Team team) {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }
		this.SetupEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, entityHeight, null, team, false);
		this.position = this.transform.position;
	}

	public new void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		base.Update();
		this.transform.position = this.position;
	}
}