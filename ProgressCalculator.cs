using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace DatabankCompletionLegacy
{
    public static class ProgressCalculator
    {
        public static double CalculateProgress(CraftNode node)
        {
            if (node == null)
            {
                return 0.0f;
            }
            double progress = 0.0;
            TreeNode root = node.root;
            if(node.string0.StartsWith("EncyPath"))
            {
                //It's a category!
                switch (node.id)
                {
                    case "Advanced":
                        progress = node.childCount / 15.0;
                        break;
                    case "Tech":
                        progress = ((CalculateProgress(root.FindNodeById("Equipment") as CraftNode)*13)+(CalculateProgress(root.FindNodeById("Habitats") as CraftNode)*15)+(CalculateProgress(root.FindNodeById("Power") as CraftNode)*3)+(CalculateProgress(root.FindNodeById("Vehicles") as CraftNode)*7))/38.0;
                        break;
                    case "Equipment":
                        progress = node.childCount / 13.0;
                        break;
                    case "Habitats":
                        progress = node.childCount / 15.0;
                        break;
                    case "Power":
                        progress= node.childCount / 3.0;
                        break;
                    case "Vehicles":
                        progress = node.childCount / 7.0;
                        break;
                    case "DownloadedData":
                        progress = ((CalculateProgress(root.FindNodeById("Precursor") as CraftNode)*47)+(CalculateProgress(root.FindNodeById("AuroraSurvivors") as CraftNode)*17)+(CalculateProgress(root.FindNodeById("Codes") as CraftNode)*9)+(CalculateProgress(root.FindNodeById("Degasi") as CraftNode)*21)+(CalculateProgress(root.FindNodeById("BeforeCrash") as CraftNode)*4)+(CalculateProgress(root.FindNodeById("PublicDocs") as CraftNode)*12))/110.0;
                        break;
                    case "Precursor":
                        progress = ((CalculateProgress(root.FindNodeById("Equipment") as CraftNode)*12)+(CalculateProgress(root.FindNodeById("Artifacts") as CraftNode)*21)+(CalculateProgress(root.FindNodeById("Scan") as CraftNode)*14)+(CalculateProgress(root.FindNodeById("Terminal") as CraftNode)*7))/47.0;
                        break;  
                    case "Artifacts":
                        progress = node.childCount / 12.0;
                        break;
                    case "Scan":
                        progress = node.childCount / 21.0;
                        break;
                    case "Terminal":
                        progress = node.childCount / 14.0;
                        break;
                    case "AuroraSurvivors":
                        progress = node.childCount / 17.0;
                        break;
                    case "Codes":
                        progress = node.childCount / 9.0;
                        break;
                    case "Degasi":
                        progress=((CalculateProgress(root.FindNodeById("Orders") as CraftNode)*4)+node.childCount-1)/21.0;
                        break;
                    case "Orders":
                        progress = node.childCount / 4.0;
                        break;
                    case "BeforeCrash":
                        progress = node.childCount / 4.0;
                        break;
                    case "PublicDocs":
                        progress = node.childCount / 12.0;
                        break;
                    case "PlanetaryGeology":
                        progress = node.childCount / 12.0;
                        break;
                    case "Lifeforms":
                        progress=((CalculateProgress(root.FindNodeById("Coral") as CraftNode)*7)+(CalculateProgress(root.FindNodeById("Fauna") as CraftNode)*58)+(CalculateProgress(root.FindNodeById("Flora") as CraftNode)*47))/112.0;
                        break;
                    case "Coral":
                        progress=node.childCount/7.0    ;
                        break;
                    case "Fauna":
                        progress=((CalculateProgress(root.FindNodeById("Carnivores") as CraftNode)*14)+(CalculateProgress(root.FindNodeById("Deceased") as CraftNode)*7)+(CalculateProgress(root.FindNodeById("LargeHerbivores") as CraftNode)*7)+(CalculateProgress(root.FindNodeById("SmallHerbivores") as CraftNode)*14)+(CalculateProgress(root.FindNodeById("Leviathans") as CraftNode)*7)+(CalculateProgress(root.FindNodeById("Scavengers") as CraftNode)*9))/58.0;
                        break;
                    case "Carnivores":
                        progress=node.childCount/14.0;
                        break;
                    case "Deceased":
                        progress = node.childCount / 7.0;
                        break;
                    case "LargeHerbivores":
                        progress=node.childCount/7.0;
                        break;
                    case "SmallHerbivores":
                        progress=node.childCount/14.0;
                        break; 
                    case "Leviathans":
                        progress=node.childCount/7.0;
                        break;
                    case "Scavengers":
                        progress = node.childCount / 9.0;
                        break;
                    case "Flora":
                        progress=((CalculateProgress(root.FindNodeById("Exploitable") as CraftNode)*12)+(CalculateProgress(root.FindNodeById("Land") as CraftNode)*7)+(CalculateProgress(root.FindNodeById("Sea") as CraftNode)*28))/47.0;
                        break;
                    case "Exploitable":
                        progress=node.childCount/12.0;
                        break;
                    case "Land":
                        progress=node.childCount/7.0;
                        break;
                    case "Sea":
                        progress = node.childCount / 28.0;
                        break;
                    case "Welcome" :
                        progress = ((node.childCount - 1) + (CalculateProgress(root.FindNodeById("StartGear") as CraftNode) * 3) )/ 6.0;
                        break;
                    case "StartGear":
                        progress = node.childCount / 3.0;
                        break;
                    default:
                        progress = 0;
                        break;
                }

                return progress;
            }
            else
            {
                //You can't calculate progress for a non-category node
                return 0;
            }
        }

        public static void UpdateCategory(CraftNode category, CraftNode tree)
        {
            if (category == null)
                return;
            if (!category.string0.StartsWith("EncyPath"))
                return;
            List<TreeNode> reversedPath = category.GetReversedPath(true);
            for (int index = reversedPath.Count - 1; index >= 0; --index)
            {
                if (tree == null)
                    return;
                string id = reversedPath[index].id;
                tree = tree[id] as CraftNode;
            }
            if (tree == null)
                return;
            tree.string1 = Language.main.Get(category.string0) + " " + CalculateProgress(category).ToString("P1");
            uGUI_ListEntry nodeListEntry = uGUI_EncyclopediaTab.GetNodeListEntry(tree);
            if ((UnityEngine.Object) nodeListEntry != (UnityEngine.Object) null)
                nodeListEntry.SetText(tree.string1);
        }

        public static void UpdateAllCategories(CraftNode tree, uGUI_EncyclopediaTab instance)
        {
            string[] categories= {"Advanced","Tech","Equipment","Habitats","Power","Vehicles","DownloadedData","Precursor","Artifacts","Scan","Terminal","AuroraSurvivors","Codes","Degasi","Orders","BeforeCrash","PublicDocs","PlanetaryGeology","Lifeforms","Coral","Fauna","Carnivores","Deceased","LargeHerbivores","SmallHerbivores","Leviathans","Scavengers","Flora","Exploitable","Land","Sea","Welcome","StartGear"};
            foreach (var categoryId in categories)
            {
                var category=tree.FindNodeById(categoryId);
                ProgressCalculator.UpdateCategory(category as CraftNode, tree);
            }
        }
            
    }
}