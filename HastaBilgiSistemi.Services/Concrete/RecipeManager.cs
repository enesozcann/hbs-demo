using AutoMapper;
using HastaBilgiSistemi.Data.Abstract;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Services.Abstract;
using HastaBilgiSistemi.Services.Utilities;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using HastaBilgiSistemi.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Concrete
{
    public class RecipeManager : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RecipeManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<RecipeDto>> AddAsync(RecipeAddDto recipeAddDto, int diagnosticId, int medicineId)
        {
            var recipe = _mapper.Map<Recipe>(recipeAddDto);
            recipe.DiagnosticId = diagnosticId;
            recipe.MedicineId = medicineId;
            var addedRecipe = await _unitOfWork.Recipes.AddAsync(recipe);
            await _unitOfWork.SaveAsync();
            return new DataResult<RecipeDto>(ResultStatus.Success, message: Messages.Recipe.Add(addedRecipe.CreatedDate.ToString()), data: new RecipeDto
            {
                Recipe = addedRecipe,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Recipe.Add(addedRecipe.CreatedDate.ToString())
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var recipesCount = await _unitOfWork.Recipes.CountAsync();
            if (recipesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, recipesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Base.UnExpectedError(), data: -1);
            }
        }

        public async Task<IDataResult<RecipeDto>> DeleteAsync(int recipeId)
        {
            var recipe = await _unitOfWork.Recipes.GetAsync(predicate: a => a.Id == recipeId);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                var deletedRecipe = await _unitOfWork.Recipes.UpdateAsync(recipe);
                await _unitOfWork.SaveAsync();
                return new DataResult<RecipeDto>(ResultStatus.Success, Messages.Recipe.Delete(deletedRecipe.CreatedDate.ToString()), data: new RecipeDto
                {
                    Recipe = deletedRecipe,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Recipe.Delete(deletedRecipe.CreatedDate.ToString())
                });
            }
            return new DataResult<RecipeDto>(ResultStatus.Success, message: Messages.Recipe.NotFound(false), data: new RecipeDto
            {
                Recipe = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Recipe.NotFound(false)
            });
        }

        public Task<IDataResult<RecipeListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<RecipeListDto>> GetAllByNonDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<RecipeListDto>> GetAllRecipesByDiagnosticAsync(int diagnosticId)
        {
            var recipes = await _unitOfWork.Recipes.GetAllAsync(predicate: a => a.DiagnosticId == diagnosticId && !a.IsDeleted, a => a.Medicine);
            if (recipes.Count > -1)
            {
                return new DataResult<RecipeListDto>(ResultStatus.Success, new RecipeListDto
                {
                    Recipes = recipes,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<RecipeListDto>(ResultStatus.Error, message: Messages.Recipe.NotFoundWithPredicate("tanıya"), data: null);
        }

        public async Task<IDataResult<RecipeDto>> GetAsync(int recipeId)
        {
            var recipe = await _unitOfWork.Recipes.GetAsync(predicate: a => a.Id == recipeId, a => a.Medicine);
            if (recipe != null)
            {
                return new DataResult<RecipeDto>(ResultStatus.Success, new RecipeDto
                {
                    Recipe = recipe,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<RecipeDto>(ResultStatus.Error, message: Messages.Recipe.NotFound(false), data: new RecipeDto
            {
                Recipe = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Recipe.NotFound(false)
            });
        }
    }
}
