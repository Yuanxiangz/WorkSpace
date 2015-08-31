using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Data;
using System.Xml;

namespace Problem1.Biz
{
	public static class QuestionMap
	{
		private static Dictionary<string, QuestionType> questionMap = new Dictionary<string, QuestionType>();

		public static void LoadMap()
		{
			try
			{
				string xpath = "/QuestionMap/Map";
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load(ConstStr.QUESTIONMAPFILEPATH);

				XmlNodeList list = xmldoc.SelectNodes(xpath);
				foreach (XmlNode node in list)
				{
					questionMap.Add(node.Attributes["Question"].Value, ConvertEnum<QuestionType>(node.Attributes["value"].Value));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ConstStr.UNEXPECTEDLOADINGXMLERROR);
				Console.WriteLine(ex);
			}
		}

		public static Dictionary<string, QuestionType> GetQuestionMap()
		{
			return questionMap;
		}

		private static T ConvertEnum<T>(string input, bool ignoreCase = false) where T : struct
		{
			T s;
			if (Enum.TryParse(input, ignoreCase, out s))
			{
				return s;
			}
			return default(T);
		}
	}
}
