using UnityEngine;

namespace Pastime.Core.Utils {
    /// <summary>
    ///  Extension methods for layers.
    /// </summary>
    public class LayerExtensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public static bool IsInLayerMask(int layer, LayerMask layerMask) {
            return (layerMask & (1 << layer)) != 0;
        }
    }
}