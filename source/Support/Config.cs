using KSP.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExceptionDetector
{
	public class Config
	{
		private static string _showFullLog = "ShowFullLog";
		private static string _hideKnowns = "HideKnowns";
		private static string _showInfoMessage = "ShowInfoMessages";
		private static string _singlePass = "SinglePass";
		private static string _doublePass = "DoublePass";
        private static string _whitelist = "Whitelist";
        private static string _wordwrap = "WordWrap";
		private static string _fixedwidth = "FixedWidth";
		private static string _usewhitelist = "UseWhitelist";
		public static void Load()
		{
			try
			{
				if (File.Exists(ExceptionDetector.SettingsFile))
				{
					var node = ConfigNode.Load(ExceptionDetector.SettingsFile);
					if (node == null) return;

					var root = node.GetNode("ExceptionDetector");
					if (root == null) return;

					var settings = root.GetNode("Config");
					if (settings == null) return;

					var singleNode = settings.GetNode(_singlePass);
					if (singleNode != null)
						ConvertToDictionary(singleNode, ExceptionDetector.SinglePassValues);
					var doubleNode = settings.GetNode(_doublePass);
					if(doubleNode != null)
						ConvertToDictionary(doubleNode, ExceptionDetector.DoublePassValues);

                    var whitelistNode = settings.GetNode(_whitelist);
                    if (whitelistNode != null)
                        ConvertToDictionary(whitelistNode, ExceptionDetector.WhitelistValues);


                    var set = settings.GetValue(_showFullLog);
					if (bool.TryParse(set, out var settf)) ExceptionDetector.FullLog = settf;

					var knowns = settings.GetValue(_hideKnowns);
					if (bool.TryParse(knowns, out var knowntf)) ExceptionDetector.HideKnowns = knowntf;

					var shInfo = settings.GetValue(_showInfoMessage);
					if (bool.TryParse(shInfo, out var llmtf)) ExceptionDetector.ShowInfoMessage = llmtf;

                    var wordwrap = settings.GetValue(_wordwrap);
                    if (bool.TryParse(wordwrap, out var wrap)) ExceptionDetector.WordWrap = wrap;

                    var fixedwidth = settings.GetValue(_fixedwidth);
                    if (bool.TryParse(fixedwidth, out var fw)) ExceptionDetector.FixedWidth = fw;
                    var usewhitelist = settings.GetValue(_usewhitelist);
                    if (bool.TryParse(usewhitelist, out var uwl)) ExceptionDetector.UseWhitelist = uwl;

                }
                Save();
			}
			catch (Exception ex)
			{
				ExceptionDetector.WriteLog(ex.ToString());
			}
		}

		private static void ConvertToDictionary(ConfigNode node, Dictionary<string, string> passValues)
		{
			for (int x = 0; x < node.CountValues; x++)
			{
				passValues.Add(node.values[x].name, node.values[x].value);
			}
		}

		private static ConfigNode ConvertFromDictionary(String name, Dictionary<string, string> passValues)
		{
			ConfigNode node = new ConfigNode(name);

			foreach (KeyValuePair<string,string> val in passValues)
			{
				node.AddValue(val.Key, val.Value);
			}
			return node;
		}

		public static void Save()
		{
			try
			{
				var node = new ConfigNode();
				var root = node.AddNode("ExceptionDetector");
				var settings = root.AddNode("Config");
				settings.AddValue(_showFullLog, ExceptionDetector.FullLog);
				settings.AddValue(_hideKnowns, ExceptionDetector.HideKnowns);
				settings.AddValue(_showInfoMessage, ExceptionDetector.ShowInfoMessage);
				var sNode = settings.AddNode(ConvertFromDictionary(_singlePass, ExceptionDetector.SinglePassValues));
				var dNode = settings.AddNode(ConvertFromDictionary(_doublePass, ExceptionDetector.DoublePassValues));

                settings.AddValue(_wordwrap, ExceptionDetector.WordWrap);
                settings.AddValue(_fixedwidth, ExceptionDetector.FixedWidth);
                settings.AddValue(_usewhitelist , ExceptionDetector.UseWhitelist);

                node.Save(ExceptionDetector.SettingsFile);
			}
			catch (Exception ex)
			{
				ExceptionDetector.WriteLog(ex.ToString());
			}
		}
	}		
}
