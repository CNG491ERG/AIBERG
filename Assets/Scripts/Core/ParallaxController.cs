using System.Collections.Generic;
using UnityEngine;

namespace AIBERG.Core{
    public class ParallaxController : MonoBehaviour
    {
        public static bool parallaxStopper = true;
    
        [System.Serializable]
        public class ParallaxLayer
        {
            public Transform[] layerTransforms; // Two transforms for seamless looping
            public float parallaxFactor;
            [HideInInspector] public float spriteWidth;
        }

        public List<ParallaxLayer> parallaxLayers;
        public float backgroundSpeed = 2f;
        public float overlapAmount = 0.1f; // Amount of overlap between layers

        void Start()
        {
            // Initialize the sprite width for each layer
            foreach (ParallaxLayer layer in parallaxLayers)
            {
                SpriteRenderer spriteRenderer = layer.layerTransforms[0].GetComponent<SpriteRenderer>();
                layer.spriteWidth = spriteRenderer.bounds.size.x;
            }
        }

        void Update()
        {
            if(parallaxStopper){
                foreach (ParallaxLayer layer in parallaxLayers)
                {
                    for (int i = 0; i < layer.layerTransforms.Length; i++)
                    {
                        Transform layerTransform = layer.layerTransforms[i];
                        // Move the layer to the left
                        float newPositionX = layerTransform.position.x - backgroundSpeed * layer.parallaxFactor * Time.deltaTime;
                        layerTransform.position = new Vector3(newPositionX, layerTransform.position.y, layerTransform.position.z);

                        // Check if the layer has moved off-screen and reposition it
                        if (layerTransform.position.x <= -layer.spriteWidth)
                        {
                            // Move it to the right of the other instance with a bit of overlap
                            Transform otherTransform = layer.layerTransforms[(i + 1) % layer.layerTransforms.Length];
                            layerTransform.position = new Vector3(otherTransform.position.x + layer.spriteWidth - overlapAmount, layerTransform.position.y, layerTransform.position.z);
                        }
                    }
                } 
            }
            
        }
    }

}
