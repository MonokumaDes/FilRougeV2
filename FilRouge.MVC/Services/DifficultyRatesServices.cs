using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels.Maps;

namespace FilRouge.MVC.Services
{
    public class DifficultyRatesServices
    {

        public List<DifficultyRate> GetAllDifficultyRates()
        {
            var difficultyRateViewModel = new List<DifficultyRate>();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.DifficultyRates.Include("Difficulty").Include("DifficultyMaster").ToList();

                foreach (var difficulty in difficultyEntities)
                {

                    difficultyRateViewModel.Add(difficulty);
                }
            }

            return difficultyRateViewModel;
        }

        public DifficultyRate GetDifficultyRateById(int idMaster, int id)
        {
            var difficultyRate = new DifficultyRate();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyRateEntities = dbContext.Difficulties.Find(idMaster).DifficultyId;
                var difficultyMasterRateEntities = dbContext.Difficulties.Find(id).DifficultyId;
                var rateDifficulty = from diff in dbContext.DifficultyRates.Include("Difficulty").Include("DifficultyMaster")
                           where (idMaster == difficultyRateEntities && id == difficultyMasterRateEntities)
                           select (diff);
                difficultyRate.DifficultyId = rateDifficulty.First().DifficultyId;
                difficultyRate.DifficultyMasterId = rateDifficulty.First().DifficultyMasterId;
                difficultyRate.Rate = rateDifficulty.First().Rate;
                difficultyRate.Difficulty = rateDifficulty.First().Difficulty;
                difficultyRate.DifficultyMaster = rateDifficulty.First().DifficultyMaster;
            }

            return difficultyRate;
        }

    }
}