//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhiteWorld.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class anamenu
    {
        public int Id { get; set; }
        public int UstMenuId { get; set; }
        public string Baslik { get; set; }
        public string Url { get; set; }
        public int Oncelik { get; set; }
        public bool Goster { get; set; }
        public Nullable<bool> Yeni { get; set; }
        public int DilKod { get; set; }
    }
}
