using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Recommandation
{
    public interface IAlgorithmDataLogic
    {
          Task<ModelAlgorithm> GetDataAsync();
          Task<bool> CreateSuggestionAsync(int UserReciever, int UserSuggest);
    }
}
