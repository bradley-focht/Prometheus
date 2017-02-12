using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAPI.Models
{
    public class Request
    {
        //ID of the Service Request
        public long Id { get; set; }

        //Name of the Service Request
        public string Name { get; set; }

        //Name of the user who submitted the Service Request
        public string Creator { get; set; }

        //Name of the user who is set as the Approver for the Service Request
        public string Approver { get; set; }

    }
}