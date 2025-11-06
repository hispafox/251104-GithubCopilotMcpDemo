using ApiDemo.Application.DTOs;
using FluentValidation;

namespace ApiDemo.Application.Validators;

public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
{
    public UpdateTaskDtoValidator()
    {
        RuleFor(x => x.Title)
   .NotEmpty().WithMessage("Title is required")
       .Length(3, 200).WithMessage("Title must be between 3 and 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Status)
.NotEmpty().WithMessage("Status is required")
          .Must(BeValidStatus).WithMessage("Status must be Pending, InProgress, or Completed");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required")
  .Must(BeValidPriority).WithMessage("Priority must be Low, Medium, or High");

        RuleFor(x => x.StartDate)
       .Must(BeInFuture).When(x => x.StartDate.HasValue)
        .WithMessage("La fecha de inicio debe ser en el futuro");

 RuleFor(x => x.DueDate)
            .Must(BeInFuture).When(x => x.DueDate.HasValue)
        .WithMessage("DueDate must be in the future");

      RuleFor(x => x)
 .Must(HaveValidDateRange)
       .WithMessage("La fecha de inicio debe ser anterior o igual a la fecha de vencimiento")
          .When(x => x.StartDate.HasValue && x.DueDate.HasValue);
    }

    private bool BeValidStatus(string status)
  {
        return status == "Pending" || status == "InProgress" || status == "Completed";
    }

    private bool BeValidPriority(string priority)
    {
        return priority == "Low" || priority == "Medium" || priority == "High";
    }

    private bool BeInFuture(DateTime? date)
    {
        return !date.HasValue || date.Value > DateTime.UtcNow;
    }

    private bool HaveValidDateRange(UpdateTaskDto dto)
    {
        if (!dto.StartDate.HasValue || !dto.DueDate.HasValue)
        return true;
        
     return dto.StartDate.Value <= dto.DueDate.Value;
    }
}
