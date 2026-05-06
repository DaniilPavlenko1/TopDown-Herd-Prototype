using System;
using UnityEngine;
using UnityPresentation.Bootstrap;

namespace UnityPresentation.Diagnostics
{
    public static class SceneReferenceValidator
    {
        public static void Validate(SceneReferences references)
        {
            Debug.Assert(references != null, "SceneReferences is not assigned.");

            if (references == null)
                return;

            Debug.Assert(references.MainCamera != null, "Main Camera is not assigned.");
            Debug.Assert(references.CameraView != null, "CameraView is not assigned.");
            Debug.Assert(references.GroundView != null, "GroundView is not assigned.");
            Debug.Assert(references.YardView != null, "YardView is not assigned.");
            Debug.Assert(references.SpawnAreaView != null, "SpawnAreaView is not assigned.");
            Debug.Assert(references.HeroView != null, "HeroView is not assigned.");
            Debug.Assert(references.AnimalPrefab != null, "Animal prefab is not assigned.");
            Debug.Assert(references.AnimalsContainer != null, "Animals container is not assigned.");
            Debug.Assert(references.ScoreView != null, "ScoreView is not assigned.");
        }

        public static void ThrowIfInvalid(SceneReferences references)
        {
            Validate(references);

            if (references == null)
                throw new InvalidOperationException("SceneReferences is not assigned.");

            if (references.MainCamera == null)
                throw new InvalidOperationException("Main Camera is not assigned.");

            if (references.CameraView == null)
                throw new InvalidOperationException("CameraView is not assigned.");

            if (references.GroundView == null)
                throw new InvalidOperationException("GroundView is not assigned.");

            if (references.YardView == null)
                throw new InvalidOperationException("YardView is not assigned.");

            if (references.SpawnAreaView == null)
                throw new InvalidOperationException("SpawnAreaView is not assigned.");

            if (references.HeroView == null)
                throw new InvalidOperationException("HeroView is not assigned.");

            if (references.AnimalPrefab == null)
                throw new InvalidOperationException("Animal prefab is not assigned.");

            if (references.AnimalsContainer == null)
                throw new InvalidOperationException("Animals container is not assigned.");

            if (references.ScoreView == null)
                throw new InvalidOperationException("ScoreView is not assigned.");
        }
    }
}
