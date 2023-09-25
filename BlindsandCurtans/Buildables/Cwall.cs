using System;
using System.Collections.Generic;
using UnityEngine;
using UWE;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace BlindsandCurtains.CreatureWall
{
    public class CreatureWall
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildableCreatureWall.Register();
        }
    }

    public static class BuildableCreatureWall
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("MLights", "Hanging Lights", "Decorate your base with a Hanging Lights!", "english", true)
            .WithIcon(Utilities.GetSprite(TechType.BaseWindow));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate creatureWallClone = new CloneTemplate(Info, "62292143-8a6c-459e-9196-cf780628ad41");

            creatureWallClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;

                // Loop through all the child objects of the creature wall and add BoxColliders to them
                foreach (Transform child in obj.transform)
                {
                    BoxCollider collider = child.gameObject.EnsureComponent<BoxCollider>();
                    collider.center = Vector3.zero;
                    collider.size = Vector3.one;
                }

                // Find the main model of the creature wall
                GameObject model = obj.transform.Find("MargGreenhouse_lnterior_01").gameObject;
                obj.transform.Find("MargGreenhouse_Interior_Lights_01_LOD0").parent = model.transform;

                // Add BoxCollider to the main model to allow placement detection
                BoxCollider mainModelCollider = model.EnsureComponent<BoxCollider>();
                mainModelCollider.center = Vector3.zero;
                mainModelCollider.size = Vector3.one;

                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
            };

            prefab.SetUnlock(TechType.Aerogel);
            prefab.SetGameObject(creatureWallClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 3), new Ingredient(TechType.Glass, 1)));

            prefab.Register();

            CreatureWall.techType = Info.TechType;
        }
    }
}
