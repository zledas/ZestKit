﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace ZestKit
{
	public class TransformRotationTarget : AbstractTweenTarget<Transform,Quaternion>
	{
		public enum TransformRotationType
		{
			Rotation,
			LocalRotation
		}
			
		TransformRotationType _targetType;


		public override void setTweenedValue( Quaternion value )
		{
			switch( _targetType )
			{
				case TransformRotationType.Rotation:
					_target.rotation = value;
					break;
				case TransformRotationType.LocalRotation:
					_target.localRotation = value;
					break;
			}
		}
	

		public TransformRotationTarget( Transform transform, TransformRotationType targetType )
		{
			_target = transform;
			_targetType = targetType;
		}
	}


	#region Material targets

	public abstract class AbstractMaterialTarget
	{
		protected Material _material;
		protected string _propertyName;


		public void prepareForUse( Material material, string propertyName )
		{
			_material = material;
			_propertyName = propertyName;
		}
	}


	public class MaterialColorTarget : AbstractMaterialTarget, ITweenTarget<Color>
	{
		public MaterialColorTarget( Material material, string propertyName )
		{
			prepareForUse( material, propertyName );
		}


		public void setTweenedValue( Color value )
		{
			_material.SetColor( _propertyName, value );
		}
	}


	public class MaterialAlphaTarget : AbstractMaterialTarget, ITweenTarget<float>
	{
		public MaterialAlphaTarget( Material material, string propertyName )
		{
			prepareForUse( material, propertyName );
		}


		public void setTweenedValue( float value )
		{
			var color = _material.GetColor( _propertyName );
			color.a = value;
			_material.SetColor( _propertyName, color );
		}
	}


	public class MaterialFloatTarget : AbstractMaterialTarget, ITweenTarget<float>
	{
		public MaterialFloatTarget( Material material, string propertyName )
		{
			prepareForUse( material, propertyName );
		}


		public void setTweenedValue( float value )
		{
			_material.SetFloat( _propertyName, value );
		}
	}


	public class MaterialVector4Target : AbstractMaterialTarget, ITweenTarget<Vector4>
	{
		public MaterialVector4Target( Material material, string propertyName )
		{
			prepareForUse( material, propertyName );
		}


		public void setTweenedValue( Vector4 value )
		{
			_material.SetVector( _propertyName, value );
		}
	}

	#endregion



	public class AudioSourceFloatTarget : AbstractTweenTarget<AudioSource,float>
	{
		public enum AudioSourceFloatType
		{
			Volume,
			Pitch,
			PanStereo
		}
			
		AudioSourceFloatType _tweenType;


		public override void setTweenedValue( float value )
		{
			switch( _tweenType )
			{
				case AudioSourceFloatType.Volume:
					_target.volume = value;
					break;
				case AudioSourceFloatType.Pitch:
					_target.pitch = value;
					break;
				case AudioSourceFloatType.PanStereo:
					_target.panStereo = value;
					break;
			}
		}


		public AudioSourceFloatTarget( AudioSource audioSource, AudioSourceFloatType targetType )
		{
			_target = audioSource;
			_tweenType = targetType;
		}
	}


	/// <summary>
	/// when used with a FloatTween the CameraTargetType determines what is tweened. When used with a ColorTween
	/// the backgroundColor is tweened
	/// </summary>
	public class CameraTarget : AbstractTweenTarget<Camera,float>, ITweenTarget<Color>
	{
		public enum CameraTargetType
		{
			OrthographicSize,
			FieldOfView
		}

		CameraTargetType _targetType;


		public override void setTweenedValue( float value )
		{
			switch( _targetType )
			{
				case CameraTargetType.OrthographicSize:
					_target.orthographicSize = value;
					break;
				case CameraTargetType.FieldOfView:
					_target.fieldOfView = value;
					break;
			}
		}


		public void setTweenedValue( Color value )
		{
			_target.backgroundColor = value;
		}


		public CameraTarget( Camera camera, CameraTargetType targetType = CameraTargetType.OrthographicSize )
		{
			_target = camera;
			_targetType = CameraTargetType.OrthographicSize;
		}
	}


	public class CanvasGroupTarget : AbstractTweenTarget<CanvasGroup,float>
	{
		public override void setTweenedValue( float value )
		{
			_target.alpha = value;
		}


		public CanvasGroupTarget( CanvasGroup canvasGroup )
		{
			_target = canvasGroup;
		}
	}


	/// <summary>
	/// when used with a FloatTween the ImageTargetType determines what is tweened. When used with a ColorTween
	/// the color is tweened
	/// </summary>
	public class ImageTarget : AbstractTweenTarget<Image,float>, ITweenTarget<Color>
	{
		public enum ImageTargetType
		{
			Alpha,
			FillAmount
		}

		ImageTargetType _targetType;
		

		public override void setTweenedValue( float value )
		{
			switch( _targetType )
			{
				case ImageTargetType.Alpha:
					var color = _target.color;
					color.a = value;
					_target.color = color;
					break;
				case ImageTargetType.FillAmount:
					_target.fillAmount = value;
					break;
			}
		}
	

		public void setTweenedValue( Color value )
		{
			_target.color = value;
		}


		public ImageTarget( Image image, ImageTargetType targetType = ImageTargetType.Alpha )
		{
			_target = image;
			_targetType = targetType;
		}
	}


	/// <summary>
	/// when used with a Vector3Tween the anchoredPosition3D will be tweened and when used with a Vector2Tween
	/// the anchoredPosition will be tweened
	/// </summary>
	public class RectTransformTarget : AbstractTweenTarget<RectTransform,Vector3>, ITweenTarget<Vector2>
	{
		public override void setTweenedValue( Vector3 value )
		{
			_target.anchoredPosition3D = value;
		}


		public void setTweenedValue( Vector2 value )
		{
			_target.anchoredPosition = value;
		}


		public RectTransformTarget( RectTransform rectTransform )
		{
			_target = rectTransform;
		}
	}

}
