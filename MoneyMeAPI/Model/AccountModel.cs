using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.Model
{
    [ExcludeFromCodeCoverage]
    public class BlacklistModel
    {
        public Guid Id { get; set; }
        public string  type { get; set; }
        public string value { get; set; }
    }
}
