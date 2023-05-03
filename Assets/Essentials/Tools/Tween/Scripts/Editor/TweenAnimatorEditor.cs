using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenAnimator))]
public class TweenAnimatorEditor : Editor
{
    private SerializedProperty movementType;
    private SerializedProperty movementSpeed;
    private SerializedProperty timeTillDestination;
    private SerializedProperty destination;
    private SerializedProperty circleRadius;
    private SerializedProperty spiralRadius;
    private SerializedProperty spiralFrequency;
    private SerializedProperty pingPongActive;

    TweenAnimator animator;

    private void OnEnable()
    {
         animator = (TweenAnimator)target;
        // Get references to all of the serialized properties
        movementType = serializedObject.FindProperty("movementType");
        movementSpeed = serializedObject.FindProperty("movementSpeed");
        timeTillDestination = serializedObject.FindProperty("timeTillDestination");
        destination = serializedObject.FindProperty("destination");
        circleRadius = serializedObject.FindProperty("circleRadius");
        spiralRadius = serializedObject.FindProperty("spiralRadius");
        spiralFrequency = serializedObject.FindProperty("spiralFrequency");
        pingPongActive = serializedObject.FindProperty("pingPongActive");


    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Display the movement type selector
        EditorGUILayout.PropertyField(movementType);




        // Display the circle radius slider if the movement type is Circle
        bool check1 = movementType.enumValueIndex == (int)TweenAnimator.MovementType.ToWorldPosition;
        bool check2 = movementType.enumValueIndex == (int)TweenAnimator.MovementType.ToLocalPosition;
        bool check3 = movementType.enumValueIndex == (int)TweenAnimator.MovementType.Circle;
        bool check4 = movementType.enumValueIndex == (int)TweenAnimator.MovementType.Spiral;
        if (check1 || check2)
        {
            // Display the destination vector fields
            EditorGUILayout.PropertyField(timeTillDestination, new GUIContent("Time Till Destination"));
            EditorGUILayout.PropertyField(destination);
        }


        // Display the circle radius slider if the movement type is Circle
        if (check3)
        {
            // Display the movement speed slider
            EditorGUILayout.PropertyField(timeTillDestination, new GUIContent("Time Till Destination"));
            EditorGUILayout.PropertyField(movementSpeed, new GUIContent("Movement Speed"));
            EditorGUILayout.PropertyField(circleRadius, new GUIContent("Circle Radius"));
        }

        // Display the spiral radius and frequency sliders if the movement type is Spiral
        if (check4)
        {
            // Display the movement speed slider
            EditorGUILayout.PropertyField(movementSpeed, new GUIContent("Movement Speed"));
            EditorGUILayout.PropertyField(spiralRadius, new GUIContent("Spiral Radius"));
            EditorGUILayout.PropertyField(spiralFrequency, new GUIContent("Spiral Frequency"));
        }


        GUILayout.BeginHorizontal();
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Start"))
            {
                // Get a reference to the target component and call the StartMovement method
                animator.StartMovement();
            }
            GUI.backgroundColor = Color.white;
            if (GUILayout.Button("STOP"))
            {
                animator.StopAll();
            }
        }
        
        // Display the Start Movement button

        if (animator.pingPongActive)
        {
            GUI.backgroundColor = Color.green;
        }
        else
        {
            GUI.backgroundColor = Color.red;
        }
        if (GUILayout.Button("Ping Pong"))
        {
            animator.PingPong();
        }


        GUILayout.EndHorizontal();


        // Apply any changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
