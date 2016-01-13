/**
 * Boyan Bonev @2011/2012
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbideByTheRules
{
    public class TypeOfUser
    {
        private static bool chekUser;

        public static bool ChekUser
        {
            get { return TypeOfUser.chekUser; }
            set { TypeOfUser.chekUser = value; }
        }

        private static string nameUser;

        public static string NameUser
        {
            get { return TypeOfUser.nameUser; }
            set { TypeOfUser.nameUser = value; }
        }
    }
}
