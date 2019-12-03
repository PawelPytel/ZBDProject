//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektBazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Animal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animal()
        {
            this.DiseaseHistories = new HashSet<DiseaseHistory>();
        }
        [Display(Name ="Id zwierz�cia")]
        public int id { get; set; }
        [Display(Name = "Wiek")]
        public int age { get; set; }
        [Display(Name = "P�e�")]
        public string sex { get; set; }
        [Display(Name = "Pochodzenie")]
        public string origin { get; set; }
        [Display(Name = "W Zoo od:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime inZooSince { get; set; }
        [Display(Name = "Imi�")]
        public string name { get; set; }
        [Display(Name = "Gatunek")]
        public string spiece { get; set; }
        [Display(Name = "Wybieg")]
        public int runwayID { get; set; }

        public virtual Runway Runway { get; set; }
        public virtual Spiece Spiece1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseHistory> DiseaseHistories { get; set; }
    }
    public enum Gender
    {
        
        Samiec,
        Samica
    }
}
