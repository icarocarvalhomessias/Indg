namespace Trader.Api.Domain.Models
{
    public class ItemTransfer
    {
        public int Id { get; set; }
        public int FromPersonId { get; set; }
        public int ToPersonId { get; set; }
        public int ItemId { get; set; }
    }
}
