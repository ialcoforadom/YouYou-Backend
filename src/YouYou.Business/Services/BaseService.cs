using FluentValidation.Results;
using FluentValidation;
using YouYou.Business.Interfaces;
using YouYou.Business.ErrorNotifications;
using YouYou.Business.Models;

namespace YouYou.Business.Services
{
    public abstract class BaseService
    {
        private readonly IErrorNotifier _errorNotifier;

        public BaseService(IErrorNotifier errorNotifier)
        {
            _errorNotifier = errorNotifier;
        }

        protected void NotifyError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotifyError(error.ErrorMessage);
            }
        }

        protected void NotifyError(string message)
        {
            _errorNotifier.Handle(new ErrorNotification(message));
        }

        protected IEnumerable<string> GetErrorNotifications()
        {
            return _errorNotifier.GetErrorNotifications().Select(s => s.Message).Distinct();
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
            {
                return true;
            }

            NotifyError(validator);

            return false;
        }

        protected bool ExecuteValidation<TE>(TE entity) where TE : Entity
        {
            if (entity.IsValid())
            {
                return true;
            }

            NotifyError(entity.ValidationResult);

            return false;
        }
    }
}
