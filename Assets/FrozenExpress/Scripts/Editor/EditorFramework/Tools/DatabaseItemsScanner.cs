using System.Collections;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Tools
{
    public static class DatabaseItemsScanner
    {
        public static void GetItems(object database, IList result)
        {
            result.Clear();

            if (!CheckValidDatabase(database))
            {
                return;
            }

            var typeInfo = DatabaseTypeScanner.GetInfo(database.GetType());
            var itemList = typeInfo.ItemListField.GetValue(database) as IList;
            if (itemList == null)
            {
                return;
            }

            foreach (var item in itemList)
            {
                result.Add(item);
            }
        }
        
        public static bool CheckValidDatabase(object database)
        {
            if (database == null)
            {
                return false;
            }
            
            var typeInfo = DatabaseTypeScanner.GetInfo(database.GetType());
            
            return typeInfo.ItemListField != null;
        }
    }
}