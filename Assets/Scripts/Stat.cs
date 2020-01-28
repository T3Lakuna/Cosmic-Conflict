public class Stat {
	public enum StatId {
		Damage, // Physical damage
		Magic, // Magical damage
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

	public float CurrentValue;
	private float _baseValue;
	private readonly float _scalingValue;
	public float BonusValue;
	public float PercentageBonusValue;
	public StatId Id;

	public Stat(float baseValue, float scalingValue, StatId id) {
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