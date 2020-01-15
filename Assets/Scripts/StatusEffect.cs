public class StatusEffect {
	public enum StatusEffectType { immobilize, silence }

	public StatusEffectType type;
	private System.DateTime startTime;
	public double severity;
	private double originalSeverity;
	private System.TimeSpan duration;
	private bool decaying;
	private bool reverseDecaying;
	private Entity target;

	public StatusEffect(StatusEffectType type, double severity, double duration, bool decaying, bool reverseDecaying, Entity target) {
		this.type = type;
		this.startTime = System.DateTime.Now;
		this.severity = severity;
		this.originalSeverity = severity;
		this.duration = System.TimeSpan.FromSeconds(duration);
		this.target = target;
		this.decaying = decaying;
		this.reverseDecaying = reverseDecaying;
		target.currentStatusEffects.Add(this);
	}

	private void Update() {
		System.TimeSpan timePassed = System.DateTime.Now - this.startTime;
		System.TimeSpan remainingTime = this.duration - timePassed;
		if (remainingTime < System.TimeSpan.Zero) { this.target.currentStatusEffects.Remove(this); }

		if (this.decaying) { this.severity = this.originalSeverity - ((timePassed.TotalSeconds / this.duration.TotalSeconds) * this.originalSeverity); } else if (this.reverseDecaying) { this.severity = (timePassed.TotalSeconds / this.duration.TotalSeconds) * this.originalSeverity; }
	}
}