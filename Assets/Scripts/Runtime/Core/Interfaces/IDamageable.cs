using Runtime.Models.Damage;

namespace Runtime.Interfaces
{
	public interface IDamageable
	{
		void TakeDamage(in DamageArgs args);
	}
}