//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public System.Guid SessionID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ItemID { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}
