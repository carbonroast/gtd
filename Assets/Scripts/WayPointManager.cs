using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour {
	//private static Dictionary <string, GameObject> _wayPoints = new Dictionary<string, GameObject>();
	private static List <Vector3> _wayPoints = new List<Vector3>();

	public static void RegisterWayPoints ( Vector3 _WayPointLocation){
		_wayPoints.Add (_WayPointLocation);
	}

	public static Vector3 GetWayPoints(int _WayPoint){
		return _wayPoints [_WayPoint];
	}

	public static int  GetSize(){
		return _wayPoints.Count;
	}
}
