using SneakerStoree.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Services
{
    public interface ISneakerService
    {
        Task<Sneaker> Create(Sneaker createSneaker);
        Task<Sneaker> GetSneakerById(int sneakerId);
        Task<Sneaker> Modify(Sneaker sneaker);
        Task<Sneaker> Remove(int sneakerId);
        Task<Sneaker> Restore(int sneakerId);
    }
}
