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
    
    public partial class MovieLineItem
    {
        public string MovieTitle { get; set; }
        public int OrderID { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual CheckOutRecord CheckOutRecord { get; set; }
        public virtual Movy Movy { get; set; }
    }
}