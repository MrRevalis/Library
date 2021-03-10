using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Library.Domain.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string item)
        {
            IdentityResult identityResult = await base.ValidateAsync(item);
            if (item.Equals("zaq1@WSX"))
            {
                var errors = identityResult.Errors.ToList();
                errors.Add("Password must not consist of known passwords");
                identityResult = new IdentityResult(errors);
            }
            return identityResult;
        }
    }
}
