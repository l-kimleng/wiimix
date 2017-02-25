using System;
using System.Collections.Generic;

namespace WiiMix.Data.Entities
{
    public class StockTransfer
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual ICollection<ItemTransfer> TransferItems { get; set; }
    }
}