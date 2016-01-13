using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbideByTheRules
{
    public class CheckUser
    {
        private static bool checkuser;

        public static bool CheckTypeOfUser
        {
            get { return checkuser; }
            set { checkuser = value; }
        }
        
    }
}
