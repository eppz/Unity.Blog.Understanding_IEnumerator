﻿using UnityEngine;
using System.Collections;


namespace EPPZ.Blog.IEnumerator_Example_C
{


	[System.Serializable]
	public class Polygon
	{


		public Vector2[] points;
		public Polygon[] polygons;


		public IEnumerator PointEnumerator()
		{
			// Enumerate local points.
			foreach (Vector2 eachPoint in points)
			{
				yield return eachPoint;
			}
			
			// Enumerate each sub-polygon points.
			foreach (Polygon eachPolygon in polygons)
			{
				IEnumerator pointEnumerator = eachPolygon.PointEnumerator();
				while (pointEnumerator.MoveNext())
				{
					yield return pointEnumerator.Current;
				}
			}
			
			yield break;
		}
	}


	public class IEnumerator_Example_C : MonoBehaviour
	{


		public Polygon polygon; // Setup in inspector


		void Start()
		{
			float start = Time.realtimeSinceStartup;
			for (int i = 0; i < 100000; i++)
			{
				// Enumerate points (recursive).
				IEnumerator pointEnumerator = polygon.PointEnumerator ();
				while (pointEnumerator.MoveNext())
				{
					// Debug.Log (pointEnumerator.Current);
				}
			}
			float end = Time.realtimeSinceStartup;
			Debug.Log("100000 enumerations using IEnumerator: "(end - start)+" ms lapsed ().");
		}
	}
}