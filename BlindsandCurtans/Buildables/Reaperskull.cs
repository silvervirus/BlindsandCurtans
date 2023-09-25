using System;
using System.Collections;
using UnityEngine;
using UWE;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace BlindsandCurtains.PunchingBag
{
    public class PunchingBag
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildablePunchingBag.Register();
        }
    }

    public static class BuildablePunchingBag
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PunchingBag", "Punching Bag", "A punching bag for training", "english", true)
            .WithIcon(Utilities.GetSprite(TechType.Titanium)); // Change the icon as needed

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);

           

            // Set the recipe for crafting the punching bag
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 3), new Ingredient(TechType.Quartz, 1)));

            // Register the prefab
            prefab.Register();

            PunchingBag.techType = Info.TechType;
        }

        public static void GetPunchingBagGameObject(Action<GameObject> onGameObjectReady)
        {
            CoroutineHost.StartCoroutine(GetPunchingBagGameObjectAsync(onGameObjectReady));
        }

        private static IEnumerator GetPunchingBagGameObjectAsync(Action<GameObject> onGameObjectReady)
        {
            var task = UWE.PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Marguerit/MargueritBase/Marg_base_punching_bag");
            yield return task;
            task.TryGetPrefab(out var prefab);

            // Create an instance of the punching bag GameObject
            GameObject instantiatedPunchingBag = GameObject.Instantiate(prefab);

            // Add BoxCollider to the main model to allow placement detection
            BoxCollider mainModelCollider = instantiatedPunchingBag.EnsureComponent<BoxCollider>();
            mainModelCollider.center = new Vector3(0f, 0.18f, 0.28f);
            mainModelCollider.size = new Vector3(1.5f, 0.36f, 0.02f);

            // Change the layer of the main model to the Default layer to avoid red placement indicators
            instantiatedPunchingBag.layer = LayerMask.NameToLayer("Default");

            // Call the callback with the instantiated punching bag GameObject or null
            onGameObjectReady?.Invoke(instantiatedPunchingBag);
        }
    }
}
