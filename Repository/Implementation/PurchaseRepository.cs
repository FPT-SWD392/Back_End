﻿using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IGenericDao<Purchase> _dao;
        public PurchaseRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<Purchase>();
        }

        public async Task CreateNewPurchase(Purchase purchase)
        {
            await _dao.CreateAsync(purchase);
        }

        public async Task DeletePurchase(Purchase purchase)
        {
            await _dao.DeleteAsync(purchase);
        }

        public async Task<List<Purchase>> GetAllArtPurchases(int artId)
        {
            return await _dao
                .Where(x => x.ArtId == artId)
                .ToListAsync();
        }

        public async Task<List<Purchase>> GetAllUserPurchases(int userId)
        {
            return await _dao
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Purchase?> GetPurchaseById(int purchaseId)
        {
            return await _dao
                .Where(x => x.PurchaseId == purchaseId)
                .SingleOrDefaultAsync();
        }

        public async Task UpdatePurchase(Purchase purchase)
        {
            await _dao.UpdateAsync(purchase);
        }
    }
}
