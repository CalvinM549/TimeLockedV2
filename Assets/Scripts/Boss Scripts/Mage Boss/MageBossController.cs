using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBossController : BossController
{
    public GameObject timeArtifact;
    private Health artifactHealth;

    public int fireballAttackThreshold;
    public int fireballThresholdChange;

    public int turretSpawnThreshold;
    public int turretThresholdChange;

    public int maxTurrets;

    private float lastTeleportTime;
    private float currentTeleportCooldown;
    public float minTeleportCooldown;
    public float maxTeleportCooldown;

    protected override void Start()
    {
        base.Start();
        artifactHealth = timeArtifact.GetComponent<Health>();
        maxTurrets = 0;
        currentTeleportCooldown = Random.Range(minTeleportCooldown, maxTeleportCooldown);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (health.currentHealth < turretSpawnThreshold)
        {
            maxTurrets++;
            turretSpawnThreshold -= turretThresholdChange;
        }




    }
    protected override void EnableAttack()
    {
        base.EnableAttack();


        if (attacks[2].GetComponent<MageOrbitalTurretAttack>().turrets.Count < maxTurrets)
        {
            currentAttack = attacks[2];
        }

        else if (CanTeleport(currentTeleportCooldown))
        {
            currentAttack = attacks[3];
            lastTeleportTime = Time.time;
            currentTeleportCooldown = Random.Range(minTeleportCooldown, maxTeleportCooldown);
        }

        else if (artifactHealth.currentHealth < fireballAttackThreshold && artifactHealth.currentHealth >= 0)
        {
            currentAttack = attacks[1]; // sets to fireball attack
            fireballAttackThreshold -= fireballThresholdChange;
        }

        else
        {
            currentAttack = attacks[4];
        }


        StartAttackWindup();
    }


    private bool CanTeleport(float teleportCooldown)
    {
        return Time.time >= teleportCooldown + lastTeleportTime;
    }

}

