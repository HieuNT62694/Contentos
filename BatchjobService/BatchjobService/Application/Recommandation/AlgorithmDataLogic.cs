using BatchjobService.Entities;
using BatchjobService.Models;
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
        public async Task<List<AlgorithmDataBeforeModel>> AlgorithmDataBefore()
        {
            var listIduser = await _context.UsersInteractions.Select(x => x.IdUser).Distinct().ToListAsync();
            var lstAlori = new List<AlgorithmDataBeforeModel>();
            var lstTest = new List<ListTaskModel>();
            foreach (var test in listIduser)
            {
                var userInterId = await _context.UsersInteractions.Where(x => x.IdUser == test).Select(x => new ListTaskModel
                {
                    IdUser = x.IdUser,
                    IdTask = _context.UsersInteractions.Where(z => z.IdUser == x.IdUser).Select(z => new TaskInterModel { Id = z.IdTask, Interaction = z.Interaction ?? 0 }).ToList()
                }).FirstOrDefaultAsync();
                lstTest.Add(userInterId);
            }


            var lstTagsum = new List<int>();
            foreach (var item in lstTest)
            {
                foreach (var item1 in item.IdTask)
                {
                    var lstTag = _context.TasksTags.Where(x => x.IdTask == item1.Id).Select(x => x.IdTag).ToList();
                    foreach (var item2 in lstTag)
                    {
                        if (lstAlori.Select(x => x.IdTag).Contains(item2) && lstAlori.Select(x => x.IdUser).Contains(item.IdUser))
                        {
                            lstAlori.Where(x => x.IdTag == item2 && x.IdUser == item.IdUser).FirstOrDefault().TimeInTeraction += item1.Interaction ?? 0;
                        }
                        else
                        {
                            var Alori = new AlgorithmDataBeforeModel { IdUser = item.IdUser };
                            Alori.IdTag = item2;
                            Alori.TimeInTeraction = Alori.TimeInTeraction + item1.Interaction ?? 0;
                            lstAlori.Add(Alori);
                        }

                    }

                }



            }


            return lstAlori;
        }
        public async void UpdateSuggestion()
        {
            _context.Personalizations.ToList().ForEach(x => x.IsSuggestion = false);
            await _context.SaveChangesAsync();
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
