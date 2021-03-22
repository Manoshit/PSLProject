using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class Order
    {
        public static List<string> statusValues = new List<string> { "Being Prepared", "Out For Delivery", "Delivered" };

        public int id { get; set; }
        public virtual Registeration customer { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public string status { get; set; }
        public double totalAmount { get; set; }
        public string feedback { get; set; }
        public string timeStamp { get; set; }
        public virtual Registeration DeliveryExecutive { get; set; }

    }
    //public class Order
    //{
    //    public static List<string> statusValues = new List<string> { "Being Prepared", "Out For Delivery", "Delivered" };

    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int id { get; set; }
    //    // TODO : Integrate with correct user model
    //    [ForeignKey("customer")]
    //    public int customerId { get; set; }

    //    public virtual Registeration customer { get; set; }
    //    [InverseProperty("Order")]
    //    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    //    public string status { get; set; }
    //    public double totalAmount { get; set; }
    //    public string feedback { get; set; }
    //    public string timeStamp { get; set; }
    //    //public virtual DeliveryExecutive DeliveryExecutive { get; set; }

    //}




}