using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class UserDevice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeviceId { get; set; }
    }
}
