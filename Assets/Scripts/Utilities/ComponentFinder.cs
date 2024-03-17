using UnityEngine;

namespace Utility{
    public static class ComponentFinder{
        // Static function to search for a component in the parent hierarchy recursively
        public static T FindComponentInParents<T>(Transform currentTransform) where T : Component{
            // Loop through parents until reaching the topmost parent
            while (currentTransform != null)
            {
                // Attempt to get the component from the current parent
                T component = currentTransform.GetComponent<T>();

                // If the component is found, return it
                if (component != null)
                {
                    return component;
                }

                // Move up to the next parent
                currentTransform = currentTransform.parent;
            }

            // Return null if the component is not found in any parent
            return null;
        }
    }

}