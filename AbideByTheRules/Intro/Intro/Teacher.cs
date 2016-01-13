/**
 * Boyan Bonev @2011/2012
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbideByTheRules
{
    class Teacher
    {
        string _username;
        string _password;
        string _fName;
        string _lName;

        public Teacher(string username, string password, string fName, string lName)
        {
            this._username = username;
            this._password = password;
            this._fName = fName;
            this._lName = lName;
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string FName
        {
            get { return _fName; }
        }

        public string LName
        {
            get { return _lName; }
        }
    }
}
