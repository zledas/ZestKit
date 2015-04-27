﻿using UnityEngine;
using System.Collections.Generic;


namespace Prime31.ZestKit
{
	public partial class ZestKit : MonoBehaviour
	{
		public static EaseType defaultEaseType = EaseType.QuartIn;

		/// <summary>
		/// if enabled, does a null check on the object being tweened. If null, the tweened value will not be set.
		/// Only AbstractTweenTarget subclasses and Transform tweens will do validation (that includes all the built in tweens).
		/// It is up to any ITweenTarget custom implementations to add validation themselves if they want to take part in the babysitter.
		/// </summary>
		public static bool enableBabysitter = false;

		private List<ITweenable> _activeTweens = new List<ITweenable>();


		/// <summary>
		/// holds the singleton instance. creates one on demand if none exists.
		/// </summary>
		private static ZestKit _instance;
		public static ZestKit instance
		{
			get
			{
				if( !_instance )
				{
					// check if there is a GoKitLite instance already available in the scene graph before creating one
					_instance = FindObjectOfType( typeof( ZestKit ) ) as ZestKit;

					if( !_instance )
					{
						var obj = new GameObject( "ZestKit" );
						_instance = obj.AddComponent<ZestKit>();
						DontDestroyOnLoad( obj );
					}
				}

				return _instance;
			}
		}
			

		#region MonoBehaviour

		void Awake()
		{
			if( _instance == null )
				_instance = this;
		}


		private void OnApplicationQuit()
		{
			_instance = null;
			Destroy( gameObject );
		}


		void Update()
		{
			// loop backwards so we can remove completed tweens
			for( var i = _activeTweens.Count - 1; i >= 0; --i )
			{
				var tween = _activeTweens[i];
				if( tween.tick() )
					removeTween( tween, i );
			}
		}

		#endregion


		#region Tween management

		/// <summary>
		/// adds a tween to the active tweens list
		/// </summary>
		/// <param name="tween">Tween.</param>
		public void addTween( ITweenable tween )
		{
			_activeTweens.Add( tween );
		}


		/// <summary>
		/// removes the tween at index from the active tweens list.
		/// </summary>
		/// <param name="tween">Tween.</param>
		/// <param name="index">Index.</param>
		public void removeTween( ITweenable tween, int index )
		{
			_activeTweens.RemoveAt( index );
			tween.recycleSelf();
		}


		/// <summary>
		/// removes a tween from the active tweens list. List.Remove can be quite slow so it is preferable to sue the other
		/// removeTween variant.
		/// </summary>
		/// <param name="tween">Tween.</param>
		public void removeTween( ITweenable tween )
		{
			_activeTweens.Remove( tween );
			tween.recycleSelf();
		}


		/// <summary>
		/// stops all tweens optionlly bringing them all to completion
		/// </summary>
		/// <param name="bringToCompletion">If set to <c>true</c> bring to completion.</param>
		public void stopAllTweens( bool bringToCompletion = false )
		{
			for( var i = _activeTweens.Count - 1; i >= 0; --i )
				_activeTweens[i].stop( bringToCompletion );
		}


		/// <summary>
		/// returns all the tweens that have a specific context. Tweens are returned as ITweenControl since that is all
		/// that ZestKit knows about.
		/// </summary>
		/// <returns>The tweens with context.</returns>
		/// <param name="context">Context.</param>
		public List<ITweenControl> allTweensWithContext( object context )
		{
			var foundTweens = new List<ITweenControl>();

			for( var i = 0; i < _activeTweens.Count; i++ )
			{
				if( _activeTweens[i].context == context )
					foundTweens.Add( _activeTweens[i] );
			}

			return foundTweens;
		}

		#endregion

	}
}
