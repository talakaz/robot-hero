using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Known : MonoBehaviour {


	public static bool P1_Ready;
	public static bool P2_Ready;
	public static bool Players_Ready;
	public static string Tracked_Card_info_1;
	public static string Tracked_Card_info_2;
	public static int VumarkCard1ID_idx = -1;
	public static int VumarkCard2ID_idx = -1;


	public static void Reset(){
		P1_Ready = false;
		P2_Ready = false;
		Players_Ready = false;
	}
	public static void SetTrackingType(int T){
		VuMarkHandler.TrackingType = T;
	}
}
