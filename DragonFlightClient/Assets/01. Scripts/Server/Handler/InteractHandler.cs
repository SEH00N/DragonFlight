using System;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public class InteractHandler : Handler
{
    public override int HandlersSize => (int)InteractEvents.Last;

    #region property
    private Player player = null;
    private Player Player {  
        get {
            if(player == null && DEFINE.Player != null)
                player = DEFINE.Player;
            return player;
        }
    }
    private OtherPlayer otherPlayer = null;
    private OtherPlayer OtherPlayer {
        get {
            if(otherPlayer == null && DEFINE.OtherPlayer != null)
                otherPlayer = DEFINE.OtherPlayer;
            return otherPlayer;
        }
    }
    private Dragon dragon = null;
    private Dragon Dragon {
        get {
            if(dragon == null && DEFINE.Dragon != null)
                dragon = DEFINE.Dragon;
            return dragon;
        }
    }
    private OtherDragon otherDragon = null;
    private OtherDragon OtherDragon {
        get {
            if(otherDragon == null && DEFINE.OtherDragon != null)
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
        handlers[(int)InteractEvents.Spawn] = SpawnEvent;
        handlers[(int)InteractEvents.BoolAnim] = BoolAnimEvent;
        handlers[(int)InteractEvents.Ride] = RideEvent;
    }

    private void RideEvent(Packet packet)
    {
        if(OtherPlayer == null)
            return;

        bool isRide = bool.Parse(packet.value);

        OtherPlayer.Riding(isRide);
    }

    private void BoolAnimEvent(Packet packet)
    {
        if(OtherDragon == null || OtherPlayer == null)
            return;

        BoolAnimPacket boolAnimPacket = JsonConvert.DeserializeObject<BoolAnimPacket>(packet.value);
        Animator anim = (boolAnimPacket.target == "Player") ? OtherPlayer.animator : OtherDragon.animator;
        anim.SetBool(boolAnimPacket.key, boolAnimPacket.value);
    }

    private void SpawnEvent(Packet packet)
    {
        SpawnPacket spawnPacket = JsonConvert.DeserializeObject<SpawnPacket>(packet.value);
        PoolableMono obj = PoolManager.Instance.Pop(spawnPacket.name);
        obj.transform.position = spawnPacket.position;
        obj.transform.localRotation = Quaternion.Euler(spawnPacket.rotation);
    }

    private void PlayerMoveEvent(Packet packet)
    {
        if(OtherPlayer == null)
            return;

        MovePacket movePacket = JsonConvert.DeserializeObject<MovePacket>(packet.value);
        OtherPlayer.DoMove(movePacket.position, movePacket.rotation, movePacket.animationValue);
    }

    private void DragonMoveEvent(Packet packet)
    {
        if(OtherDragon == null)
            return;

        MovePacket movePacket = JsonConvert.DeserializeObject<MovePacket>(packet.value);
        OtherDragon.DoMove(movePacket.position, movePacket.rotation, movePacket.animationValue);
    }

    private void DamageEvent(Packet packet)
    {
        if(Player == null || Dragon == null)
            return;

        DamagePacket damagePacket = JsonConvert.DeserializeObject<DamagePacket>(packet.value);

        IDamageable id = (damagePacket.target == "Player" ? Player.PlayerHealth : Dragon.DragonHealth);
        id.OnDamage(damagePacket.damage);
    }
}
