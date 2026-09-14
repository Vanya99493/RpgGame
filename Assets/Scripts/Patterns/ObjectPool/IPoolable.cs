using System;

namespace Patterns
{
	public interface IPoolable
	{
		public event Action<IPoolable> ReturnToPoolEvent;
		
		public void ReturnToPool();
	}
}