using System;

namespace Utils
{
	public static class GuidGenerator
	{
		public static string GenerateGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}