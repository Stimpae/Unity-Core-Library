using Thundertale.Core.RuntimeSet;
using UnityEngine;

namespace Thundertale.Core.RuntimeSet {
    [CreateAssetMenu(menuName = "Runtime Set/Mono Set", fileName = "New Mono Set")]
    public class MonoSet : RuntimeSet<MonoBehaviour> { }
}