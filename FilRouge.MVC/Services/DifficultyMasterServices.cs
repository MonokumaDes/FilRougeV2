using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.MVC.Services
{
    public class DifficultyMasterServices
    {
        public List<DifficultyMasterViewModel> GetAllDifficulties()
        {
            var difficultyViewModel = new List<DifficultyMasterViewModel>();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.Difficulties.ToList();

                foreach (var difficulty in difficultyEntities)
                {
                    difficultyViewModel.Add(difficulty.MapToDifficultyMasterViewModel());
                }
            }

            return difficultyViewModel;
        }

        public DifficultyMasterViewModel GetDifficultyById(int difficultyId)
        {
            var difficultyViewModel = new DifficultyMasterViewModel();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.Difficulties.Find(difficultyId);
                difficultyViewModel = difficultyEntities.MapToDifficultyMasterViewModel();
            }

            return difficultyViewModel;
        }

        public int AddDifficulty(DifficultyMasterViewModel difficultyViewModel)
        {
            int id = 0;

            using (var dbContext = new FilRougeDBContext())
            {
                var difficulty = difficultyViewModel.MapToDifficultyMaster();
                dbContext.Difficulties.Add(difficulty);
                dbContext.SaveChanges();
                id = difficulty.DifficultyId;
            }
            return id;
        }

        public int EditDifficulty(DifficultyMasterViewModel difficultyViewModel)
        {
            var id = 0;
            using (var dbContext = new FilRougeDBContext())
            {
                var difficulty = dbContext.Difficulties.Find(difficultyViewModel.DiffMasterId);
                difficulty.DifficultyId = difficultyViewModel.DiffMasterId;
                difficulty.DifficultyName = difficultyViewModel.DiffMasterName;
                dbContext.Entry(difficulty).State = EntityState.Modified;
                dbContext.SaveChanges();
                id = difficulty.DifficultyId;
            }
            return id;
        }
    }
}