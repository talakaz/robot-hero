/*===============================================================================
Copyright (c) 2016-2017 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using System.Collections;
using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler which uses the VuMarkManager.
/// </summary>
public class VuMarkHandler : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES

   // PanelShowHide m_IdPanel;
    VuMarkManager m_VuMarkManager;
    VuMarkTarget m_ClosestVuMark;
    VuMarkTarget m_CurrentVuMark;
	int VumarkAmount = 0;
	float Timer;
	public static int TrackingType = 0;
	VuMarkBehaviour[] bhvrs = new VuMarkBehaviour[2];

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // register callbacks to VuMark Manager
        m_VuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        m_VuMarkManager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
        m_VuMarkManager.RegisterVuMarkLostCallback(OnVuMarkLost);
    }

    void Update()
    {
//		if(!Known.Tracking){
//			return;
//		}
		#if UNITY_EDITOR || PLATFORM_IOS 
		if(VumarkAmount == 1){
			switch (TrackingType) {
			case 2:
				foreach (VuMarkBehaviour bhvr in m_VuMarkManager.GetActiveBehaviours()) {
					int Idx = int.Parse (bhvr.VuMarkTarget.InstanceId.ToString ());
					if (Idx <= Configurations.Hero_Amount) {
						Known.VumarkCard1ID_idx = Idx;
						TimerRun ();
						Debug.Log ("1P=" + Known.VumarkCard1ID_idx+" "+Known.P1_Ready);
					}
				}
				break;
			case 3:
				foreach (VuMarkBehaviour bhvr in m_VuMarkManager.GetActiveBehaviours()) {
					int Idx = int.Parse (bhvr.VuMarkTarget.InstanceId.ToString ());
					if (Idx <= Configurations.Hero_Amount) {
						Known.VumarkCard2ID_idx = Idx;
						TimerRun ();
						Debug.Log ("2P=" + Known.VumarkCard2ID_idx+" "+Known.P2_Ready);
					}
				}
				break;
			}
		}

		if (VumarkAmount == 2 && TrackingType != 2 && TrackingType != 3) {
			foreach (VuMarkBehaviour bhvr in m_VuMarkManager.GetActiveBehaviours()) {
				var position = bhvr.transform.localPosition;
				if (bhvrs [0] == null) {
					bhvrs [0] = bhvr;
				} else if (bhvrs [0] != null) {
					if (position.x < bhvrs [0].transform.localPosition.x) {
						bhvrs [1] = bhvr;
					} else if (position.x > bhvrs [0].transform.localPosition.x) {
						bhvrs [1] = bhvrs [0];
						bhvrs [0] = bhvr;
					}
				}
				if(bhvrs[0] != null && bhvrs[1] != null){
					int Idx1 = int.Parse(bhvrs[0].VuMarkTarget.InstanceId.ToString());
					int Idx2 = int.Parse(bhvrs[1].VuMarkTarget.InstanceId.ToString());
					switch(TrackingType){
					case 0:
						Known.VumarkCard1ID_idx = Idx1;
						Known.VumarkCard2ID_idx = Idx2;
						break;
					case 1:
						if(Idx1 <= Configurations.Hero_Amount && Idx2 <= Configurations.Hero_Amount){
							Known.VumarkCard1ID_idx = Idx1;
							Known.VumarkCard2ID_idx = Idx2;
							TimerRun ();
						}
						break;
					case 4:
						if(Idx1 >= Configurations.Battle_Card_from && Idx1 <= Configurations.Battle_Card_to && Idx2 >= Configurations.Battle_Card_from && Idx2 <= Configurations.Battle_Card_to){
							Known.VumarkCard1ID_idx = Idx1;
							Known.VumarkCard2ID_idx = Idx2;
							TimerRun ();
						}
						break;
					}
						

//					Known.Tracked_Card_info_1 = bhvrs[0].VuMarkTarget.InstanceId.ToString () + " X:" + bhvrs[0].transform.localPosition.x.ToString();
//					Known.VuMarkCard2ID = bhvrs [1].VuMarkTarget.InstanceId.ToString ();
//					Known.Tracked_Card_info_2 = bhvrs[1].VuMarkTarget.InstanceId.ToString () + " X:" + bhvrs[1].transform.localPosition.x.ToString();
//					Debug.Log ("Left:" + bhvrs [0].VuMarkTarget.InstanceId.ToString()+ " x:" + bhvrs[0].transform.localPosition.x
//						+"\n"
//						+ " Right:"+bhvrs[1].VuMarkTarget.InstanceId.ToString()+" x:"+bhvrs[1].transform.localPosition.x);
					Debug.Log("1P:"+Known.VumarkCard1ID_idx+" 2P:"+Known.VumarkCard2ID_idx+" "+ Known.Players_Ready + " " + TrackingType);
				}

				//hard work goes here
			}
		} else if (VumarkAmount < 2 || VumarkAmount > 2) {
			for (int i = 0; i < 2; i++) {
				bhvrs [i] = null;
			}
		}
		#endif

		#if !UNITY_EDITOR
		if(VumarkAmount == 1){
			switch (TrackingType) {
			case 2:
				foreach (VuMarkBehaviour bhvr in m_VuMarkManager.GetActiveBehaviours()) {
					int Idx = int.Parse (bhvr.VuMarkTarget.InstanceId.ToString ());
					if (Idx <= Configurations.Hero_Amount) {
						Known.VumarkCard1ID_idx = Idx;
						TimerRun ();
						Debug.Log ("1P=" + Known.VumarkCard1ID_idx+" "+Known.P1_Ready);
					}
				}
				break;
			case 3:
				foreach (VuMarkBehaviour bhvr in m_VuMarkManager.GetActiveBehaviours()) {
					int Idx = int.Parse (bhvr.VuMarkTarget.InstanceId.ToString ());
					if (Idx <= Configurations.Hero_Amount) {
						Known.VumarkCard2ID_idx = Idx;
						TimerRun ();
						Debug.Log ("2P=" + Known.VumarkCard2ID_idx+" "+Known.P2_Ready);
					}
				}
				break;
			}
		}

		if (VumarkAmount == 2 && TrackingType != 2 && TrackingType != 3) {
			foreach (VuMarkBehaviour bhvr in m_VuMarkManager.GetActiveBehaviours()) {
				var position = bhvr.transform.localPosition;
				if (bhvrs [0] == null) {
					bhvrs [0] = bhvr;
				} else if (bhvrs [0] != null) {
					if (position.x > bhvrs [0].transform.localPosition.x) {
						bhvrs [1] = bhvr;
					} else if (position.x < bhvrs [0].transform.localPosition.x) {
						bhvrs [1] = bhvrs [0];
						bhvrs [0] = bhvr;
					}
				}
				if(bhvrs[0] != null && bhvrs[1] != null){
					int Idx1 = int.Parse(bhvrs[0].VuMarkTarget.InstanceId.ToString());
					int Idx2 = int.Parse(bhvrs[1].VuMarkTarget.InstanceId.ToString());
					switch(TrackingType){
					case 0:
						Known.VumarkCard1ID_idx = Idx1;
						Known.VumarkCard2ID_idx = Idx2;
						break;
					case 1:
						if(Idx1 <= Configurations.Hero_Amount && Idx2 <= Configurations.Hero_Amount){
							Known.VumarkCard1ID_idx = Idx1;
							Known.VumarkCard2ID_idx = Idx2;
							TimerRun ();
						}
						break;
					case 4:
						if(Idx1 >= Configurations.Battle_Card_from && Idx1 <= Configurations.Battle_Card_to && Idx2 >= Configurations.Battle_Card_from && Idx2 <= Configurations.Battle_Card_to){
							Known.VumarkCard1ID_idx = Idx1;
							Known.VumarkCard2ID_idx = Idx2;
							TimerRun ();
						}
						break;
					}


					//					Known.Tracked_Card_info_1 = bhvrs[0].VuMarkTarget.InstanceId.ToString () + " X:" + bhvrs[0].transform.localPosition.x.ToString();
					//					Known.VuMarkCard2ID = bhvrs [1].VuMarkTarget.InstanceId.ToString ();
					//					Known.Tracked_Card_info_2 = bhvrs[1].VuMarkTarget.InstanceId.ToString () + " X:" + bhvrs[1].transform.localPosition.x.ToString();
					//					Debug.Log ("Left:" + bhvrs [0].VuMarkTarget.InstanceId.ToString()+ " x:" + bhvrs[0].transform.localPosition.x
					//						+"\n"
					//						+ " Right:"+bhvrs[1].VuMarkTarget.InstanceId.ToString()+" x:"+bhvrs[1].transform.localPosition.x);
					Debug.Log("1P:"+Known.VumarkCard1ID_idx+" 2P:"+Known.VumarkCard2ID_idx+" "+ Known.Players_Ready + " " + TrackingType);
				}

				//hard work goes here
			}
		} else if (VumarkAmount < 2 || VumarkAmount > 2) {
			for (int i = 0; i < 2; i++) {
				bhvrs [i] = null;
			}
		}
		#endif

    }

    void OnDestroy()
    {
        // unregister callbacks from VuMark Manager
        m_VuMarkManager.UnregisterVuMarkDetectedCallback(OnVuMarkDetected);
        m_VuMarkManager.UnregisterVuMarkLostCallback(OnVuMarkLost);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

	public void TimerRun(){
		if (TrackingType == 1 || TrackingType == 4) {
			Timer += Time.deltaTime / 2;
		} else {
			Timer += Time.deltaTime;
		}

		if (Timer > Configurations.Timer_Known_Delay) {
			switch (TrackingType) {
			case 1:
			case 4:
				Known.Players_Ready = true;
				break;
			case 2:
				Known.P1_Ready = true;
				break;
			case 3:
				Known.P2_Ready = true;
				break;
			}
			Timer = 0;
		}
	}
    /// <summary>
    /// This method will be called whenever a new VuMark is detected
    /// </summary>
    public void OnVuMarkDetected(VuMarkTarget target)
    {
		if (TrackingType == 1 || TrackingType == 2 || TrackingType == 3 || TrackingType == 4) {
			QA.Invoke<int>(Tag.AUDIO_PLAY_ONESHOT, 2);
		}
		Debug.Log("New VuMark: " + GetVuMarkId(target));
		VumarkAmount += 1;

    }

    /// <summary>
    /// This method will be called whenever a tracked VuMark is lost
    /// </summary>
    public void OnVuMarkLost(VuMarkTarget target)
    {
        Debug.Log("Lost VuMark: " + GetVuMarkId(target));
		VumarkAmount -= 1;
		Timer = 0;
		Known.Reset ();
		if (Known.VumarkCard1ID_idx.ToString() == GetVuMarkId (target)) {
			Known.VumarkCard1ID_idx = -1;
		} else if (Known.VumarkCard2ID_idx.ToString() == GetVuMarkId (target)) {
			Known.VumarkCard2ID_idx = -1;
		}
//        if (target == m_CurrentVuMark)
           // m_IdPanel.ResetShowTrigger();
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS

    void UpdateClosestTarget()
    {
        Camera cam = DigitalEyewearARController.Instance.PrimaryCamera ?? Camera.main;

        float closestDistance = Mathf.Infinity;

        foreach (var bhvr in m_VuMarkManager.GetActiveBehaviours())
        {
            Vector3 worldPosition = bhvr.transform.position;
            Vector3 camPosition = cam.transform.InverseTransformPoint(worldPosition);

            float distance = Vector3.Distance(Vector2.zero, camPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                m_ClosestVuMark = bhvr.VuMarkTarget;
            }
        }

        if (m_ClosestVuMark != null &&
            m_CurrentVuMark != m_ClosestVuMark)
        {
           // var vuMarkId = GetVuMarkId(m_ClosestVuMark);
            //var vuMarkDataType = GetVuMarkDataType(m_ClosestVuMark);
           // var vuMarkImage = GetVuMarkImage(m_ClosestVuMark);
            //var vuMarkDesc = GetNumericVuMarkDescription(m_ClosestVuMark);

            m_CurrentVuMark = m_ClosestVuMark;

           // StartCoroutine(ShowPanelAfter(0.5f, vuMarkId, vuMarkDataType, vuMarkDesc, vuMarkImage));
        }
    }
		

    string GetVuMarkDataType(VuMarkTarget vumark)
    {
        switch (vumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return "Bytes";
            case InstanceIdType.STRING:
                return "String";
            case InstanceIdType.NUMERIC:
                return "Numeric";
        }
        return string.Empty;
    }

    string GetVuMarkId(VuMarkTarget vumark)
    {
        switch (vumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return vumark.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
                return vumark.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
                return vumark.InstanceId.NumericValue.ToString();
        }
        return string.Empty;
    }

    Sprite GetVuMarkImage(VuMarkTarget vumark)
    {
        var instanceImg = vumark.InstanceImage;
        if (instanceImg == null)
        {
            Debug.Log("VuMark Instance Image is null.");
            return null;
        }

        // First we create a texture
        Texture2D texture = new Texture2D(instanceImg.Width, instanceImg.Height, TextureFormat.RGBA32, false)
        {
            wrapMode = TextureWrapMode.Clamp
        };
        instanceImg.CopyToTexture(texture);

        // Then we turn the texture into a Sprite
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }

    string GetNumericVuMarkDescription(VuMarkTarget vumark)
    {
        int vuMarkIdNumeric;

        if (int.TryParse(GetVuMarkId(vumark), out vuMarkIdNumeric))
        {
            // Change the description based on the VuMark id
            switch (vuMarkIdNumeric % 4)
            {
                case 1:
                    return "Astronaut";
                case 2:
                    return "Drone";
                case 3:
                    return "Fissure";
                case 0:
                    return "Oxygen Tank";
                default:
                    return "Astronaut";
            }
        }

        return string.Empty; // if VuMark DataType is byte or string
    }

    #endregion // PRIVATE_METHODS
}
