using System.Linq;
using System.Reflection;
using AFSInterview;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class CustomComponentEditorInspector : UnityEditor.Editor
    {
        private Component component;
        private MethodInfo[] methodsWithButtonAttribute;
        
        private void OnEnable()
        {
            component = (Component)target;
            
            if (component == null)
                return;
            
            methodsWithButtonAttribute = component.GetType().GetMethods()
                .Where(x => x.GetCustomAttributes(true).OfType<InspectorButtonAttribute>().Any()).ToArray();
        }
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (methodsWithButtonAttribute == null || !methodsWithButtonAttribute.Any())
                return;
            
            foreach (var method in methodsWithButtonAttribute)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(component, null);
                }
            }
        }
    }
}