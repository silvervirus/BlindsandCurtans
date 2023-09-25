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

namespace BlindsandCurtains.Gen
{
    public class Hgen
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildableCurtains.Register();
        }
    }

    public static class BuildableCurtains
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Hgen", "Heavy Generator", "Add a Generator to your base!", "English")
            .WithIcon(CandB.GetSprite("OpenC"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate curtainsClone = new CloneTemplate(Info, "cf6c02c3-195e-4d26-aec5-4b0ac0776ce2");

            curtainsClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;

                // Find the main model of the curtains
                GameObject model = obj.transform.Find("heavy_generator").gameObject;

                // Add BoxCollider to the main model to allow placement detection

                
                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
                PrefabUtils.AddStorageContainer(obj, "Heavry Generator", "Hgen", 1, 1, true);
            };

            prefab.SetUnlock(TechType.Builder);
            prefab.SetGameObject(curtainsClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2)));

            prefab.Register();

            Hgen.techType = Info.TechType;
        }
    }
}
