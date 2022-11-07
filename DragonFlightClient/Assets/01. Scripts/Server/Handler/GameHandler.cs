using System;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public class GameHandler : Handler
{
    public override int HandlersSize => (int)InteractEvents.Last;

    #region property
    private Player player = null;
    private Player Player {  
        get {
            if(player == null)
                player = DEFINE.Player;
            return player;
        }
    }
    private OtherPlayer otherPlayer = null;
    private OtherPlayer OtherPlayer {
        get {
            if(otherPlayer == null)
                otherPlayer = DEFINE.OtherPlayer;
            return otherPlayer;
        }
    }
    private Dragon dragon = null;
    private Dragon Dragon {
        get {
            if(dragon == null)
                dragon = DEFINE.Dragon;
            return dragon;
        }
    }
    private OtherDragon otherDragon = null;
    private OtherDragon OtherDragon {
        get {
            if(otherDragon == null)
                otherDragon = DEFINE.OtherDragon;
            return otherDragon;
        }
    }
    #endregion

    public override void CreateHandler()
    {
        base.CreateHandler();

        handlers[(int)InteractEvents.Damage] = DamageEvent;
        handlers[(int)InteractEvents.PlayerMove] = PlayerMoveEvent;
        handlers[(int)InteractEvents.DragonMove] = DragonMoveEvent;
    }

    private void PlayerMoveEvent(Packet packet)
    {
        MovePacket movePacket = JsonConvert.DeserializeObject<MovePacket>(packet.value);
        OtherPlayer.DoMove(movePacket.position, movePacket.rotation, movePacket.animationValue);
    }

    private void DragonMoveEvent(Packet packet)
    {
        MovePacket movePacket = JsonConvert.DeserializeObject<MovePacket>(packet.value);
        OtherDragon.DoMove(movePacket.position, movePacket.rotation, movePacket.animationValue);
    }

    private void DamageEvent(Packet packet)
    {
        DamagePacket damagePacket = JsonConvert.DeserializeObject<DamagePacket>(packet.value);

        IDamageable id = (damagePacket.target == "Player" ? Player.PlayerHealth : Dragon.DragonHealth);
        id.OnDamage(damagePacket.damage);
    }
}
