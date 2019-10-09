namespace FileManager
{
	/// <summary>
	/// Factory to easily create ErrorFormPresenters
	/// </summary>
	internal static class ErrorFormFactory
	{
		public static ErrorFormPresenter CreateFromStrings(string errorType, string errorDetail)
		{
			IErrorForm form = new ErrorForm();
			IErrorMessage errMsg = new ErrorMessage { ErrorType = errorType, ErrorDetail = errorDetail };

			return new ErrorFormPresenter(form, errMsg);
		}

		public static ErrorFormPresenter CreateFromException(ICommandException e)
		{
			IErrorForm form = new ErrorForm();
			IErrorMessage errMsg = new ErrorMessage { ErrorType = e.Type, ErrorDetail = e.Message };

			return new ErrorFormPresenter(form, errMsg);
		}
	}
}
