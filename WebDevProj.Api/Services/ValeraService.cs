using Microsoft.EntityFrameworkCore;
using WebDevProj.Api.Data;
using WebDevProj.Api.Models;

namespace WebDevProj.Api.Services;

public class ValeraService : IValeraService
{
    private readonly AppDbContext _context;

    public ValeraService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Valera>> GetAllAsync()
    {
        return await _context.Valeras.ToListAsync();
    }

    public async Task<Valera?> GetByIdAsync(int id)
    {
        return await _context.Valeras.FindAsync(id);
    }

    public async Task<Valera> CreateAsync(Valera valera)
    {
        _context.Valeras.Add(valera);
        await _context.SaveChangesAsync();
        return valera;
    }

    public async Task<Valera?> UpdateAsync(int id, Valera valera)
    {
        var existing = await _context.Valeras.FindAsync(id);
        if (existing == null) return null;

        // Копируем поля (Id не трогаем)
        existing.Health = valera.Health;
        existing.Alcohol = valera.Alcohol;
        existing.Cheer = valera.Cheer;
        existing.Fatigue = valera.Fatigue;
        existing.Money = valera.Money;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var valera = await _context.Valeras.FindAsync(id);
        if (valera == null) return false;

        _context.Valeras.Remove(valera);
        await _context.SaveChangesAsync();
        return true;
    }
}
