using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MongoDB.Bson;
using XmlTester.PvpCompetitions;

namespace XmlTester
{
    class Program
    {
        static void Main(string[] args)
        {

            // тип ресурса
            var resType = "gold";
            var res = new PalyerResources();

            // значение русурса
            int newResourceValue = 800;

            // изменение значения ресурса
            res.SetNewResourceValue(resType, newResourceValue);

            // новый тип ресурса
            var newResourceType = "bronze";
            res.SetNewResourceType(newResourceType);

            // удаления любого ресурса
            var resourceType = "silver";
            res.DelResourceType(resourceType);

            // трата ресурса
            var resourceToSpend = "gold";
            var spent = res.SpendResource(resourceToSpend, 0);

            var resAmount = res.GetResourceCount(resType);
            
            // вывод значение ресурса указанного типа в консоль
            Console.WriteLine(resAmount);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            
        }

        class PalyerResources
        {
            // словарь 
            private Dictionary<string, int> resources = new Dictionary<string, int>()   
        {
            {"silver", 10 },                                        
            {"gold", 55 },
           
        };

            // метод проверки ключа (проверка на существование ресурса, ошибки и пр.)

            
            public int GetResourceCount(string resourceToGet)
            {
                bool hasKey = resources.ContainsKey(resourceToGet);
                if (hasKey)
                {
                    int a = resources[resourceToGet];
                    return a;
                }
                else
                {
                    Console.WriteLine("Error! Unknown resource type!");
                    return 0;
                }

            }


            public int GetResourceCountThroughTryGetValue(string resourceToGet)
            {
                int val;
                resources.TryGetValue(resourceToGet, out val);
                return val;

            }

            // метод изменения значения ресурса (так же проверка на существование ресурса)
            public bool SetNewResourceValue(string resourceToGet, int valueToSet)
            {
                bool hasResource = resources.ContainsKey(resourceToGet);
                if (hasResource)
                {
                    resources[resourceToGet] = valueToSet;
                    return true;
                }
                else
                {
                    Console.WriteLine("Error! Resource not found!");
                    return false;
                }

            }

            // метод добавления нового ресурса со значение 0
            public void SetNewResourceType(string newResourceType)
            {
                if (!resources.ContainsKey(newResourceType))
                {
                    resources.Add(newResourceType, 0);
                }

            }
            
            // метод удаление любого ресурса 
            public void DelResourceType(string resourceType)
            {
                resources.Remove(resourceType);
            
            }
        

            // метод списывания ресурса
            public bool SpendResource(string resourceName, int valueToSpend)
            {
                int availableRes = GetResourceCount(resourceName);
                if (valueToSpend < availableRes)
                    return false;
                return SetNewResourceValue(resourceName, availableRes - valueToSpend);
                
            }

            // метод списывания ресурса
            public bool SpendResourceTryGet(string resourceName, int valueToSpend)
            {
                int availableRes;
                if (resources.TryGetValue(resourceName, out availableRes))
                {
                    return SetNewResourceValue(resourceName, availableRes - valueToSpend);
                }
                return false;
                

            }

        }

    }    
}
