using System;

namespace UnityCore.ReactiveProperties
{
	public class DecimalReactive : ReactiveProperty<decimal>, INumericReactive
	{
		public float GetFloatValue()
		{
			return Convert.ToInt32(Value);
		}

		public void SetFromFloat(float value)
		{
			Value = Convert.ToDecimal(value);
		}
	}
}