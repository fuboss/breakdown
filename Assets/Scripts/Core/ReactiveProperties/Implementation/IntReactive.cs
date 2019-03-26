using System;

namespace UnityCore.ReactiveProperties
{
	public class IntReactive : ReactiveProperty<int>, INumericReactive
	{
		public IntReactive()
			: base()
		{
		}

		public IntReactive(int value)
			: base(value)
		{
		}

		public float GetFloatValue()
		{
			return Convert.ToSingle(Value);
		}

		public void SetFromFloat(float value)
		{
			Value = Convert.ToInt32(value);
		}
	}
}