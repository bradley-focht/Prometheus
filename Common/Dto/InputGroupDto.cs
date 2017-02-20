using System.Collections.Generic;

namespace Common.Dto
{
	public class InputGroupDto : IInputGroupDto
	{
		public IEnumerable<ITextInputDto> TextInputs { get; set; }
		public IEnumerable<ISelectionInputDto> SelectionInputs { get; set; }
		public IEnumerable<IScriptedSelectionInputDto> ScriptedSelectionInputs { get; set; }

		/// <summary>
		/// Return a list of all associated user inputs in one, nice, list
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IUserInput> UserInputs
		{
			get
			{
				List<IUserInput> userInputs = new List<IUserInput>();
				if (TextInputs != null)
					userInputs.AddRange(TextInputs);
				if (SelectionInputs != null)
					userInputs.AddRange(SelectionInputs);
				if (ScriptedSelectionInputs != null)
					userInputs.AddRange(ScriptedSelectionInputs);

				return userInputs;
			}
		}
	}
}
