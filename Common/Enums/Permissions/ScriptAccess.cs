namespace Common.Enums.Permissions
{
	/// <summary>
	/// Level of access for executing scripts
	/// </summary>
	public enum ScriptAccess
	{
		/// <summary>
		/// User cannot perform any action that involves the execution of a script
		///	e.g. any user inputs that execute scripts 
		///  </summary>
		NoAccess,

		/// <summary>
		/// Users have access to functions that execute scripts
		/// </summary>
		CanExecute,
		
		/// <summary>
		/// Access to the script maintenance area of prometheus and access to 
		/// update and modify scripts
		/// </summary>
		CanEdit
	}
}