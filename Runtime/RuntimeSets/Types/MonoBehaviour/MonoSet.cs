using UnityEngine;

namespace Pastime.Core.RuntimeSets {
    [CreateAssetMenu(menuName = "Runtime Set/Mono Set", fileName = "New Mono Set")]
    public class MonoSet : RuntimeSet<UnityEngine.MonoBehaviour> { }
}