using AutoMapper;
using Logeecom.DemoProjekat.BL.Services;
using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.PL.ViewModels.QueryModels;
using Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels;
using Logeecom.DemoProjekat.PL.ViewModels.SubmitionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DALCategoryQuery = Logeecom.DemoProjekat.DAL.QueryModels.CategoryQuery;

namespace Logeecom.DemoProjekat.PL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        // GET: <CategoryController>
        // For getting all root categories, specify parentId = 0.
        [HttpGet]
        public IEnumerable<CategoryResponseModel> Get([FromQuery] CategoryQuery categoryQuery)
        {
            DALCategoryQuery dalCategoryQuery = this.mapper.Map<DALCategoryQuery>(categoryQuery);
            IEnumerable<Category> categories = this.categoryService.GetCategories(dalCategoryQuery);

            return categories.Select(category => this.mapper.Map<CategoryResponseModel>(category));
        }

        [HttpGet]
        [Authorize]
        [Route("count")]
        public int GetCount()
        {
            return this.categoryService.GetCategoryCount();
        }

        // GET <CategoryController>/5
        [HttpGet("{id}")]
        public CategoryResponseModel Get(int id)
        {
            return this.mapper.Map<CategoryResponseModel>(this.categoryService.GetCategory(id));
        }

        // POST <CategoryController>
        [HttpPost]
        [Authorize]
        public CategoryResponseModel Post(CategorySubmitModel sourceCategory)
        {
            Category category = this.mapper.Map<Category>(sourceCategory);
            Category createdCategory = this.categoryService.CreateCategory(category);

            return this.mapper.Map<CategoryResponseModel>(createdCategory);
        }

        // PUT <CategoryController>/5
        [HttpPut("{id}")]
        [Authorize]
        public CategoryResponseModel Put(int id, CategorySubmitModel sourceCategory)
        {
            sourceCategory.Id = id;
            Category category = this.mapper.Map<Category>(sourceCategory);
            Category updatedCategory = this.categoryService.UpdateCategory(category);


            return this.mapper.Map<CategoryResponseModel>(updatedCategory);
        }

        // DELETE <CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public OkResult Delete(int id)
        {
            this.categoryService.DeleteCategory(id);

            return this.Ok();
        }
    }
}
