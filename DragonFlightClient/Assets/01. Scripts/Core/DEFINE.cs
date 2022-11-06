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
    public const int EnemyDragonLayer = 1 << 11;
    public const int EnemyPlayerLayer = 1 << 12;

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

    private static CinemachineVirtualCamera cmMainCam = null;
    public static CinemachineVirtualCamera CmMainCam {
        get {
            if(cmMainCam == null)
                cmMainCam = GameObject.Find("CmMainCam").GetComponent<CinemachineVirtualCamera>();

            return cmMainCam;
        }
    }
    #endregion

    #region player properties

    private static Player player = null;
    public static Player Player {
        get {
            if(player == null)
                player = GameObject.Find("Player").GetComponent<Player>();

            return player;
        }
    }

    private static Dragon dragon = null;
    public static Dragon Dragon {
        get {
            if(dragon == null)  
                dragon = GameObject.Find("Dragon").GetComponent<Dragon>();

            return dragon;
        }
    }

    private static OtherPlayer otherPlayer = null;
    public static OtherPlayer OtherPlayer {
        get {
            if(otherPlayer == null)  
                otherPlayer = GameObject.Find("OtherPlayer").GetComponent<OtherPlayer>();

            return otherPlayer;
        }
    }

    private static OtherDragon otherDragon = null;
    public static OtherDragon OtherDragon {
        get {
            if(otherDragon == null)  
                otherDragon = GameObject.Find("OtherDragon").GetComponent<OtherDragon>();

            return otherDragon;
        }
    }

    #endregion
}
