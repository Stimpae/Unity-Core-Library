using System;
using System.Collections.Generic;
using UnityEngine;

namespace Thundertale.Core.RuntimeSet {
    /// <summary>
/// A generic runtime set that manages a collection of items of type T.
/// </summary>
/// <typeparam name="T">The type of items managed by the runtime set.</typeparam>
public abstract class RuntimeSet<T> : ScriptableObject {
    /// <summary>
    /// A struct representing an item in the runtime set.
    /// </summary>
    [Serializable]
    private struct SetItem {
        /// <summary>
        /// The unique identifier for the item.
        /// </summary>
        public string id;
        /// <summary>
        /// The item of type T.
        /// </summary>
        public T item;
    }

    /// <summary>
    /// The list of items in the runtime set.
    /// </summary>
    [SerializeField] private List<SetItem> items = new();

    /// <summary>
    /// Adds an item to the runtime set.
    /// </summary>
    /// <param name="id">The unique identifier for the item.</param>
    /// <param name="setToAdd">The item to add to the set.</param>
    public void Add(string id, T setToAdd) {
        if (items.Exists(x => x.id == id)) {
            Debug.LogWarning($"Set already contains an item with id {id}");
            return;
        }
        items.Add(new SetItem { id = id, item = setToAdd });
    }

    /// <summary>
    /// Removes an item from the runtime set.
    /// </summary>
    /// <param name="id">The unique identifier for the item.</param>
    /// <param name="setToRemove">The item to remove from the set.</param>
    public void Remove(string id, T setToRemove) {
        var item = items.Find(x => x.id == id);
        if (item.item.Equals(setToRemove)) {
            items.Remove(item);
        }
    }

    /// <summary>
    /// Gets the first item of type T1 from the runtime set.
    /// </summary>
    /// <typeparam name="T1">The type of item to get.</typeparam>
    /// <returns>The first item of type T1, or the default value if no such item exists.</returns>
    public T1 GetData<T1>() {
        foreach (var item in items) {
            if (item.item is T1 t) {
                return t;
            }
        }
        return default;
    }

    /// <summary>
    /// Gets an item by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the item.</param>
    /// <returns>The item with the specified identifier, or the default value if no such item exists.</returns>
    public T1 GetData<T1>(string id) {
        foreach (var item in items) {
            if (item.id == id) {
                if (item.item is T1 t) {
                    return t;
                }
            }
        }
        return default;
    }
}
}
