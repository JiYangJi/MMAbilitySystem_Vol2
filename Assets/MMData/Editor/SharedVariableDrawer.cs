using UnityEditor;
using UnityEngine;

/*namespace MildMania.Blackboard
{
    [CustomPropertyDrawer(typeof(SharedVariable), true)]
    public class SharedVariableDrawer : PropertyDrawer
    {
        private Rect _position;
        private float _currentY;
        private SerializedProperty _property;

        private float _totalHeight = 0;

        private SerializedProperty _isBindedProperty;
        private SerializedProperty _nameProperty;
        private SerializedProperty _valueProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _totalHeight = 0;

            _position = position;
            _currentY = _position.y;
            _property = property;

            _isBindedProperty = _property.FindPropertyRelative("_isBinded");

            if(_nameProperty == null)
                _nameProperty = _property.FindPropertyRelative("_name");

            if(_valueProperty == null)
                _valueProperty = _property.FindPropertyRelative("_value");

            EditorGUI.BeginProperty(position, label, _property);

            DrawUILine(Color.white);

            Rect labelRect = new Rect(_position.x, _currentY, _position.width, 15);

            EditorGUI.LabelField(labelRect, _property.displayName);

            _totalHeight += 25;
            _currentY += 25;

            Rect buttonRect = new Rect(_position.x, _currentY, _position.width, 10);

            _property.serializedObject.Update();
            _isBindedProperty.boolValue = GUI.Toggle(buttonRect, _isBindedProperty.boolValue, "Is Binded");
            _property.serializedObject.ApplyModifiedProperties();

            _totalHeight += EditorGUI.GetPropertyHeight(_isBindedProperty);
            _currentY += EditorGUI.GetPropertyHeight(_isBindedProperty);

            Rect propertyRect = new Rect(_position.x, _currentY, _position.width, EditorGUI.GetPropertyHeight(_isBindedProperty));

            if (_isBindedProperty.boolValue)
            {
                EditorGUI.PropertyField(propertyRect, _nameProperty);
                _totalHeight += EditorGUI.GetPropertyHeight(_nameProperty);
            }
            else
            {
                EditorGUI.PropertyField(propertyRect, _valueProperty);
                _totalHeight += EditorGUI.GetPropertyHeight(_valueProperty);
            }

            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _totalHeight;
        }

        public void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = new Rect(_position.x, _currentY, _position.width, thickness);
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);

            _totalHeight += thickness + padding;
            _currentY += thickness + padding;
        }
    }
}*/