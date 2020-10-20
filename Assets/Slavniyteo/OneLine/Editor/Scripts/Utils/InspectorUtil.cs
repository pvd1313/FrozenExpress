using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;

using OneLine.Settings;
using System.Collections;

namespace OneLine {
    // Optimization: ommit drawing properties that lie outside window.
    // Grow up performance on drawing bit (100+ arrays)
    internal class InspectorUtil {

        private const string INSPECTOR_WINDOW_ASSEMBLY_QUALIFIED_NAME =
                                "UnityEditor.InspectorWindow, UnityEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string INITIALIZATION_ERROR_MESSAGE = @"OneLine can not initialize Inspector Window Utility. 
You may experience some performance issues. 
Please create an issue on https://github.com/slavniyteo/one-line/ and we will repair it.

";
#if UNITY_2018_1_OR_NEWER
        private const BindingFlags ACCESS_LEVEL = BindingFlags.NonPublic;
#else
        private const BindingFlags ACCESS_LEVEL = BindingFlags.Public;
#endif

        private bool enabled = true;

        private Type inspectorWindowType;
        private MethodInfo getWindowPositionInfo;
        private MethodInfo scrollPositionInfo;

        private object window;
        private object scrollView;

        public InspectorUtil() {
            enabled = false;
            if (EditorWindow.focusedWindow is PopupWindow) return; // Detect is it [Expandable] popup

            try {
                Initialize();
                enabled = true;
            }
            catch (Exception ex){
                Debug.LogError(INITIALIZATION_ERROR_MESSAGE + ex.ToString());
            }
        }

        private void Initialize(){
            inspectorWindowType = Type.GetType(INSPECTOR_WINDOW_ASSEMBLY_QUALIFIED_NAME);
            window = inspectorWindowType
                        .GetField("m_AllInspectors", ACCESS_LEVEL | BindingFlags.Static)
                        .GetValue(null);

            window = (window as IList)[0];

            scrollView = inspectorWindowType.GetField("m_ScrollView", ACCESS_LEVEL | BindingFlags.Instance).GetValue(window);

            scrollPositionInfo = scrollView.GetType().GetProperty("scrollOffset", typeof(Vector2)).GetGetMethod();
            
            getWindowPositionInfo = inspectorWindowType.GetProperty("position", typeof(Rect)).GetGetMethod();
            
            var test = ScrollPosition;
        }

        private Vector2 ScrollPosition { get { return (Vector2) scrollPositionInfo.Invoke(scrollView, null); } }

        private Rect WindowPosition { get { return (Rect) getWindowPositionInfo.Invoke(window, null); } }

        public bool IsOutOfScreen(Rect position){
            if (! enabled) { return false; }

            bool above = (position.y + position.height) < ScrollPosition.y;
            bool below = position.y > (ScrollPosition.y + WindowPosition.height);

            return above || below;
        }

    }
}
