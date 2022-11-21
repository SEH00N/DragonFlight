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
        handlers[(int)InteractEvents.Spawn] = SpawnEvent;
        handlers[(int)InteractEvents.BoolAnim] = BoolAnimEvent;
        handlers[(int)InteractEvents.TriggerAnim] = TriggerAnimEvent;
        handlers[(int)InteractEvents.Ride] = RideEvent;
        handlers[(int)InteractEvents.Fire] = FireEvent;
    }

    private void FireEvent(Packet packet)
    {
        if(!DEFINE.Ready2Start)
            return;

        OtherPlayer.DoFireEffect();
    }

    private void TriggerAnimEvent(Packet packet)
    {
        if(!DEFINE.Ready2Start)
            return;

        TriggerAnimPacket tirggetAnimPacket = JsonConvert.DeserializeObject<TriggerAnimPacket>(packet.value);
        Animator anim = (packet.value == "Player") ? OtherPlayer.animator : OtherDragon.animator;
        anim.SetTrigger(tirggetAnimPacket.value);
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
        if(!DEFINE.Ready2Start)
            return;

        BoolAnimPacket boolAnimPacket = JsonConvert.DeserializeObject<BoolAnimPacket>(packet.value);
        Animator anim = (boolAnimPacket.target == "Player") ? OtherPlayer.animator : OtherDragon.animator;
        anim.SetBool(boolAnimPacket.key, boolAnimPacket.value);
    }

    private void SpawnEvent(Packet packet)
    {
        if(!DEFINE.Ready2Start)
            return;

        SpawnPacket spawnPacket = JsonConvert.DeserializeObject<SpawnPacket>(packet.value);
        PoolableMono obj = PoolManager.Instance.Pop(spawnPacket.name);
        obj.transform.position = spawnPacket.position;
        obj.transform.localRotation = Quaternion.Euler(spawnPacket.rotation);
    }

    private void PlayerMoveEvent(Packet packet)
    {
        if(!DEFINE.Ready2Start)
            return;

        MovePacket movePacket = JsonConvert.DeserializeObject<MovePacket>(packet.value);
        OtherPlayer.DoMove(movePacket.position, movePacket.rotation, movePacket.animationValue);
    }

    private void DragonMoveEvent(Packet packet)
    {
        if(!DEFINE.Ready2Start)
            return;

        MovePacket movePacket = JsonConvert.DeserializeObject<MovePacket>(packet.value);
        OtherDragon.DoMove(movePacket.position, movePacket.rotation, movePacket.animationValue);
    }

    private void DamageEvent(Packet packet)
    {
        if(!DEFINE.Ready2Start)
            return;

        DamagePacket damagePacket = JsonConvert.DeserializeObject<DamagePacket>(packet.value);

        IDamageable id = (damagePacket.target == "Player" ? Player.PlayerHealth : Dragon.DragonHealth);
        id.OnDamage(damagePacket.damage);
    }
}
