using System;
using System.Collections.Generic;

#nullable disable

namespace ShoesStore.DAL.Models
{
    public partial class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
