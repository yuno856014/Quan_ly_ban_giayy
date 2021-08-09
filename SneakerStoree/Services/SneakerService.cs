using Microsoft.EntityFrameworkCore;
using SneakerStoree.DBContexts;
using SneakerStoree.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Services
{
    public class SneakerService : ISneakerService
    {
        private readonly SneakerStoreDBContex context;

        public SneakerService(SneakerStoreDBContex context)
        {
            this.context = context;
        }
        public async Task<Sneaker> Create(Sneaker createSneaker)
        {
            try
            {
                context.Add(createSneaker);
                var sneakerId = await context.SaveChangesAsync();
                createSneaker.SneakerId = sneakerId;
                return createSneaker;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Sneaker> GetSneakerById (int sneakerId)
        {
            return await context.Sneakers.Include(t => t.TradeMarks).FirstOrDefaultAsync(s => s.SneakerId == sneakerId);
        }

        public async Task<Sneaker> Modify(Sneaker sneaker)
        {
            try
            {
                context.Attach(sneaker);
                context.Entry<Sneaker>(sneaker).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return sneaker;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Sneaker> Remove(int sneakerId)
        {
            try 
            {
                var sneaker = await GetSneakerById(sneakerId);
                sneaker.IsDeleted = true;
                context.Attach(sneaker);
                context.Entry<Sneaker>(sneaker).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return sneaker;
            }catch(Exception)
            {
                throw;
            }
        }

        public async Task<Sneaker> Restore(int sneakerId)
        {
            try
            {
                var sneaker = await GetSneakerById(sneakerId);
                sneaker.IsDeleted = false;
                context.Attach(sneaker);
                context.Entry<Sneaker>(sneaker).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return sneaker;
            }catch(Exception)
            {
                throw;
            }
        }
    }
}
