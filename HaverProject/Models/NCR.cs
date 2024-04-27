using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaverProject.Models
{
    public class NCR
    {

        //Quality
        public int ID { get; set; }
       public string? NCRNumber { get; set; } = "12334";
       public long? PONumber { get; set; }

        // Identify Process Applicable
        public int SupplierOrRecInspID { get; set; }
        public SupplierOrRecInsp SupplierOrRecInsp { get; set; }


        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int? QuantityReceived { get; set; }

       public int? QuantityDefected { get; set; }

        
        public string SapId { get; set; }

        public SapNo? SapNo { get; set; }
        public string DescriptionItemID { get; set; }
        public ItemProblem ItemProblem { get; set; }

        public string DescriptionDefect { get; set; }

        // Item Marked

        public int? ItemMarkedID { get; set; }
        public ItemMarked ItemMarked { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Date { get; set; } = DateTime.Today;

        public string RepresentativesName { get; set; }
        //Engineering

        // Review 
        [Display(Name = "Use As Is")]
        public int? UseAsIsId { get; set; }
        public UseAsIs UseAsIs { get; set; }

        // CustomerNotification 
        [Display(Name = "Yes")]
        public bool CustomerYes { get; set; }
        [Display(Name = "No")]
        public bool CustomerNO { get; set; }
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

        [Display(Name = "Yes")]
        public bool reviewyes { get; set; }

        [Display(Name = "No")]
        public bool reviewno { get; set; }
        //
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
        public string? Video { get; set; }

        public List<NCRPhoto> NCRPhotos { get; set; }

        public int? ncrEmail { get; set; }

        //Review
     

      public bool reinyes { get; set; }
        public bool reino { get; set; }
        public bool ncrclosenyes { get; set; }
        public bool ncrcloseno { get; set; }
        public int? newNcrnno { get; set; }
        public string? InspectorName { get; set; }
        public DateTime reviewDate { get; set; }
        public string NcrClosed { get; set; }
        public string Qualitydepartment { get; set; }
        public DateTime finalDate { get; set; }
        public string? VoidReason { get; set; }
        public List<NCRComment> Comments { get; set; }

    }

    public class LessThanReceivedAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public LessThanReceivedAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (propertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = (int?)propertyInfo.GetValue(validationContext.ObjectInstance);

            if (value != null && comparisonValue != null && (int)value > comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
