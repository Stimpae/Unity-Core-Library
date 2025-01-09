using System.Diagnostics;
using UnityEngine;

namespace Pastime.Core.EventBus {
    /// <summary>
    /// An attribute used to mark properties for subclass selection in the Unity Editor.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    public class SubclassSelectorAttribute : PropertyAttribute {
    }
}