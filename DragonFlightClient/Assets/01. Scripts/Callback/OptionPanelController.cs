using UnityEngine;

public class OptionPanelController : ToggleInputCallback
{
    private GameObject blockPanel = null;
    
    private void Awake()
    {
        blockPanel = transform.parent.Find("BlockPanel").gameObject;
    }

    protected override void Update()
    {
        if(DEFINE.GameOver || blockPanel.activeSelf)
            return;

        base.Update();

        DEFINE.Player.PlayerMovement.rotationable = OnA;
        DEFINE.Dragon.DragonMovement.rotationable = OnA; 
    }
}
