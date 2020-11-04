using System.Collections.Generic;
using System.Linq;

namespace POS.Domain.SeedWork
{
    public class Notification
    {
        public IReadOnlyCollection<string> Errors => this.errors.AsReadOnly();
        public string ErrorsAsString => string.Join(", ", this.errors.ToArray());
        private List<string> errors = new List<string>();

        public void AddError(string error)
        {
            AddErrors(new string[] { error });
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            this.errors = errors.Union(errors).ToList();
        }

        public bool HasErrors => this.errors.Any();
    }
}
