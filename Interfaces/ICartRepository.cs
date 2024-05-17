namespace Paris2024.Interfaces
{
    public interface ICartRepository
    {
        Task<int> GetCartItemCount(string userId = "");
        Task<Cart> GetUserCart();
        Task<Cart> GetCart(string userId);

        Task<int> AddItem(int offerId, int qty);
        Task<int> RemoveItem(int offerId);
       
       
        Task<bool> DoCheckout(CheckoutVM model);
    }
}
