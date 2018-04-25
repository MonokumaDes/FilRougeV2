using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.ViewModels.Maps
{
    public static class DifficultyMap
    {
        /// <summary>
        /// Convertir une Difficulté de Question en View.
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns>New DifficultyViewModel</returns>
        public static DifficultyViewModel MapToDifficultyViewModel(this Difficulty difficulty)
        {
            var difficultyViewModel = new DifficultyViewModel();
            if (difficulty == null)
                return difficultyViewModel;
            else
            {
                difficultyViewModel = new DifficultyViewModel
                {
                    DifficultyId = difficulty.DifficultyId,
                    DifficultyName = difficulty.DifficultyName
                };
                return difficultyViewModel;
            }
        }

        /// <summary>
        /// Convertir une DifficultyViewModel en Difficulty de Question
        /// </summary>
        /// <param name="difficultyViewModel"></param>
        /// <returns>new Difficulty</returns>
        public static Difficulty MapToDifficulty(this DifficultyViewModel difficultyViewModel)
        {
            var difficulty = new Difficulty();
            if (difficultyViewModel == null)
                return difficulty;
            else
            {
                difficulty = new Difficulty {
                    DifficultyId = difficultyViewModel.DifficultyId,
                    DifficultyName = difficultyViewModel.DifficultyName
                };
                return difficulty;
            }
        }
    }
}