using MildMania.SharedDataSystem;
using UnityEditor;

public class SharedDataCreator
{
	[MenuItem("Assets/Shared Data/Create TestData")]
	public static void MenuItem_MenuItem_TestData()
	{
		ScriptableObjectUtility.CreateAsset<TestData>();
	}
}
