/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

25/07/2023	1.0.0.1		JVT, Skyline	Initial version
****************************************************************************
*/

namespace SetParameter_1
{
    using Newtonsoft.Json;
    using Skyline.DataMiner.Automation;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a DataMiner Automation script.
    /// </summary>
    public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			var element = GetElement(engine);
			if (element == null)
			{
				return;
			}

			SetParameter(engine, element);
		}

		private static Element GetElement(IEngine engine)
		{
            var elementIdInputParameter = GetFirstValueFromStringArrayJson(engine.GetScriptParam("elementId").Value);

            try
			{
				return engine.FindElementByKey(elementIdInputParameter);
			}
			catch (Exception e)
			{
				engine.Log($"Exception retrieving element with id {elementIdInputParameter}: {e}");
				return null;
			}
        }

		private static void SetParameter(IEngine engine, Element element)
		{
            var parameterIdInput = Convert.ToInt32(engine.GetScriptParam("ParameterId").Value);
            var primaryKeyInput = GetFirstValueFromStringArrayJson(engine.GetScriptParam("PrimaryKey").Value);
            var valueInput = GetFirstValueFromStringArrayJson(engine.GetScriptParam("Value").Value);

			try
			{
				if (String.IsNullOrEmpty(primaryKeyInput))
				{
					element.SetParameter(parameterIdInput, valueInput);
				}
				else
				{
					element.SetParameterByPrimaryKey(parameterIdInput, primaryKeyInput, valueInput);
				}
			}
			catch (Exception e)
			{
                engine.Log($"Exception setting parameter with id {parameterIdInput} and primary key {primaryKeyInput} to value {valueInput} on element {element.ElementName}: {e}");
            }
        }

		private static string GetFirstValueFromStringArrayJson(string input)
		{
			try
			{
				return JsonConvert.DeserializeObject<string[]>(input)
					.DefaultIfEmpty(String.Empty)
					.FirstOrDefault();
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}
	}
}