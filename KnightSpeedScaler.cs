using UnityEngine;
using HKMirror;
using HKMirror.Hooks.OnHooks;
using HKMirror.Hooks.ILHooks;
using MonoMod.Cil;
using UnityEngine.Windows.Speech;
using System;
using Satchel;
using Satchel.Futils;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace FastWorld
{

    public class KnightSpeedScaler : MonoBehaviour
    {
        private HeroController hc = HeroController.instance;
        private PlayMakerFSM spellFsm;
        private PlayMakerFSM shadowRechargeFsm;
        private Rigidbody2D rb;

        private Vector2 previousPos;

        void Start()
        {
            rb = hc.gameObject.GetComponent<Rigidbody2D>();

            spellFsm = hc.gameObject.LocateMyFSM("Spell Control");

            shadowRechargeFsm = hc.shadowRechargePrefab.gameObject.LocateMyFSM("Recharge Effect");
            shadowRechargeFsm.GetVariable<FsmFloat>("Shadow Recharge Time").Value = 1.5f / FastWorld.KnightSlowness;
            shadowRechargeFsm.GetAction<FloatSubtract>("Init", 2).subtract = 0.85f / FastWorld.KnightSlowness;

            ChangeScreamSpeed();
            ChangeQuakeSpeed();
            ChangeHealingSpeed();

            hc.RUN_SPEED *= FastWorld.KnightSlowness;
            hc.RUN_SPEED_CH *= FastWorld.KnightSlowness;
            hc.RUN_SPEED_CH_COMBO *= FastWorld.KnightSlowness;
            hc.WALK_SPEED *= FastWorld.KnightSlowness;
            hc.UNDERWATER_SPEED *= FastWorld.KnightSlowness;
            hc.JUMP_SPEED *= FastWorld.KnightSlowness;
            hc.JUMP_SPEED_UNDERWATER *= FastWorld.KnightSlowness;
            hc.MIN_JUMP_SPEED *= FastWorld.KnightSlowness;
            hc.JUMP_STEPS = Mathf.RoundToInt(hc.JUMP_STEPS / FastWorld.KnightSlowness) - 1;
            hc.JUMP_STEPS_MIN = Mathf.RoundToInt(hc.JUMP_STEPS_MIN / FastWorld.KnightSlowness) - 1;
            hc.WJ_KICKOFF_SPEED *= FastWorld.KnightSlowness;
            hc.WJLOCK_STEPS_LONG = Mathf.RoundToInt(hc.WJLOCK_STEPS_LONG / FastWorld.KnightSlowness);
            hc.WJLOCK_STEPS_SHORT = Mathf.RoundToInt(hc.WJLOCK_STEPS_SHORT / FastWorld.KnightSlowness);
            hc.WALL_STICKY_STEPS = Mathf.RoundToInt(hc.WALL_STICKY_STEPS / FastWorld.KnightSlowness);
            hc.DASH_SPEED *= FastWorld.KnightSlowness;
            hc.DASH_SPEED_SHARP *= FastWorld.KnightSlowness;
            hc.DASH_TIME /= FastWorld.KnightSlowness;
            hc.SHADOW_DASH_SPEED *= FastWorld.KnightSlowness;
            hc.SHADOW_DASH_TIME /= FastWorld.KnightSlowness;
            hc.SHADOW_DASH_COOLDOWN /= FastWorld.KnightSlowness;
            hc.SUPER_DASH_SPEED *= FastWorld.KnightSlowness;
            hc.DASH_COOLDOWN /= FastWorld.KnightSlowness;
            hc.DASH_COOLDOWN_CH /= FastWorld.KnightSlowness;
            hc.WALLSLIDE_SPEED *= FastWorld.KnightSlowness;
            hc.WALLSLIDE_DECEL *= FastWorld.KnightSlowness;
            hc.NAIL_CHARGE_TIME_DEFAULT /= FastWorld.KnightSlowness;
            hc.NAIL_CHARGE_TIME_CHARM /= FastWorld.KnightSlowness;
            hc.CYCLONE_HORIZONTAL_SPEED *= FastWorld.KnightSlowness;
            hc.SWIM_ACCEL *= FastWorld.KnightSlowness;
            hc.SWIM_MAX_SPEED *= FastWorld.KnightSlowness;
            hc.ATTACK_DURATION /= FastWorld.KnightSlowness;
            hc.ATTACK_DURATION_CH /= FastWorld.KnightSlowness;
            hc.ALT_ATTACK_RESET /= FastWorld.KnightSlowness;
            hc.ATTACK_RECOVERY_TIME /= FastWorld.KnightSlowness;
            hc.ATTACK_COOLDOWN_TIME /= FastWorld.KnightSlowness;
            hc.ATTACK_COOLDOWN_TIME_CH /= FastWorld.KnightSlowness;
            hc.BOUNCE_TIME /= FastWorld.KnightSlowness;
            hc.BOUNCE_VELOCITY *= FastWorld.KnightSlowness;
            hc.SHROOM_BOUNCE_VELOCITY *= FastWorld.KnightSlowness;
            hc.RECOIL_HOR_TIME /= FastWorld.KnightSlowness;
            hc.RECOIL_HOR_VELOCITY *= FastWorld.KnightSlowness;
            hc.RECOIL_HOR_VELOCITY_LONG *= FastWorld.KnightSlowness;
            hc.RECOIL_HOR_STEPS *= FastWorld.KnightSlowness;
            hc.RECOIL_DOWN_VELOCITY *= FastWorld.KnightSlowness;
            hc.RUN_PUFF_TIME /= FastWorld.KnightSlowness;
            hc.BIG_FALL_TIME /= FastWorld.KnightSlowness;
            hc.HARD_LANDING_TIME /= FastWorld.KnightSlowness;
            hc.DOWN_DASH_TIME /= FastWorld.KnightSlowness;
            hc.MAX_FALL_VELOCITY *= FastWorld.KnightSlowness;
            hc.MAX_FALL_VELOCITY_UNDERWATER *= FastWorld.KnightSlowness;
            hc.RECOIL_DURATION /= FastWorld.KnightSlowness;
            hc.RECOIL_DURATION_STAL /= FastWorld.KnightSlowness;
            hc.RECOIL_VELOCITY *= FastWorld.KnightSlowness;
            hc.DAMAGE_FREEZE_DOWN /= FastWorld.KnightSlowness;
            hc.DAMAGE_FREEZE_WAIT /= FastWorld.KnightSlowness;
            hc.DAMAGE_FREEZE_UP /= FastWorld.KnightSlowness;
            hc.INVUL_TIME /= FastWorld.KnightSlowness;
            hc.INVUL_TIME_STAL /= FastWorld.KnightSlowness;
            hc.INVUL_TIME_PARRY /= FastWorld.KnightSlowness;
            hc.INVUL_TIME_QUAKE /= FastWorld.KnightSlowness;
            hc.INVUL_TIME_CYCLONE /= FastWorld.KnightSlowness;
            hc.CAST_TIME /= FastWorld.KnightSlowness;
            hc.CAST_RECOIL_TIME /= FastWorld.KnightSlowness;
            hc.CAST_RECOIL_VELOCITY *= FastWorld.KnightSlowness;
            hc.WALLSLIDE_CLIP_DELAY /= FastWorld.KnightSlowness;

            spellFsm.AddCustomAction("Can Cast?", () => {
                ChangeFpsForAllSpellObjects();
            });
        }

        void Update()
        {
            ChangeFireballSpeed("Fireball Top");
            ChangeFireballSpeed("Fireball2 Top");

            if (rb.gravityScale != (float)(0.79 * (FastWorld.KnightSlowness * FastWorld.KnightSlowness)) && rb.gravityScale != 0)
            {
                rb.gravityScale = (float)(0.79 * (FastWorld.KnightSlowness * FastWorld.KnightSlowness));
                hc.DEFAULT_GRAVITY = (float)(0.79 * (FastWorld.KnightSlowness * FastWorld.KnightSlowness));
            }
        }

        private void ChangeFpsForAllSpellObjects()
        {
            var spellObj = GameObject.Find("Spells");
            foreach (Transform t in spellObj.GetComponentsInChildren<Transform>())
            {
                var childObj = t.gameObject;
                ChangeTk2dAnimationFps(childObj);
            }
        }

        private void ChangeTk2dAnimationFps(GameObject go, float defaultValue = -1)
        {
            var animator = go.GetComponent<tk2dSpriteAnimator>();
            if (animator != null)
            {
                foreach (var clip in animator.Library.clips)
                {
                    if (defaultValue != -1)
                        clip.fps = defaultValue * FastWorld.KnightSlowness;
                    else
                        clip.fps = clip.fps * FastWorld.KnightSlowness;
                }
            }
        }

        private void ChangeScreamSpeed()
        {
            spellFsm.GetAction<Wait>("Scream Burst 1", 10).time = 0.3f / FastWorld.KnightSlowness;
            spellFsm.GetAction<Wait>("End Roar", 0).time = 0.15f / FastWorld.KnightSlowness;

            spellFsm.GetAction<Wait>("Scream Burst 2", 11).time = 0.3f / FastWorld.KnightSlowness;
            spellFsm.GetAction<Wait>("End Roar 2", 0).time = 0.15f / FastWorld.KnightSlowness;

            spellFsm.GetAction<Wait>("Scream Burst 3", 10).time = 0.5f / FastWorld.KnightSlowness;
        }

        private void ChangeQuakeSpeed()
        {
            spellFsm.GetAction<SetFloatValue>("Q On Ground", 0).floatValue = 11 * FastWorld.KnightSlowness;

            spellFsm.GetAction<SetVelocity2d>("Quake1 Down", 6).y = -50f * FastWorld.KnightSlowness;
            spellFsm.GetAction<SetVelocity2d>("Quake2 Down", 6).y = -50f * FastWorld.KnightSlowness;

            spellFsm.GetAction<Wait>("Quake1 Land", 15).time = 0.75f / FastWorld.KnightSlowness;
            spellFsm.GetAction<Wait>("Q2 Land", 14).time = 0.75f / FastWorld.KnightSlowness;
        }

        private void ChangeHealingSpeed()
        {
            ChangeSpeedValueOfSpell("Time Per MP Drain", 1 / FastWorld.KnightSlowness);
            ChangeSpeedValueOfSpell("Time Per MP Drain CH", 1 / FastWorld.KnightSlowness);
            ChangeSpeedValueOfSpell("Time Per MP Drain UnCH", 1 / FastWorld.KnightSlowness);
            ChangeSpeedValueOfSpell("Focus Start Time", 1 / FastWorld.KnightSlowness);
            ChangeSpeedValueOfSpell("Grace Time", 1 / FastWorld.KnightSlowness);

            ChangeSpeedValueOfSpell("Slug Speed R", FastWorld.KnightSlowness);
        }

        private void ChangeFireballSpeed(string fireballName)
        {
            var fireballGO = GameObject.Find(fireballName);
            if (fireballGO != null)
            {
                var fireballFsm = fireballGO.LocateMyFSM("Fireball Cast");
                fireballFsm.GetVariable<FsmFloat>("Fire Speed").Value = fireballFsm.GetVariable<FsmFloat>("Fire Speed").Value * FastWorld.KnightSlowness;
            }
        }

        private void ChangeSpeedValueOfSpell(string varName, float scale)
        {
            spellFsm.GetVariable<FsmFloat>(varName).Value = spellFsm.GetVariable<FsmFloat>(varName).Value * scale;
        }
    }
}