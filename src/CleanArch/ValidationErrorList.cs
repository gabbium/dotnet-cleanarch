namespace CleanArch;

public sealed record ValidationErrorList(IReadOnlyList<ValidationError> Errors)
    : Error(ErrorType.Validation, "One or more validation errors occurred");
