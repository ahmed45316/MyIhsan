using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.Parameters
{
   public  class ScreensAssignedParameters
    {
        public string RoleId { get; set; }
        public string[] ScreenAssigned { get; set; }
        public string[] ScreenAssignedRemove { get; set; }
    }
}
