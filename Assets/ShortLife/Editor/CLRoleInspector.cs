using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CLRole), true)]
public class CLRoleInspector : CLBaseLuaInspector
{
	CLRole role;
	Object luaFile;
	string luaPath = "";

	public override void OnInspectorGUI ()
	{
		role = (CLRole)target;
		init ();
		base.OnInspectorGUI ();
		
		CLEditorTools.BeginContents ();
		{
			GUILayout.BeginHorizontal ();
			{
				EditorGUILayout.LabelField ("Lua Speci Proc", GUILayout.Width (100));
				luaFile = EditorGUILayout.ObjectField (luaFile, typeof(UnityEngine.Object), GUILayout.Width (125));
			}
			GUILayout.EndHorizontal ();
		}
		CLEditorTools.EndContents ();

		luaPath =  CLEditorTools.getPathByObject(luaFile);
		role.speciProcLua = Utl.filterPath (luaPath);
		EditorUtility.SetDirty (role);
	}
	
	bool isFinishInit = false;
	
	void init ()
	{
		if (!isFinishInit || luaFile == null) {
			isFinishInit = true;
			
			if (!string.IsNullOrEmpty (role.speciProcLua)) {
				string tmpPath = role.speciProcLua.Replace("/upgradeRes", "/upgradeResMedium");
				luaFile = CLEditorTools.getObjectByPath(tmpPath);
			}
		}
	}
}
