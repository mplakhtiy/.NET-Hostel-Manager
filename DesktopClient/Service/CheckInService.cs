﻿using System;
using DesktopClient.Model;
using DesktopClient.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DesktopClient.Service
{
    public interface ICheckInService
    {
        Task<List<CheckIn>> GetPendingCheckOutAsync();

        Task<bool> CreateAsync(CheckIn checkIn);

        Task<bool> DoCheckOutAsync(CheckIn checkIn);
    }

    public class CheckInService : ICheckInService
    {
        private ICheckInRepository _checkInRepo;

        private IBedroomRepository _bedroomRepo;

        public CheckInService()
        {
            _checkInRepo = new CheckInRepository();
            _bedroomRepo = new BedroomRepository();
        }

        public async Task<List<CheckIn>> GetPendingCheckOutAsync()
        {
            try
            {
                return await _checkInRepo.FindWithPendingCheckOutAsync();
            }
            catch (Exception)
            {
                return null;
            }    
        }

        public async Task<bool> CreateAsync(CheckIn checkIn)
        {
            checkIn.Active = true;
            checkIn.Bedroom.Available = false;
            try
            {
                bool created = await _checkInRepo.InsertOrUpdateAsync(checkIn);
                return created;
            }
            catch (Exception)
            {
                return false;
            }   
        }

        public async Task<bool> DoCheckOutAsync(CheckIn checkIn)
        {
            checkIn.Active = false;
            checkIn.Bedroom.Available = true;
            try
            {
                bool updated = await _checkInRepo.InsertOrUpdateAsync(checkIn);
                return updated;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
