using static Entity;

public class Attack {
	Entity source;
	Entity target;
	DamageType damageType;
	double actualAmount;
	double effectiveAmount;
	double amountBlocked;

	public Attack(Entity source, Entity target, DamageType damageType, double actualAmount, double effectiveAmount) {
		this.source = source;
		this.target = target;
		this.damageType = damageType;
		this.actualAmount = actualAmount;
		this.effectiveAmount = effectiveAmount;
		this.amountBlocked = this.actualAmount - this.effectiveAmount;
	}
}
