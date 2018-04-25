using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.Web.ViewModels.Maps
{
    public static class DifficultyMasterMap
    {
        /// <summary>
        /// Convertir une Difficulté de Quizz en View.
        /// </summary>
        /// <param name="difficultyMaster"></param>
        /// <returns>New DifficultyMasterViewModel</returns>
        public static DifficultyMasterViewModel MapToDifficultyMasterViewModel(this Difficulty difficultyMaster)
        {
            var difficultyMasterViewModel = new DifficultyMasterViewModel();
            if (difficultyMaster == null)
                return difficultyMasterViewModel;
            else
            {
                difficultyMasterViewModel = new DifficultyMasterViewModel
                {
                    DiffMasterId = difficultyMaster.DifficultyId,
                    DiffMasterName = difficultyMaster.DifficultyName
                };
                return difficultyMasterViewModel;
            }
        }

        /// <summary>
        /// Convertir une DifficultyViewModel en Difficulty de Quizz
        /// </summary>
        /// <param name="difficultyViewModel"></param>
        /// <returns>new Difficulty</returns>
        public static Difficulty MapToDifficultyMaster(this DifficultyMasterViewModel difficultyMasterViewModel)
        {
            var difficultyMaster = new Difficulty();
            if (difficultyMasterViewModel == null)
                return difficultyMaster;
            else
            {
                difficultyMaster = new Difficulty
                {
                    DifficultyId = difficultyMasterViewModel.DiffMasterId,
                    DifficultyName = difficultyMasterViewModel.DiffMasterName
                };
                return difficultyMaster;
            }
        }
    }
}