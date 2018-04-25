using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.ViewModels.Maps
{
    public static class DifficultyRateMap
    {
        /// <summary>
        /// Convertir une Difficulté de Quizz en View.
        /// </summary>
        /// <param name="difficultyRate"></param>
        /// <returns>New DifficultyRateViewModel</returns>
        public static DifficultyRateViewModel MapToDifficultyRateViewModel(this DifficultyRate difficultyRate)
        {
            var difficultyRateViewModel = new DifficultyRateViewModel();
            if (difficultyRate == null)
                return difficultyRateViewModel;
            else
            {
                difficultyRateViewModel = new DifficultyRateViewModel
                {
                    DifficultyId = difficultyRate.DifficultyId,
                    DifficultyMasterId = difficultyRate.DifficultyMasterId,
                    Rate = difficultyRate.Rate
                };
                return difficultyRateViewModel;
            }
        }

        /// <summary>
        /// Convertir une DifficultyViewModel en Difficulty de Quizz
        /// </summary>
        /// <param name="difficultyRateViewModel"></param>
        /// <returns>new DifficultyRate</returns>
        public static DifficultyRate MapToDifficultyRate(this DifficultyRateViewModel difficultyRateViewModel)
        {
            var difficultyRate = new DifficultyRate();
            if (difficultyRateViewModel == null)
                return difficultyRate;
            else
            {
                difficultyRate = new DifficultyRate
                {
                    DifficultyId = difficultyRateViewModel.DifficultyId,
                    DifficultyMasterId = difficultyRateViewModel.DifficultyMasterId,
                    Rate = difficultyRateViewModel.Rate
                };
                return difficultyRate;
            }
        }
    }
}