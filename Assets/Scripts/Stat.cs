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

	public double CurrentValue;
	private double _baseValue;
	private readonly double _scalingValue;
	public readonly double BonusValue;
	public readonly double PercentageBonusValue;
	public StatId Id;

	public Stat(double baseValue, double scalingValue, StatId id) {
		this._baseValue = baseValue;
		this._scalingValue = scalingValue;
		this.Id = id;
		this.BonusValue = 0;
		this.PercentageBonusValue = 0;
		this.CurrentValue = baseValue;
	}

	public void Update() { this.CurrentValue = this._baseValue + this.BonusValue + (this._baseValue + this.BonusValue) * this.PercentageBonusValue; }

	public void LevelUp() { this._baseValue += this._scalingValue; }
}