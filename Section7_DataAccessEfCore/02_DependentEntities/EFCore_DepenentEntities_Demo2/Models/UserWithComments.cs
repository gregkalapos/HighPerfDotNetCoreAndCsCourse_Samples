using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_DepenentEntities_Demo2.Models
{
    public class UserWithComments
    {
        public UserWithComments()
        {
            Comments = new List<string>();
        }
		public String UserName { get; set; }
		public List<String> Comments { get; set; }
	}
}
