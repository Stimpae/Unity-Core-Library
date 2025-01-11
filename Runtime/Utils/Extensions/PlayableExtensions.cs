#if TIMELINE_INSTALLED
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pastime.Core.Utils {
    /// <summary>
    /// Provides extension methods for sampling animation tracks in a PlayableDirector.
    /// </summary>
    public static class PlayableExtensions {
        /// <summary>
        /// Samples an animation track at a specific time.
        /// </summary>
        /// <param name="playable">The PlayableDirector controlling the timeline.</param>
        /// <param name="animator">The Animator component to which the animation is applied.</param>
        /// <param name="sampleTime">The time at which to sample the animation.</param>
        /// <param name="clipIndex">The index of the animation clip within the track.</param>
        /// <param name="trackName">The name of the animation track.</param>
        public static void SampleAnimationTrack(PlayableDirector playable, Animator animator, float sampleTime, int clipIndex, string trackName) {
            var timelineAsset = playable.playableAsset as TimelineAsset;
            if (timelineAsset == null) {
                Debug.LogWarning("Timeline asset is not valid");
                return;
            }
        
            var animationTrack = GetAnimationTrack(timelineAsset, trackName);
            if (animationTrack == null) {
                Debug.LogWarning($"Can't find animation track with this name: {trackName}");
                return;
            }
        
            playable.SetGenericBinding(animationTrack, animator);
            SampleAnimationClip(animationTrack, animator, sampleTime, clipIndex);
        }
        
        /// <summary>
        /// Retrieves an animation track from a timeline asset by name.
        /// </summary>
        /// <param name="timelineAsset">The timeline asset containing the tracks.</param>
        /// <param name="trackName">The name of the animation track to retrieve.</param>
        /// <returns>The animation track if found, otherwise null.</returns>
        private static AnimationTrack GetAnimationTrack(TimelineAsset timelineAsset, string trackName) {
            foreach (var track in timelineAsset.GetOutputTracks()) {
                if (track.name == trackName && track is AnimationTrack animTrack) {
                    return animTrack;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Samples an animation clip at a specific time.
        /// </summary>
        /// <param name="animationTrack">The animation track containing the clip.</param>
        /// <param name="animator">The Animator component to which the animation is applied.</param>
        /// <param name="sampleTime">The time at which to sample the animation.</param>
        /// <param name="clipIndex">The index of the animation clip within the track.</param>
        private static void SampleAnimationClip(AnimationTrack animationTrack, Animator animator, float sampleTime, int clipIndex) {
            var clip = animationTrack.GetClips().ElementAtOrDefault(clipIndex)?.animationClip;
            if (clip != null) {
                clip.SampleAnimation(animator.gameObject, sampleTime);
            } else {
                Debug.LogWarning($"Animation track clip index {clipIndex} is invalid, check Playable asset");
            }
        }
    }
}
#endif