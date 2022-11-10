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
    public static bool Ready2Start = false;

    #endregion

    #region canvas

    private static Transform mainCanvas = null;
    public static Transform MainCanvas {
        get {
            if(mainCanvas == null)
                mainCanvas = GameObject.Find("MainCanvas").transform;
        
            return mainCanvas;
        }
    }

    private static Transform staticCanvas = null;
    public static Transform StaticCanvas {
        get {
            if(staticCanvas == null)
                staticCanvas = GameObject.Find("StaticCanvas").transform;

            return staticCanvas;
        }
    }

    #endregion

    #region camera

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

    private static OtherPlayer otherPlayer = null;
    public static OtherPlayer OtherPlayer {
        get {
            if(otherPlayer == null)  
                otherPlayer = GameObject.Find("EnemyPlayer").GetComponent<OtherPlayer>();

            return otherPlayer;
        }
    }

    #endregion

    #region dragon properties

    private static Dragon dragon = null;
    public static Dragon Dragon {
        get {
            if(dragon == null)  
                dragon = GameObject.Find("BlueThinDragon").GetComponent<Dragon>();

            return dragon;
        }
    }

    private static OtherDragon otherDragon = null;
    public static OtherDragon OtherDragon {
        get {
            if(otherDragon == null)  
                otherDragon = GameObject.Find("EnemyDragon").GetComponent<OtherDragon>();

            return otherDragon;
        }
    }

    #endregion
}
