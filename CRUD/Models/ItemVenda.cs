//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemVenda
    {
        public int idItemVenda { get; set; }
        public int idVenda { get; set; }
        public int idProduto { get; set; }
        public int qtd { get; set; }
        public int valor { get; set; }
    
        public virtual Produto Produto { get; set; }
        public virtual Venda Venda { get; set; }
    }
}
