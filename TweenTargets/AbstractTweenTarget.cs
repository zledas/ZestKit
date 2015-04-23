﻿using UnityEngine;
using System.Collections;


namespace ZestKit
{
	/// <summary>
	/// helper base class to make creating custom ITweenTargets as trivial as possible
	/// </summary>
	public abstract class AbstractTweenTarget<U,T> : ITweenTarget<T> where T : struct
	{
		protected U _target;

		abstract public void setTweenedValue( T value );


		public void setTarget( U target )
		{
			_target = target;
		}
	}
}