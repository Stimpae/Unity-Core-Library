#if TIMELINE_INSTALLED
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

using UnityEngine.Timeline;

namespace Pastime.Core.Utils {
    public static class PlayableExtensions {
        public static void SampleAnimationTrack(this PlayableDirector playable, GameObject gameObject, float sampleTime, int clipIndex, string trackName) {
            var timelineAsset = playable.playableAsset as TimelineAsset;
            AnimationTrack animationTrack = null;

            if (timelineAsset != null) {
                foreach (var track in timelineAsset.GetOutputTracks()) {
                    if (track.name == trackName) {
                        if (track is AnimationTrack animTrack) {
                            animationTrack = animTrack;
                        }
                        Debug.LogWarning($"Can't find animation track with this name : {trackName}");
                    }
                    Debug.LogWarning($"Can't find track with this name : {trackName}");
                }
            }
            else {
                Debug.LogWarning($"Timeline asset is not valid");
            }

            if (animationTrack != null) {
                AnimationClip clip = animationTrack.GetClips().ElementAtOrDefault(clipIndex)?.animationClip;
                if (clip != null) clip.SampleAnimation(gameObject, sampleTime);
                else Debug.LogWarning($"Animation track clip index {clipIndex} is invalid, check Playable asset");
            }
              
        }
    }
}
#endif