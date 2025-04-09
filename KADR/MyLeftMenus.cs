using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KADR
{
    static class MyLeftMenus
    {
        public enum MenuType : int
        {
            Root,
            Tables
        };
        static public MenuType currentMenuType = MenuType.Root;
        static public readonly List<string>[] myMenus = new List<string>[] {
            new List<string>() { "📁 Справочники", "⚙️ Админист-е", "📝 Приказы", "📊 Отчеты" },
            new List<string>() { " . .", "Users", "TypeDocs", "Tree", "SaveDocs", "PropValue", "Prop", "Post", "Peoples", "JornalTabel", "JornalKard", "FieldsJornal", "Department", "ClassName", "ClassArr", "Class" }
        };
        static public readonly Dictionary<string, int> Element = new Dictionary<string, int>();
        static MyLeftMenus()
        {

            for (int o = 0; o < myMenus.Length; o++)
            {
                for (int i = 0; i < myMenus[o].Count; i++)
                {
                    Element.Add(myMenus[o][i], i);
                }
            }
        }
    }
}
