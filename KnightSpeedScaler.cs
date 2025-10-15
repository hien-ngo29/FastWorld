using UnityEngine;
using HKMirror;
using HKMirror.Hooks.OnHooks;
using HKMirror.Hooks.ILHooks;
using MonoMod.Cil;
using UnityEngine.Windows.Speech;
using System;

namespace FastWorld
{
    public class KnightSpeedScaler : MonoBehaviour
    {
        private float speedScale = 0.5f;
        private HeroController hc = HeroController.instance;
        private Rigidbody2D rb;

        private Vector2 previousPos;

        public tk2dSpriteAnimator animator;

        void Start()
        {
            rb = hc.gameObject.GetComponent<Rigidbody2D>();

            animator = hc.gameObject.GetComponent<tk2dSpriteAnimator>();
            if (animator == null || animator.Library == null)
            {
                Modding.Logger.Log("Nothing found!!!");
            }

            foreach (var clip in animator.Library.clips)
            {
                clip.fps /= 2f;   // Halve the FPS
            }

            // hc = HeroController.instance;
            // rb = gameObject.GetComponent<Rigidbody2D>();

            // OnHeroController.WithOrig.Move += (orig, self, direction) =>
            // {
            //     orig(self, direction * speedScale);
            // };

            // ILHeroController.Jump += (il) =>
            // {
            //     ILCursor cursor = new ILCursor(il).Goto(0);

            //     if (cursor.TryGotoNext
            //     (
            //         i => i.MatchLdfld<float>();
            //     ))
            // };

            rb.gravityScale = 0.1975f;
            hc.DEFAULT_GRAVITY = 0.1975f;
            // hc.UNDERWATER_GRAVITY *= 0.5f;

            hc.RUN_SPEED *= speedScale;
            hc.RUN_SPEED_CH *= speedScale;
            hc.RUN_SPEED_CH_COMBO *= speedScale;
            hc.WALK_SPEED *= speedScale;
            hc.UNDERWATER_SPEED *= speedScale;
            hc.JUMP_SPEED *= speedScale;
            hc.JUMP_SPEED_UNDERWATER *= speedScale;
            hc.MIN_JUMP_SPEED *= speedScale;
            // hc.JUMP_STEPS = (int)Math.Round(hc.JUMP_STEPS / speedScale);
            // hc.JUMP_STEPS_MIN *= 0.5f;
            // hc.JUMP_TIME *= 2;
            // hc.DOUBLE_JUMP_STEPS *= 2;
            // hc.WJLOCK_STEPS_SHORT *= 0.5f;
            // hc.WJLOCK_STEPS_LONG *= 0.5f;
            // hc.WJ_KICKOFF_SPEED *= 0.5f;
            // hc.WALL_STICKY_STEPS *= 0.5f;
            hc.DASH_SPEED *= speedScale;
            hc.DASH_SPEED_SHARP *= speedScale;
            hc.DASH_TIME /= speedScale;
            // hc.DASH_QUEUE_STEPS *= 2;
            // hc.BACK_DASH_SPEED *= 0.5f;
            // hc.BACK_DASH_TIME *= 0.5f;
            hc.SHADOW_DASH_SPEED *= speedScale;
            hc.SHADOW_DASH_TIME /= speedScale;
            hc.SHADOW_DASH_COOLDOWN /= speedScale;
            hc.SUPER_DASH_SPEED *= speedScale;
            hc.DASH_COOLDOWN /= speedScale;
            hc.DASH_COOLDOWN_CH /= speedScale;
            // hc.BACKDASH_COOLDOWN *= 0.5f;
            hc.WALLSLIDE_SPEED *= speedScale;
            hc.WALLSLIDE_DECEL *= speedScale;
            hc.NAIL_CHARGE_TIME_DEFAULT /= speedScale;
            hc.NAIL_CHARGE_TIME_CHARM /= speedScale;
            hc.CYCLONE_HORIZONTAL_SPEED *= speedScale;
            hc.SWIM_ACCEL *= speedScale;
            hc.SWIM_MAX_SPEED *= speedScale;
            // hc.TIME_TO_ENTER_SCENE_BOT *= 0.5f;
            // hc.TIME_TO_ENTER_SCENE_HOR *= 0.5f;
            // hc.SPEED_TO_ENTER_SCENE_HOR *= 0.5f;
            // hc.SPEED_TO_ENTER_SCENE_UP *= 0.5f;
            // hc.SPEED_TO_ENTER_SCENE_DOWN *= 0.5f;
            // hc.DEFAULT_GRAVITY *= 0.5f;
            // hc.UNDERWATER_GRAVITY *= 0.5f;
            hc.ATTACK_DURATION /= speedScale;
            hc.ATTACK_DURATION_CH /= speedScale;
            hc.ALT_ATTACK_RESET /= speedScale;
            hc.ATTACK_RECOVERY_TIME /= speedScale;
            hc.ATTACK_COOLDOWN_TIME /= speedScale;
            hc.ATTACK_COOLDOWN_TIME_CH /= speedScale;
            hc.BOUNCE_TIME /= speedScale;
            // hc.BOUNCE_SHROOM_TIME *= 0.5f;
            hc.BOUNCE_VELOCITY *= speedScale;
            // hc.SHROOM_BOUNCE_VELOCITY *= 0.5f;
            hc.RECOIL_HOR_TIME /= speedScale;
            hc.RECOIL_HOR_VELOCITY *= speedScale;
            hc.RECOIL_HOR_VELOCITY_LONG *= speedScale;
            // hc.RECOIL_HOR_STEPS = 0.5f;
            hc.RECOIL_DOWN_VELOCITY *= speedScale;
            hc.RUN_PUFF_TIME /= speedScale;
            hc.BIG_FALL_TIME /= speedScale;
            hc.HARD_LANDING_TIME /= speedScale;
            hc.DOWN_DASH_TIME /= speedScale;
            hc.MAX_FALL_VELOCITY *= speedScale;
            // hc.MAX_FALL_VELOCITY_UNDERWATER *= 0.5f;
            hc.RECOIL_DURATION /= speedScale;
            hc.RECOIL_DURATION_STAL /= speedScale;
            hc.RECOIL_VELOCITY *= speedScale;
            // hc.DAMAGE_FREEZE_DOWN /= speedScale;
            // hc.DAMAGE_FREEZE_WAIT *= 0.5f;
            // hc.DAMAGE_FREEZE_UP *= 0.5f;
            hc.INVUL_TIME /= speedScale;
            // hc.INVUL_TIME_STAL *= 0.5f;
            // hc.INVUL_TIME_PARRY *= 0.5f;
            // hc.INVUL_TIME_QUAKE *= 0.5f;
            // hc.INVUL_TIME_CYCLONE *= 0.5f;
            hc.CAST_TIME /= speedScale;
            hc.CAST_RECOIL_TIME /= speedScale;
            hc.CAST_RECOIL_VELOCITY /= speedScale;
            hc.WALLSLIDE_CLIP_DELAY /= speedScale;

        }

        void LateUpdate()
        {
            // hc.JUMP_STEPS = 20;
            // hc.JUMP_SPEED = speedScale;
            // Vector2 pos2D = transform.position;
            // pos2D -= (pos2D - previousPos) * speedScale;
            // transform.position = pos2D;

            // previousPos = transform.position;

            // rb.velocity *= speedScale;
            //     if (speedScale == 1f) return;

            //     if (rb != null && !rb.isKinematic)
            //     {
            //         rb.velocity *= speedScale;
            //     }
            //     else
            //     {
            //         // Translate manually controlled motion
            //         Vector2 current = transform.position;
            //         Vector2 delta = current - lastPosition;

            //         transform.position = lastPosition + delta * speedScale;
            //     }

            //     lastPosition = transform.position;
            // }
        }
    }
}