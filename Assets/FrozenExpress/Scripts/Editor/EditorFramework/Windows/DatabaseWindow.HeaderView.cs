using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    partial class DatabaseWindow
    {
        [System.Serializable]
        private class DatabaseWindowHeaderView
        {
            private static readonly GUIContent HeaderButtonContent = new GUIContent();
            private static readonly GUILayoutOption HeaderButtonWidth = GUILayout.Width(24);
            
            private readonly DatabaseWindow _owner;
            
            public DatabaseWindowHeaderView(DatabaseWindow owner)
            {
                _owner = owner;
            }
            
            public int OnGUI()
            {
                var result = 0;

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Database", GUILayout.Width(60));
                    _owner._database = AssetField("", _owner._database);
                    result |= HeaderButton("D", 8, "Duplicate selected item");
                    result |= HeaderButton("▲", 4, "Move up selected item");
                    result |= HeaderButton("▼", 2, "Move down selected item");
                    result |= HeaderButton("X", 1, "Remove selected item");
                }
                EditorGUILayout.EndHorizontal();
            
                return result;
            }
            
            public void HandleAction(int action)
            {
                if (action == 0)
                {
                    return;
                }
            
                var databaseInfo = DatabaseScanner.GetInfo(_owner._database);
                var databaseList = _owner._databaseSerialized.FindProperty(databaseInfo.ListName);
                var selectedItem = databaseList.GetArrayElementAtIndex(_owner._selectedItemIndex);
                
                switch (action)
                {
                    case 1:
                        selectedItem.DeleteCommand();
                        break;
                    case 8:
                        selectedItem.DuplicateCommand();
                        break;
                    case 4:
                        if (databaseList.MoveArrayElement(_owner._selectedItemIndex, _owner._selectedItemIndex - 1))
                        {
                            _owner._selectedItemIndex--;
                        }
                        break;
                    case 2:
                        if (databaseList.MoveArrayElement(_owner._selectedItemIndex, _owner._selectedItemIndex + 1))
                        {
                            _owner._selectedItemIndex++;
                        }
                        break;
                }

                _owner._databaseSerialized.ApplyModifiedProperties();
            }

            private static int HeaderButton(string icon, int flag, string tooltip)
            {
                HeaderButtonContent.text = icon;
                HeaderButtonContent.tooltip = tooltip;
            
                return GUILayout.Button(HeaderButtonContent, HeaderButtonWidth) ? flag : 0;
            }
            
            private static T AssetField<T>(string label, T asset) where T : Object
            {
                return EditorGUILayout.ObjectField(label, asset, typeof(T), false) as T;
            }
        }
    }
}