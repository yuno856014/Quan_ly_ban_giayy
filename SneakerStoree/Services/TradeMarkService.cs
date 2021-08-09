using Microsoft.EntityFrameworkCore;
using SneakerStoree.DBContexts;
using SneakerStoree.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Services
{
    public class TradeMarkService : ITradeMarkService
    {
        private readonly SneakerStoreDBContex context;

        public TradeMarkService(SneakerStoreDBContex context)
        {
            this.context = context;
        }
        public async Task<List<TradeMark>> GetTradeMarks()
        {
            return await context.TradeMarks.Include(b => b.Sneakers).ToListAsync();
        }
        public async Task<TradeMark> GetTradeMarkById(int tradeMarkId)
        {
            return await context.TradeMarks.Include(b => b.Sneakers).FirstOrDefaultAsync(t => t.TradeMarkId == tradeMarkId);
        }

        public async Task<TradeMark> Create(TradeMark createtradeMark)
        {
            try
            {
                context.Add(createtradeMark);
                var traId = await context.SaveChangesAsync();
                createtradeMark.TradeMarkId = traId;
                return createtradeMark;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<TradeMark> Modify(TradeMark modifytradeMark)
        {
            try
            {
                context.Attach(modifytradeMark);
                context.Entry<TradeMark>(modifytradeMark).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return modifytradeMark;
            }catch(Exception)
            {
                throw;
            }
        }

        public async Task<TradeMark> Remove(int traId)
        {
            try
            {
                var tra = await GetTradeMarkById(traId);
                tra.IsDeleted = true;
                context.Attach(tra);
                context.Entry<TradeMark>(tra).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return tra;

            }catch(Exception)
            {
                throw;
            }
        }

        public async Task<TradeMark> Restore(int traId)
        {
            try
            {
                var tra = await GetTradeMarkById(traId);
                tra.IsDeleted = false;
                context.Attach(tra);
                context.Entry<TradeMark>(tra).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return tra;
            }catch(Exception)
            {
                throw;
            }
        }
    }
}
