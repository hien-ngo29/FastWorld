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
        private float speedScale = (float)(1 / 1.5f);
        private HeroController hc = HeroController.instance;
        private PlayMakerFSM spellFsm;
        private Rigidbody2D rb;

        private Vector2 previousPos;

        public tk2dSpriteAnimator animator;

        void Start()
        {
            rb = hc.gameObject.GetComponent<Rigidbody2D>();

            animator = hc.gameObject.GetComponent<tk2dSpriteAnimator>();
            foreach (var clip in animator.Library.clips)
            {
                clip.fps *= speedScale;   // Halve the FPS
            }

            spellFsm = hc.gameObject.LocateMyFSM("Spell Control");

            ChangeQuakeSpeed();
            ChangeHealingSpeed();

            // hc.UNDERWATER_GRAVITY *= 0.5f;

            hc.RUN_SPEED *= speedScale;
            hc.RUN_SPEED_CH *= speedScale;
            hc.RUN_SPEED_CH_COMBO *= speedScale;
            hc.WALK_SPEED *= speedScale;
            hc.UNDERWATER_SPEED *= speedScale;
            hc.JUMP_SPEED *= speedScale;
            hc.JUMP_SPEED_UNDERWATER *= speedScale;
            hc.MIN_JUMP_SPEED *= speedScale;
            hc.JUMP_STEPS = Mathf.RoundToInt(hc.JUMP_STEPS / speedScale) - 1;
            hc.JUMP_STEPS_MIN = Mathf.RoundToInt(hc.JUMP_STEPS_MIN / speedScale) - 1;
            hc.WJ_KICKOFF_SPEED *= speedScale;
            hc.DASH_SPEED *= speedScale;
            hc.DASH_SPEED_SHARP *= speedScale;
            hc.DASH_TIME /= speedScale;
            hc.SHADOW_DASH_SPEED *= speedScale;
            hc.SHADOW_DASH_TIME /= speedScale;
            hc.SHADOW_DASH_COOLDOWN /= speedScale;
            hc.SUPER_DASH_SPEED *= speedScale;
            hc.DASH_COOLDOWN /= speedScale;
            hc.DASH_COOLDOWN_CH /= speedScale;
            hc.WALLSLIDE_SPEED *= speedScale;
            hc.WALLSLIDE_DECEL *= speedScale;
            hc.NAIL_CHARGE_TIME_DEFAULT /= speedScale;
            hc.NAIL_CHARGE_TIME_CHARM /= speedScale;
            hc.CYCLONE_HORIZONTAL_SPEED *= speedScale;
            hc.SWIM_ACCEL *= speedScale;
            hc.SWIM_MAX_SPEED *= speedScale;
            hc.ATTACK_DURATION /= speedScale;
            hc.ATTACK_DURATION_CH /= speedScale;
            hc.ALT_ATTACK_RESET /= speedScale;
            hc.ATTACK_RECOVERY_TIME /= speedScale;
            hc.ATTACK_COOLDOWN_TIME /= speedScale;
            hc.ATTACK_COOLDOWN_TIME_CH /= speedScale;
            hc.BOUNCE_TIME /= speedScale;
            hc.BOUNCE_VELOCITY *= speedScale;
            hc.SHROOM_BOUNCE_VELOCITY *= speedScale;
            hc.RECOIL_HOR_TIME /= speedScale;
            hc.RECOIL_HOR_VELOCITY *= speedScale;
            hc.RECOIL_HOR_VELOCITY_LONG *= speedScale;
            hc.RECOIL_HOR_STEPS *= speedScale;
            hc.RECOIL_DOWN_VELOCITY *= speedScale;
            hc.RUN_PUFF_TIME /= speedScale;
            hc.BIG_FALL_TIME /= speedScale;
            hc.HARD_LANDING_TIME /= speedScale;
            hc.DOWN_DASH_TIME /= speedScale;
            hc.MAX_FALL_VELOCITY *= speedScale;
            hc.MAX_FALL_VELOCITY_UNDERWATER *= speedScale;
            hc.RECOIL_DURATION /= speedScale;
            hc.RECOIL_DURATION_STAL /= speedScale;
            hc.RECOIL_VELOCITY *= speedScale;
            hc.DAMAGE_FREEZE_DOWN /= speedScale;
            hc.DAMAGE_FREEZE_WAIT /= speedScale;
            hc.DAMAGE_FREEZE_UP /= speedScale;
            hc.INVUL_TIME /= speedScale;
            hc.INVUL_TIME_STAL /= speedScale;
            hc.INVUL_TIME_PARRY /= speedScale;
            hc.INVUL_TIME_QUAKE /= speedScale;
            hc.INVUL_TIME_CYCLONE /= speedScale;
            hc.CAST_TIME /= speedScale;
            hc.CAST_RECOIL_TIME /= speedScale;
            hc.CAST_RECOIL_VELOCITY *= speedScale;
            hc.WALLSLIDE_CLIP_DELAY /= speedScale;
        }

        void Update()
        {
            ChangeFireballSpeed("Fireball Top");
            ChangeFireballSpeed("Fireball2 Top");

            if (rb.gravityScale != (float)(0.79*(speedScale*speedScale)) && rb.gravityScale != 0)
            {
                rb.gravityScale = (float)(0.79*(speedScale*speedScale));
                hc.DEFAULT_GRAVITY =  (float)(0.79*(speedScale*speedScale));
            }
        }

        private void ChangeQuakeSpeed()
        {
            spellFsm.GetAction<SetFloatValue>("Q On Ground", 0).floatValue = 11 * speedScale;
            
            spellFsm.GetAction<SetVelocity2d>("Quake1 Down", 6).y = -50f * speedScale;
            spellFsm.GetAction<SetVelocity2d>("Quake2 Down", 6).y = -50f * speedScale;

            spellFsm.GetAction<Wait>("Quake1 Land", 15).time = 0.75f / speedScale;
            spellFsm.GetAction<Wait>("Q2 Land", 14).time = 0.75f / speedScale;
        }

        private void ChangeHealingSpeed()
        {
            ChangeSpeedValueOfSpell("Time Per MP Drain", 1 / speedScale);
            ChangeSpeedValueOfSpell("Time Per MP Drain CH", 1 / speedScale);
            ChangeSpeedValueOfSpell("Time Per MP Drain UnCH", 1 / speedScale);
            ChangeSpeedValueOfSpell("Focus Start Time", 1 / speedScale);
            ChangeSpeedValueOfSpell("Grace Time", 1 / speedScale);

            ChangeSpeedValueOfSpell("Slug Speed R", speedScale);
        }

        private void ChangeFireballSpeed(string fireballName)
        {
            var fireballGO = GameObject.Find(fireballName);
            if (fireballGO != null)
            {
                var fireballFsm = fireballGO.LocateMyFSM("Fireball Cast");
                fireballFsm.GetVariable<FsmFloat>("Fire Speed").Value = fireballFsm.GetVariable<FsmFloat>("Fire Speed").Value * speedScale;
            }
        }

        private void ChangeSpeedValueOfSpell(string varName, float scale)
        {
            spellFsm.GetVariable<FsmFloat>(varName).Value = spellFsm.GetVariable<FsmFloat>(varName).Value * scale;
        }
    }
}