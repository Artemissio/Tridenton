using System.ComponentModel.DataAnnotations;

namespace Tridenton.Core.Utilities;

public abstract class ConditionalRequirementAttribute<TModel> : ValidationAttribute
    where TModel : class
{
    private readonly Func<TModel, bool> _condition;

    protected ConditionalRequirementAttribute(Func<TModel, bool> condition)
    {
        _condition = condition;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not TModel model)
        {
            throw new InvalidCastException($"Validation instance must be of type {typeof(TModel).Name}");
        }

        return _condition(model)
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}