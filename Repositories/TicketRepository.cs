using Microsoft.EntityFrameworkCore;

namespace Paris2024.Repositories;
public class TicketRepository

{
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context)
    {
        _context = context;
    }


}

