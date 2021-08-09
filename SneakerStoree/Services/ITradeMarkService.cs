using SneakerStoree.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Services
{
    public interface ITradeMarkService
    {
        Task<List<TradeMark>> GetTradeMarks();  
        Task<TradeMark> GetTradeMarkById(int tradeMarkId);
        Task<TradeMark> Create(TradeMark createtradeMark);
        Task<TradeMark> Modify(TradeMark modifytradeMark);
        Task<TradeMark> Remove(int traId);
        Task<TradeMark> Restore(int traId);
    }
}
