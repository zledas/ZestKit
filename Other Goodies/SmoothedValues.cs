﻿using UnityEngine;
using System.Collections;


namespace Prime31.ZestKit
{
	/// <summary>
	/// SmoothedValue subclasses were made to address the the propensity to misuse Lerps in Update/FixedUpdate/Coroutines. It is a common
	/// pattern to do things camera follow and other movement via Lerp( from, to, Time.deltaTime * someValue ). Lerping like that isnt
	/// really doing much more than jumping towards the to value with an exponential out ease. The target will never reach the to value.
	/// 
	/// SmoothedValue will do the same basic thing with 2 major differences: you can choose the ease type and the target will reach
	/// the to value at duration has passed.
	/// </summary>
	public abstract class SmoothedValue<T> where T : struct
	{
		public EaseType easeType = ZestKit.defaultEaseType;

		protected float _duration;
		protected float _startTime;

		protected T _currentValue;
		protected T _fromValue;
		protected T _toValue;

		protected bool _isTimeScaleIndependent;

		protected bool _finalValueReturned;
		public bool finalValueReturned {
			get {
				return _finalValueReturned;
			}
		}

		public abstract T value { get; }


		public SmoothedValue( T currentValue, float duration = 0.3f, bool isTimeScaleIndependent = false)
		{
			_isTimeScaleIndependent = isTimeScaleIndependent;
			_duration = duration;
			_startTime = (_isTimeScaleIndependent ? Time.unscaledTime : Time.time);
			_finalValueReturned = false;

			_currentValue = currentValue;
			_fromValue = currentValue;
			_toValue = currentValue;
		}

		public SmoothedValue( T currentValue, EaseType easeType, float duration = 0.3f, bool isTimeScaleIndependent = false ): this(currentValue, duration, isTimeScaleIndependent)
		{
			this.easeType = easeType;
		}


		public void setToValue( T toValue )
		{
			_startTime = (_isTimeScaleIndependent ? Time.unscaledTime : Time.time);
			_finalValueReturned = false;
			_fromValue = _currentValue;
			_toValue = toValue;
		}

		public void setToValue( T toValue, float duration )
		{
			_duration = duration;
			_startTime = (_isTimeScaleIndependent ? Time.unscaledTime : Time.time);
			_finalValueReturned = false;
			_fromValue = _currentValue;
			_toValue = toValue;
		}


		public void resetFromAndToValues( T fromValue, T toValue )
		{
			_startTime = (_isTimeScaleIndependent ? Time.unscaledTime : Time.time);
			_finalValueReturned = false;
			_fromValue = fromValue;
			_toValue = toValue;
		}

		public void resetFromAndToValues( T fromValue, T toValue, float duration )
		{
			_duration = duration;
			_startTime = (_isTimeScaleIndependent ? Time.unscaledTime : Time.time);
			_finalValueReturned = false;
			_fromValue = fromValue;
			_toValue = toValue;
		}
	}


	public class SmoothedFloat : SmoothedValue<float>
	{
		public SmoothedFloat( float currentValue, float duration = 0.3f, bool isTimeScaleIndependent = false ) : base( currentValue, duration, isTimeScaleIndependent )
		{}

		public SmoothedFloat( float currentValue, EaseType easeType, float duration = 0.3f, bool isTimeScaleIndependent = false ) : base( currentValue, easeType, duration, isTimeScaleIndependent )
		{}

		public override float value
		{
			get
			{
				// skip the calculation if we are already at our target
				if( _finalValueReturned )
					return _currentValue;

				// how far along are we?
				var elapsedTime = Mathf.Clamp( (_isTimeScaleIndependent ? Time.unscaledTime : Time.time) - _startTime, 0f, _duration );
				if (elapsedTime == _duration) {
					_finalValueReturned = true;
				}
				_currentValue = Zest.ease( easeType, _fromValue, _toValue, elapsedTime, _duration );

				return _currentValue;
			}
		}
	}


	public class SmoothedVector2 : SmoothedValue<Vector2>
	{
		public SmoothedVector2( Vector2 currentValue, float duration = 0.3f, bool isTimeScaleIndependent = false ) : base( currentValue, duration, isTimeScaleIndependent )
		{}

		public SmoothedVector2( Vector2 currentValue, EaseType easeType, float duration = 0.3f, bool isTimeScaleIndependent = false ) : base( currentValue, easeType, duration, isTimeScaleIndependent )
		{}

		public override Vector2 value
		{
			get
			{
				// skip the calculation if we are already at our target
				if( _finalValueReturned )
					return _currentValue;

				// how far along are we?
				var elapsedTime = Mathf.Clamp( (_isTimeScaleIndependent ? Time.unscaledTime : Time.time) - _startTime, 0f, _duration );
				if (elapsedTime == _duration) {
					_finalValueReturned = true;
				}
				_currentValue = Zest.ease( easeType, _fromValue, _toValue, elapsedTime, _duration );

				return _currentValue;
			}
		}
	}


	public class SmoothedVector3 : SmoothedValue<Vector3>
	{
		public SmoothedVector3( Vector3 currentValue, float duration = 0.3f, bool isTimeScaleIndependent = false ) : base( currentValue, duration, isTimeScaleIndependent )
		{}

		public SmoothedVector3( Vector3 currentValue, EaseType easeType, float duration = 0.3f, bool isTimeScaleIndependent = false ) : base( currentValue, easeType, duration, isTimeScaleIndependent )
		{}

		public override Vector3 value
		{
			get
			{
				// skip the calculation if we are already at our target
				if( _finalValueReturned )
					return _currentValue;

				// how far along are we?
				var elapsedTime = Mathf.Clamp( (_isTimeScaleIndependent ? Time.unscaledTime : Time.time) - _startTime, 0f, _duration );
				if (elapsedTime == _duration) {
					_finalValueReturned = true;
				}
				_currentValue = Zest.ease( easeType, _fromValue, _toValue, elapsedTime, _duration );

				return _currentValue;
			}
		}
	}

}