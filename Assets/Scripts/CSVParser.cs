using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CSVParser : MonoBehaviour {
	public TextAsset file;
	public int maxColumnNumber;
	private List<Dictionary<string, List<string>>> Reciple = new List<Dictionary<string, List<string>>>();
	//private TextAsset data = Resources.Load (file) as TextAsset;
	private string pattern = ",|\r";
	public void Start(){
		
		string[] lineparse = Regex.Split(file.text,pattern);
		for (int i = 0;	i < lineparse.Length; i=i+4) {
			//Debug.Log (lineparse[i] + " " + lineparse[(i + 1)] + " "  + " " + lineparse[(i + 2)] + " " + lineparse[(i + 3)] + "\n");
		}
	}
	void Awake(){
		
	}
}
