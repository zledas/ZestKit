﻿using UnityEngine;
using System.Collections;


namespace ZestKit
{
	public class SplineTween : Tween<Vector3>, ITweenTarget<Vector3>
	{
		Transform _transform;
		Spline _spline;
		bool _isRelativeTween;


		public SplineTween( Transform transform, Spline spline, float duration, bool isRelativeTween = false )
		{
			_transform = transform;
			_spline = spline;
			_spline.buildPath();

			initialize( this, _transform.position, Vector3.zero, duration );
			_isRelativeTween = isRelativeTween;
		}


		public void setTweenedValue( Vector3 value )
		{
			_transform.position = value;
		}


		protected override void updateValue()
		{
			var easedTime = EaseHelper.ease( _easeType, _elapsedTime, _duration );
			var position = _spline.getPointOnPath( easedTime );

			// if this is a relative tween we use the fromValue (initial position) as a base and add the spline to it
			if( _isRelativeTween )
				position += _fromValue;

			setTweenedValue( position );
		}
	}
}