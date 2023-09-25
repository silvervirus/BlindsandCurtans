using BlindsandCurtains.Books;
using BlindsandCurtans.Main;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UWE;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace BlindsandCurtains.Curtains
{
    public class Curtains
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildableCurtains.Register();
        }
    }

    public static class BuildableCurtains
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("WindowCurtains", "Window Curtains", "Add beautiful curtains to your base!", "English")
            .WithIcon(CandB.GetSprite("OpenC"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate curtainsClone = new CloneTemplate(Info, "6fbb1f5e-0f0c-4898-b2a7-1dfd66b86586");

            curtainsClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;

                // Find the main model of the curtains
                GameObject model = obj.transform.Find("WindowCurtains_01_open_c").gameObject;

                // Add BoxCollider to the main model to allow placement detection
                BoxCollider mainModelCollider = model.EnsureComponent<BoxCollider>();
                mainModelCollider.center = new Vector3(0f, 0f, 0.14f); // Adjust the center to the desired position
                mainModelCollider.size = new Vector3(3.0f, 0.1f, 3.0f); // Increase the height (Y) to make it easier to interact with the curtains

                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
            };

            prefab.SetUnlock(TechType.Builder);
            prefab.SetGameObject(curtainsClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2)));

            prefab.Register();

            Curtains.techType = Info.TechType;
        }
    }
}
