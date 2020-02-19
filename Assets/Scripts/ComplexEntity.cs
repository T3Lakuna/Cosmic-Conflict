using Photon.Pun;
using UnityEngine;

public abstract class ComplexEntity : Entity {
	[HideInInspector] public Ability passiveAbility;
	[HideInInspector] public Ability primaryAbility;
	[HideInInspector] public Ability secondaryAbility;
	[HideInInspector] public Ability tertiaryAbility;
	[HideInInspector] public Ability ultimateAbility;

	public void SetupComplexEntity(float damageBase, float damageScaling, float magicBase, float magicScaling, float vitalityBase, float vitalityScaling, float regenerationBase, float regenerationScaling, float energyBase, float energyScaling, float enduranceBase, float enduranceScaling, float armorBase, float armorScaling, float nullificationBase, float nullificationScaling, float forceBase, float forceScaling, float pierceBase, float pierceScaling, float vampBase, float vampScaling, float fervorBase, float fervorScaling, float speedBase, float speedScaling, float tenacityBase, float tenacityScaling, float critBase, float critScaling, float efficiencyBase, float efficiencyScaling, float rangeBase, float rangeScaling, float entityHeight, Animator entityAnimator, Team team, bool respawnable, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }
		this.SetupEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, entityHeight, entityAnimator, team, respawnable);
		this.passiveAbility = passiveAbility;
		this.primaryAbility = primaryAbility;
		this.secondaryAbility = secondaryAbility;
		this.tertiaryAbility = tertiaryAbility;
		this.ultimateAbility = ultimateAbility;
	}

	public new void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		base.Update();
		this.UpdateCooldowns();
	}

	private void UpdateCooldowns() {
		this.passiveAbility.UpdateCooldown();
		this.primaryAbility.UpdateCooldown();
		this.secondaryAbility.UpdateCooldown();
		this.tertiaryAbility.UpdateCooldown();
		this.ultimateAbility.UpdateCooldown();
	}
}