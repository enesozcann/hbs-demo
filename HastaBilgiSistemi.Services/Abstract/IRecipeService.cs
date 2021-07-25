using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Abstract
{
    public interface IRecipeService
    {
        Task<IDataResult<RecipeDto>> GetAsync(int recipeId);
        Task<IDataResult<RecipeListDto>> GetAllAsync();
        Task<IDataResult<RecipeListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<RecipeListDto>> GetAllRecipesByDiagnosticAsync(int diagnosticId);
        Task<IDataResult<RecipeDto>> AddAsync(RecipeAddDto recipeAddDto, int diagnosticId, int medicineId);
        Task<IDataResult<RecipeDto>> DeleteAsync(int recipeId); //isActive = false;
        Task<IDataResult<int>> CountAsync();
    }
}
