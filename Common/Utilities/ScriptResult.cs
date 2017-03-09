using System;

namespace Common.Utilities
{
	public class ScriptResult<TValue, TLabel>
	{
		public TValue Value { get; set; }
		public TLabel Label { get; set; }
	}
}
