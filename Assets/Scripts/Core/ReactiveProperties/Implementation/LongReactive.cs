using System;

namespace UnityCore.ReactiveProperties
{
	public class LongReactive : ReactiveProperty<long>, INumericReactive
	{
		public float GetFloatValue()
		{
			return Convert.ToSingle(Value);
		}

		public void SetFromFloat(float value)
		{
			Value = Convert.ToInt64(value);
		}
	}
}