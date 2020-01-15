public class StatusEffect {
	public enum StatusEffectType { slow, stun, immobilize, silence }

	public StatusEffectType type;
	private System.DateTime startTime;
	private double severity;
	private double duration;
	private Entity target;

	public StatusEffect(StatusEffectType type, double severity, double duration, Entity target) {
		this.type = type;
		this.startTime = System.DateTime.Now;
		this.severity = severity;
		this.duration = duration;
		this.target = target;
		target.currentStatusEffects.Add(this);
	}

	private void Update() {
		if (System.TimeSpan.FromSeconds(this.duration) < System.DateTime.Now - this.startTime) { this.target.currentStatusEffects.Remove(this); }
	}
}