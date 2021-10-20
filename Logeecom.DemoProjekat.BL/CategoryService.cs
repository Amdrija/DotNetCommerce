using System;
using System.Collections.Generic;
using Logeecom.DemoProjekat.DAL.Repositories;
using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.Exceptions;
using Logeecom.DemoProjekat.DAL.QueryModels;
using Logeecom.DemoProjekat.DAL;

namespace Logeecom.DemoProjekat.BL.Services
{
    public class CategoryService
    {
        private readonly UnitOfWork unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int GetCategoryCount()
        {
            return this.unitOfWork.CategoryRepository.GetCategoryCount();
        }

        public Category GetCategory(int id)
        {
            return this.unitOfWork.CategoryRepository.GetCategory(id);
        }

        public IEnumerable<Category> GetCategories(CategoryQuery categoryQuery)
        {
            return this.unitOfWork.CategoryRepository.GetCategories(categoryQuery);
        }

        public Category CreateCategory(Category category)
        {
            Category createdCategory = this.unitOfWork.CategoryRepository.CreateCategory(category);

            try
            {
                this.unitOfWork.Save();
            }
            catch(UniqueConstraintException)
            {
                throw new CategoryCodeExistsException($"The category with the code: {category.Code} already exists. Try a different code.");
            }
            catch(ForeignKeyConstraintException)
            {
                throw new ParentCategoryNotFoundException($"Couldn't find the category with id: {category.ParentId}.");
            }

            return createdCategory;
        }

        public Category UpdateCategory(Category category)
        {
            if (this.HasCircularReference(category.Id, category.ParentId))
            {
                throw new CategoryCircularReferenceException($"Category with id = {category.Id} can't have a parentId {category.ParentId}, because it will be a child of its child.");
            }
            
            this.unitOfWork.CategoryRepository.UpdateCategory(category);
            
            try
            {
                this.unitOfWork.Save();
            }
            catch (UniqueConstraintException)
            {
                throw new CategoryCodeExistsException($"The category with the code: {category.Code} already exists. Try a different code.");
            }
            catch (ForeignKeyConstraintException)
            {
                throw new ParentCategoryNotFoundException($"Couldn't find the category with id: {category.ParentId}.");
            }
            catch(PrimaryKeyConstraintException)
            {
                throw new CategoryNotFoundException($"Category with the specified id: {category.Id} not found.");
            }
            
            return category;
        }

        public void DeleteCategory(int id)
        {
            Category category = this.unitOfWork.CategoryRepository.GetCategory(id);

            if(category == null)
            {
                throw new CategoryNotFoundException($"Category with the specified id: {id} not found.");
            }    

            this.unitOfWork.CategoryRepository.DeleteCategory(category);

            this.unitOfWork.Save();
        }

        private bool HasCircularReference(int id, int? parentId)
        {
            if(id == parentId)
            {
                return true;
            }

            while(parentId != null)
            {
                var category = this.unitOfWork.CategoryRepository.GetCategory((int)parentId);

                //If somehow the category with the specified parentId doesn't exist
                // it's better to throw an excpetion when accessing the category.Id
                // then to "silently" process the error and return something.
                if(category == null)
                {
                    throw new ParentCategoryNotFoundException($"Couldn't find the category with id: {parentId}.");
                }

                if(category.Id == id)
                {
                    return true;
                }
                parentId = category.ParentId;
            }

            return false;
        }
    }
}
