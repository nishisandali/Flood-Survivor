using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Util{
	public class Dictionary{
		private static Dictionary s_instance;
		private string[] words;

		public static Dictionary instance
		{
			get { return s_instance == null ? load() : s_instance;  }
		}
		private Dictionary(string[] words) {
			this.words = words;
		}

		private static bool isWordOK(string word){
			if (word.Length < 1)
				return false;

			foreach (char c in word)
			{
				if (!TextUtil.isAplpha(c))
					return false;
			}

			return true;
		}

		public static Dictionary load(){
			if (s_instance != null)
				return s_instance;

			HashSet<string> wordList = new HashSet<string>();

			//Loaded the Word list
			TextAsset asset = Resources.Load("Words") as TextAsset;
			TextReader src = new StringReader(asset.text);

			//Read all of the lines until the End of file reached
			while (src.Peek() != -1)
			{
				string word = src.ReadLine();

				if (isWordOK(word))
					wordList.Add(word);
			}

			//Unloaded the word list
			Resources.UnloadAsset(asset);

			//Set up the Dictioanry
			string[] words = new string[wordList.Count];
			wordList.CopyTo(words);

			s_instance = new Dictionary(words);
			return s_instance;

		}
		public string next(int limit){
			int index = (int) (Random.value * (words.Length - 1));
			return words[index];
		}
	}
}
