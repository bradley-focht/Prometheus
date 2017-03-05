using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;

namespace Prometheus.WebUI.Helpers
{
	public class UserInputHelper
	{
		/// <summary>
		/// Convert a list of inputs into a DTO group
		/// </summary>
		/// <param name="userInputs">strings expected to be in the form type_id</param>
		/// <returns></returns>
		public static InputGroupDto MakeInputGroup(ICollection<string> userInputs)
		{
			List<ITextInputDto> textInputs= new List<ITextInputDto>();
			List<IScriptedSelectionInputDto> scriptedInputs = new List<IScriptedSelectionInputDto>();
			List<ISelectionInputDto> selectionInputs = new List<ISelectionInputDto>();

			IInputGroupDto group = new InputGroupDto {TextInputs = new List<ITextInputDto>(), SelectionInputs = new List<ISelectionInputDto>(), ScriptedSelectionInputs = new List<IScriptedSelectionInputDto>()};

			foreach (string input in userInputs)
			{
				string tempString = input.ToLower();
				int inputId;
				// extract the id
				try 
				{
					inputId = int.Parse(tempString.Substring(tempString.IndexOf("_") + 1));
				}
				catch (Exception) { continue; } /*just skip this one then */

				//extract the type
				if (tempString.Contains("text"))
				{
					textInputs.Add(new TextInputDto{Id = inputId});
				}
				else if (tempString.Contains("script"))		//the order here matters - think: scripted selection vs selection
				{
					scriptedInputs.Add(new ScriptedSelectionInputDto {Id = inputId});	
				}
				else if (tempString.Contains("select"))
				{
					selectionInputs.Add(new SelectionInputDto {Id = inputId});
				}
			}
			group.TextInputs = textInputs;
			group.SelectionInputs = selectionInputs;
			group.ScriptedSelectionInputs = scriptedInputs;

			return (InputGroupDto)group;
		}

		/// <summary>
		/// Create an input group from an enumerable of IUserInputs
		/// </summary>
		/// <param name="userInputs"></param>
		/// <returns></returns>
		public static InputGroupDto MakeInputGroupDto(IEnumerable<IUserInput> userInputs)
		{
			if (userInputs == null)
				return new InputGroupDto();

			var scriptedSelectionInputs = new List<IScriptedSelectionInputDto>();
			var selectionInputs = new List<ISelectionInputDto>();
			var textInputs = new List<ITextInputDto>();

			foreach (var input in userInputs)
			{
				if (input is ITextInputDto)
				{
					textInputs.Add((ITextInputDto)input);
				}
				else if (input is ISelectionInputDto)
				{
					selectionInputs.Add((ISelectionInputDto) input);
				}
				else if (input is IScriptedSelectionInputDto)
				{
					scriptedSelectionInputs.Add((IScriptedSelectionInputDto)input);
				}
			}
			InputGroupDto inputGroup = new InputGroupDto {TextInputs = textInputs, SelectionInputs = selectionInputs, ScriptedSelectionInputs = scriptedSelectionInputs};
			return inputGroup;
		}
	}
}