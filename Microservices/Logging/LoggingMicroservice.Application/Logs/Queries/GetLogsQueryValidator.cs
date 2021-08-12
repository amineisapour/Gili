using FluentValidation;

namespace LoggingMicroservice.Application.Logs.Queries
{
	public class GetLogsQueryValidator :
		FluentValidation.AbstractValidator<GetLogsQuery>
	{
		public GetLogsQueryValidator() : base()
		{
			RuleFor(current => current.Count)
				.NotEmpty()
				.WithMessage(errorMessage: Resources.Messages.Validations.ErrorRequiredFluent)

				.GreaterThan(valueToCompare: 0)
				.WithMessage(errorMessage: Resources.Messages.Validations.ErrorGreaterThanFluent)

				.LessThan(valueToCompare: 101)
				.WithMessage(errorMessage: Resources.Messages.Validations.ErrorLessThanFluent)
				;
		}
	}
}
