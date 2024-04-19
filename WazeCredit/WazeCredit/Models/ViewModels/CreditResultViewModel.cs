using System.Collections.Generic;
using System;

namespace WazeCredit.Models.ViewModels
{
    public class CreditResultViewModel
    {
        public bool Success { get; set; }
        public IEnumerable<String> ErrorList { get; set; }
        public int CreditID { get; set; }
        public double CreditApproved { get; set; }
    }
}
