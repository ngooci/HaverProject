using System.ComponentModel.DataAnnotations;

namespace HaverProject.Models
{
    public class NCR
    {

    //Quality
        public int ID { get; set; }
        public string? NCRNumber { get; set; } = "12334";
        public long? PONumber { get; set; }

        // Identify Process Applicable

        [Display(Name = "Supplier Or Rec-Insp")]
        public bool SupplierOrRecInsp { get; set; }

        [Display(Name = "WIP (Production Order)")]
        public bool WIP { get; set; }

        public string? Supplier { get; set; }
        public int? QuantityReceived { get; set; }
        public int? QuantityDefected { get; set; }
        public string? DescriptionItem { get; set; }

        public string? DescriptionDefect { get; set; }

        // Item Marked
        public bool Yes { get; set; }
        public bool No { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string? RepresentativesName { get; set; }
        


        //Engineering

        // Review 
        [Display(Name = "Use As Is")]
        public bool UseAsIs { get; set; }
        public bool Repair { get; set; }
        public bool Rework { get; set; }
        public bool Scrap { get; set; }
        //
        // CustomerNotification 
        [Display(Name = "Yes")]
        public bool CustomerYes { get; set; }
        [Display(Name = "No")]
        public bool CustomerNO { get; set; }
        //
        public string? Disposition { get; set; }
        // DrawingUpdate 

        [Display(Name = "Yes")]
        public bool DrawingYes { get; set; }

        [Display(Name = "No")]

        public bool DrawingNo { get; set; }
        //
        public string? OriginalRev { get; set; }
        public string? UpdatedRev { get; set; }
        public string? NameOfEngineer { get; set; }
        public DateTime? RevisingDate { get; set; }
        public string? Engineer { get; set; }
        public DateTime? EngineerDate { get; set; }


        // Purchase

        // PreliminaryDecision 
        [Display(Name = "Return to Supplier for either 'rework' or 'replacement'")]
        public bool ReturntoSupplier { get; set; }

        [Display(Name = "Rework 'In-House'")]
        public bool ReworkInHouse { get; set; }

        [Display(Name = "Scrap in House")]
        public bool ScrapinHouse { get; set; }

        [Display(Name = "Defer for HBC Engineering Review")]
        public bool DeferforHBC { get; set; }

        public string? PreliminaryDecision { get; set; }
        //
        // CARRaised 

        [Display(Name = "Yes")]
        public bool CARYes { get; set; }

        [Display(Name = "No")]
        public bool CARNo { get; set; }
        //
        // FollowupRaised 

        [Display(Name = "Yes")]
        public bool FollowupYes { get; set; }

        [Display(Name = "No")]
        public bool FollowupNo { get; set; }
        //
        public DateTime? PurchaseDate { get; set; }
        public string? OperatingManagerName { get; set; }
        public string? Status { get; set; }

        
    }
}
