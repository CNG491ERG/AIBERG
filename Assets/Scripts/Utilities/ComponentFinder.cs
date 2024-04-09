using System.Collections.Generic;
using UnityEngine;

namespace AIBERG.Utility{
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

        public static T FindComponentInChildren<T>(Transform currentTransform) where T : Component{
            // Attempt to get the component from the current transform
            T component = currentTransform.GetComponent<T>();

            // If the component is found, return it
            if (component != null){
                return component;
            }

            // Loop through children until reaching the bottommost child
            foreach (Transform child in currentTransform){
                // Recursively search for the component in the child's hierarchy
                component = FindComponentInChildren<T>(child);

                // If the component is found in the child hierarchy, return it
                if (component != null){
                    return component;
                }
            }
            // Return null if the component is not found in any child
            return null;
        }

        public static List<GameObject> FindGameObjectsWithTagInChildren(string tag, Transform currentTransform){
            List<GameObject> foundObjects = new List<GameObject>();

            // Search for objects with the specified tag in the current transform's hierarchy
            FindGameObjectsWithTagRecursively(tag, currentTransform, foundObjects);

            return foundObjects;
        }

        private static void FindGameObjectsWithTagRecursively(string tag, Transform currentTransform, List<GameObject> foundObjects){
            // Check if the current transform's GameObject has the specified tag
            if (currentTransform.CompareTag(tag)){
                // If it does, add it to the list of found objects
                foundObjects.Add(currentTransform.gameObject);
            }
            // Search through children for objects with the specified tag
            foreach (Transform child in currentTransform){
                // Recursively search in the child's hierarchy
                FindGameObjectsWithTagRecursively(tag, child, foundObjects);
            }
        }
        public static List<GameObject> FindGameObjectsWithTagInSameLevel(string tag, Transform currentTransform){
            List<GameObject> foundObjects = new List<GameObject>();

            // Get the parent transform
            Transform parent = currentTransform.parent;
            if (parent != null)
            {
                // Search for objects with the specified tag in the parent's children
                foreach (Transform sibling in parent)
                {
                    if (sibling.CompareTag(tag))
                    {
                        foundObjects.Add(sibling.gameObject);
                    }
                }
            }

            return foundObjects;
        }
    }
}