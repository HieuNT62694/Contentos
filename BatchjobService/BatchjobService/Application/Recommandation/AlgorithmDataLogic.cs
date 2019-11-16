using BatchjobService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Recommandation
{
    public class AlgorithmDataLogic : IAlgorithmDataLogic
    {
        AlgorithmData rs = null;
        double[,] data;

        private readonly IModel _model;
        private readonly ContentoDbContext _context;

        public AlgorithmDataLogic(IModel model, ContentoDbContext context)
        {
            _model = model;
            _context = context;
        }
        private readonly Double NULL = Double.MinValue;


        public async Task<ModelAlgorithm> GetDataAsync()
        {
            var returnModel = new ModelAlgorithm();
            returnModel.users = await _model.GetRows();
            returnModel.tags = await _model.GetColumns();
            List<Users> ListUserInteraction;
            if (returnModel.users != null && returnModel.tags != null)
            {
                int NumCol = returnModel.tags.Count;
                int NumRow = returnModel.users.Count;
                data = new double[NumCol + 1, NumRow];
                for (int i = 0; i < NumCol; i++)
                {
                    ListUserInteraction = await _model.GetListUserInteraction(returnModel.tags[i].Id);
                    if (ListUserInteraction.Count > 0)
                    {
                        for (int j = 0; j < NumRow; j++)
                        {
                            TimeInteraction interaction = await _model.GetinteractionTime(returnModel.users[j].Id, returnModel.tags[i].Id);

                            if (interaction.IsChosen == false && interaction.time == 0 && interaction.IsSuggest == false)
                            {
                                data[i, j] = NULL;
                            }
                            else
                            {
                                data[i, j] = interaction.time;
                            }
                        }
                    }

                }
                rs = new AlgorithmData(data, returnModel.users, returnModel.tags);
                returnModel.data = data;
            }

            return returnModel;
        }
        public void UpdateSuggestion()
        {
            _context.Personalizations.ToList().ForEach(x => x.IsSuggestion = false);
        }
        public async Task<bool> CreateSuggestionAsync(int UserReciever, int UserSuggest)
        {
            List<Personalization> tagsOriginal= await _model.getListTagsAsync(UserReciever);
            List<Personalization> tagsSuggestion = await _model.getListTagsAsync(UserSuggest);
            bool value = true;
            int n = tagsSuggestion.Count;
            Personalization personalization = null;
            foreach (var item in tagsSuggestion)
            {
                if (!tagsOriginal.Any(o => o.TagId == item.TagId))
                {
                    personalization = new Personalization(UserReciever, item.TagId);
                     _context.Personalizations
                    .Where(p => p.IdUser == personalization.UserId && p.IdTag == personalization.TagId)
                    .FirstOrDefault().IsSuggestion = true;
                }
            }
            await _context.SaveChangesAsync();
            return value ;
        }
    }


}
