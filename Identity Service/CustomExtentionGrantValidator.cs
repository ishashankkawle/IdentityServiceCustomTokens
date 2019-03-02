using Identity_Service.Data;
using Identity_Service.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Service
{
    public class CustomExtentionGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;
        private UserContext _context;

        public CustomExtentionGrantValidator(ITokenValidator validator , UserContext context)
        {
            _validator = validator;
            _context = context;
        }

        public string GrantType => "custom";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userToken = context.Request.Raw.Get("token");

            if(string.IsNullOrEmpty(userToken))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return ;
            }

            var result = await validateAccessTokenAsync(userToken);
            
            if(result == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient);
                return;
            }

            var dict = new Dictionary<string, object>()
            {
                {"UserId" , result }
            };

            context.Result = new GrantValidationResult(dict);
            return;
        }

        private async Task<object> validateAccessTokenAsync(string userToken)
        {
            var resp = await _context.UserData.FindAsync(int.Parse(userToken));
            return resp;
        }
    }
}
