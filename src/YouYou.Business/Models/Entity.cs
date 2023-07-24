using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouYou.Business.Models
{
    public abstract class Entity
    {

        public int Id { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
