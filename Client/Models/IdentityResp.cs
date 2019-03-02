using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models
{
    class IdentityResp
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public Userid UserId { get; set; }

        public class Userid
        {
            public int Id { get; set; }
            public int UsId { get; set; }
        }

    }
}
