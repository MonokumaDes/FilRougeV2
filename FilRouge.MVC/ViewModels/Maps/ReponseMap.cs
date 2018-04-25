using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;

namespace FilRouge.Web.ViewModels.Maps
{
    public static class ReponseMap
    {
        /// <summary>
        /// Convertir "Reponses" en "ReponseViewModel"
        /// </summary>
        /// <param name="reponse"></param>
        /// <returns></returns>
        public static ReponseViewModel MapToReponseViewModel(this Reponses reponse)
        {
            var reponseViewModel = new ReponseViewModel();
            if (reponse == null)
            {
                return reponseViewModel;
            }

            reponseViewModel = new ReponseViewModel
            {
                ReponseId = reponse.ReponseId,
                Content = reponse.Content,
                TrueReponse = reponse.TrueReponse
            };
            return reponseViewModel;
        }

        /// <summary>
        /// Convertir "ReponseViewModel" en "Reponses"
        /// </summary>
        /// <param name="reponseViewModel"></param>
        /// <returns></returns>
        public static Reponses MapToReponses(this ReponseViewModel reponseViewModel)
        {
            var reponse = new Reponses();
            if (reponseViewModel == null)
            {
                return reponse;
            }

            reponse = new Reponses()
            {
                ReponseId = reponseViewModel.ReponseId,
                Content = reponseViewModel.Content,
                TrueReponse = reponseViewModel.TrueReponse
            };
            return reponse;
        }
    }
}