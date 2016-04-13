using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;



	
	public struct Word {

	//holds the current word text(string)
	public string wordText;
	}

	
	//setup the class

	[XmlRoot("WordsRoot")]
	public class AnswerData {
		[XmlArray("Words")]
		[XmlArrayItem("Word")]


	//create a list to hold the words
	public List<Word> words = new List<Word>();


	public static AnswerData LoadFromText(String text){

		//try to load words from xml file, if error, throw an exception
		try {
						
			XmlSerializer serializer = new XmlSerializer(typeof(AnswerData));            
			return serializer.Deserialize(new StringReader(text)) as AnswerData;
		} catch (Exception e) {
			UnityEngine.Debug.LogError("Exception loading question data: " + e);
			return null;
		}
	}

}




