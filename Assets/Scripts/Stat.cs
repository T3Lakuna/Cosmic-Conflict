public class Stat {
	public enum StatId {
		Vitality, // Maximum health
		Regeneration, // Health regeneration
		Energy, // Maximum resource
		Endurance, // Resource regeneration
		Armor, // Physical damage reduction
		Nullification, // Magical damage reduction
		Force, // Physical resistance reduction
		Pierce, // Magical resistance reduction
		Vamp, // Life steal
		Fervor, // Attack speed
		Speed, // Movement speed
		Tenacity, // Disable resistance
		Crit, // Critical strike chance
		Efficiency, // Cooldown reduction
		Range // Attack range
	}

	public double currentValue;
	private double baseValue;
	private double scalingValue;
	public double bonusValue;
	public double percentageBonusValue;
	public StatId id;

	public Stat(double baseValue, double scalingValue, StatId id) {
		this.baseValue = baseValue;
		this.scalingValue = scalingValue;
		this.id = id;
		this.bonusValue = 0;
		this.percentageBonusValue = 0;
		this.currentValue = baseValue;
	}

	public void Update() {
		this.currentValue = this.baseValue + this.bonusValue + (this.baseValue + this.bonusValue) * this.percentageBonusValue;
	}

	public void LevelUp() {
		this.baseValue += this.scalingValue;
	}
}
