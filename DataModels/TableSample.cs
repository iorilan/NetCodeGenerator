using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class TableSample
    {
        /*replace below codes with data model fields*/

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string EquipmentUniqueId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        public bool? IsPhysical { get; set; }

        [StringLength(100)]
        public string EquipmentType { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CommisionDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DecommissionDate { get; set; }

        public decimal? PriceAtOwnerShip { get; set; }

        [StringLength(255)]
        public string WarrantyDescription { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PreventiveMaintenanceDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public int ContractId { get; set; }

        public void A()
        {
           //var assembly = 
        }
    }
}
