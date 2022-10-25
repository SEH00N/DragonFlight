using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class DEFINE
{
    #region consts
    
    public const float GravityScale = -9.81f;
    public const int GroundLayer = 1 << 7;
    public const int DragonLayer = 1 << 10; 

    private static Transform mainCanvas = null;
    public static Transform MainCanvas {
        get {
            if(mainCanvas == null)
                mainCanvas = GameObject.Find("MainCanvas").transform;
        
            return mainCanvas;
        }
    }

    private static Camera mainCam = null;
    public static Camera MainCam {
        get {
            if(mainCam == null)
                mainCam = Camera.main;
            return mainCam;
        }
    }

    #endregion

    private static CinemachineVirtualCamera cmMainCam = null;
    public static CinemachineVirtualCamera CmMainCam {
        get {
            if(cmMainCam == null)
                cmMainCam = GameObject.Find("CmMainCam").GetComponent<CinemachineVirtualCamera>();

            return cmMainCam;
        }
    }
}
