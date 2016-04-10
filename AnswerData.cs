using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;



	public struct Word {
	public string wordText;
	}

	
	[XmlRoot("WordsRoot")]
	public class AnswerData {
		[XmlArray("Words")]
		[XmlArrayItem("Word")]



	public List<Word> words = new List<Word>();

	public static AnswerData LoadFromText(String text){

		try {
			
			XmlSerializer serializer = new XmlSerializer(typeof(AnswerData));            
			return serializer.Deserialize(new StringReader(text)) as AnswerData;
		} catch (Exception e) {
			UnityEngine.Debug.LogError("Exception loading question data: " + e);
			return null;
		}
	}

}




