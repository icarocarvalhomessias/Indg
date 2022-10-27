namespace Trader.Api.Domain.Exceptions
{
    public enum TraderApiError
    {
        Item_name_not_Unique,
        Item_not_found,
        Item_inactive,
        Person_inactive,
        Person_not_found,
        Person_name_not_unique,
        change_Owner_with_item_different_name
    }
}
