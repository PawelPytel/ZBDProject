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
    
    public partial class DiseaseHistory
    {
        public int id { get; set; }
        public System.DateTime beginDate { get; set; }
        public int animalID { get; set; }
        public int diseaseId { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Disease Disease { get; set; }
    }
}
