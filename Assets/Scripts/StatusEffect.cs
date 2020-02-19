using System;

public class StatusEffect {
	// TODO: When rewriting, move StatusEffect stuff into Ability for consistency.

	public enum StatusEffectType { immobilize, silence }

	public StatusEffectType type;
	private System.DateTime startTime;
	private System.TimeSpan duration;
	private Entity target;

	public StatusEffect(StatusEffectType type, float duration, Entity target) {
		this.type = type;
		this.startTime = System.DateTime.Now;
		this.duration = System.TimeSpan.FromSeconds(duration * Math.Max(1 - target.tenacity.CurrentValue, 0));
		this.target = target;
		target.currentStatusEffects.Add(this.type);
	}

	private void Update() {
		System.TimeSpan timePassed = System.DateTime.Now - this.startTime;
		System.TimeSpan remainingTime = this.duration - timePassed;
		if (remainingTime < System.TimeSpan.Zero) { this.target.currentStatusEffects.Remove(this.type); }
	}
}