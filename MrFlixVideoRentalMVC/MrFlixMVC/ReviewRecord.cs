//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MrFlixMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReviewRecord
    {
        public int ReviewID { get; set; }
        public string MovieTitle { get; set; }
        public string MovieReview { get; set; }
    
        public virtual Movy Movy { get; set; }
    }
}
