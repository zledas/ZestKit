#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// place this script on any GameObject to enable route editing. note that it is not required at runtime! it is
/// only required to be in your scene while editing a path
/// </summary>
namespace Prime31.ZestKit
{
	public class DummySpline : MonoBehaviour
	{
		public string pathName = string.Empty;
		public Color pathColor = Color.magenta; // color of the path if visible in the editor
		public List<Vector3> nodes = new List<Vector3>() { Vector3.zero, Vector3.zero };
		public bool useStandardHandles = false;
		public bool forceStraightLinePath = false;
		public int pathResolution = 50;
	
	
		public void OnDrawGizmosSelected()
		{
			// the editor will draw paths when force straight line is on
			if( !forceStraightLinePath )
			{
				var spline = new Spline( nodes );
				Gizmos.color = pathColor;
				spline.drawGizmos( pathResolution );
			}
		}

	}
}
#endif