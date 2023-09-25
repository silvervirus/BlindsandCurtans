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
    public class subcons
    {
        public static TechType techType;
        public static void Patch()
        {
            Buildablsc.Register();
        }
    }

    public static class Buildablsc
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("sc", "Console ", "Add a console to your base!", "English")
            .WithIcon(CandB.GetSprite("OpenC"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate curtainsClone = new CloneTemplate(Info, "9e47f91e-c0d4-406d-af0a-d7f57904f196");

            curtainsClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;
                foreach (Transform child in obj.transform)
                {
                    BoxCollider collider = child.gameObject.EnsureComponent<BoxCollider>();
                    collider.center = Vector3.zero;
                    collider.size = Vector3.one;
                }
                // Find the main model of the curtains
                GameObject model = obj.transform.Find("Submarine_console_02").gameObject;

                // Add BoxCollider to the main model to allow placement detection
                BoxCollider mainModelCollider = model.EnsureComponent<BoxCollider>();
                mainModelCollider.center = new Vector3(0f, 0.18f, 0.28f);
                mainModelCollider.size = new Vector3(3.0f, 0.1f, 3.0f);

                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
                
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
