using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsears2749ex1a1ef.Model
{
    public partial class Employee
    {

        public string LastFirstName
        {

            get
            {

                return this.Person.LastName + ", " + this.Person.FirstName;

            }

        }


    }
}
