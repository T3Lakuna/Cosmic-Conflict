using System;
using UnityEngine;

public class Entity : MonoBehaviour {
	[HideInInspector] public int hitpoints; // Current health
	[HideInInspector] public int mana; // Current mana
	
	[HideInInspector] public int currentHealth; // Current maximum health
	[HideInInspector] public int currentArmor; // Current physical resistance
	[HideInInspector] public int currentNullification; // Current magic resistance
	[HideInInspector] public int currentForce; // Current physical penetration
	[HideInInspector] public int currentPierce; // Current magic penetration
	[HideInInspector] public int currentVamp; // Current life steal
	[HideInInspector] public int currentFervor; // Current attack speed
	[HideInInspector] public int currentSpeed; // Current movement speed
	
	[HideInInspector] public int healthBase; // Base health
	[HideInInspector] public double healthScaling; // Health per level
	[HideInInspector] public int healthBonus; // Bonus health (flat) from effects
	[HideInInspector] public double healthPercentageBonus; // Bonus health (percentage) from effects
	
	[HideInInspector] public int armorBase; // Base physical resistance
	[HideInInspector] public double armorScaling; // Physical resistance per level
	[HideInInspector] public int armorBonus; // Physical resistance (flat) from effects
	[HideInInspector] public double armorPercentageBonus; // Physical resistance (percentage) from effects
	
	[HideInInspector] public int nullificationBase; // Base magic resistance
	[HideInInspector] public double nullificationScaling; // Magic resistance per level
	[HideInInspector] public int nullificationBonus; // Magic resistance (flat) from effects
	[HideInInspector] public double nullificationPercentageBonus; // Magic resistance (percentage) from effects

	[HideInInspector] public int forceBase; // Base physical penetration
	[HideInInspector] public double forceScaling; // Physical penetration per level
	[HideInInspector] public int forceBonus; // Physical penetration (flat) from effects
	[HideInInspector] public double forcePercentageBonus; // Physical penetration (percentage) from effects

	[HideInInspector] public int pierceBase; // Base magic penetration
	[HideInInspector] public double pierceScaling; // Magic penetration per level
	[HideInInspector] public int pierceBonus; // Magic penetration (flat) from effects
	[HideInInspector] public double piercePercentageBonus; // Magic penetration (percentage) from effects
	
	[HideInInspector] public int vampBase; // Base life steal
	[HideInInspector] public double vampScaling; // Life steal per level
	[HideInInspector] public int vampBonus; // Life steal (flat) from effects
	[HideInInspector] public double vampPercentageBonus; // Life steal (percentage) from effects
	
	[HideInInspector] public int fervorBase; // Base attack speed
	[HideInInspector] public double fervorScaling; // Attack speed per level
	[HideInInspector] public int fervorBonus; // Attack speed (flat) from effects
	[HideInInspector] public double fervorPercentageBonus; // Attack speed (percentage) from effects
	
	[HideInInspector] public int speedBase; // Base movement speed
	[HideInInspector] public double speedScaling; // Movement speed per level
	[HideInInspector] public int speedBonus; // Movement speed (flat) from effects
	[HideInInspector] public double speedPercentageBonus; // Movement speed (percentage) from effects

	public enum DamageType {
		Magical, Physical
	}

	public void TakeDamage(DamageType type, int flatDamage, double percentageDamage) {
		double effectiveHealth = this.hitpoints;
		switch (type) {
			case DamageType.Magical:
				effectiveHealth = this.hitpoints + this.hitpoints * (this.currentNullification / 100.0);
				break;
			case DamageType.Physical:
				effectiveHealth = this.hitpoints + this.hitpoints * (this.currentArmor / 100.0);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		double damageTaken = effectiveHealth * percentageDamage;
		damageTaken += flatDamage;
		int effectiveDamageTaken = 0; // TODO
		this.hitpoints -= effectiveDamageTaken;
	}
}