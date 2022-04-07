using Catalog.Worker.Data;
using Catalog.Worker.Data.Context.Interfaces;
using Catalog.Worker.Data.Entities;
using Catalog.Worker.Data.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Worker.Services
{
    class CatalogService
    {
        private readonly ILogger<CatalogService> _logger;
        private readonly ICatalogContext _context;

        public CatalogService(ICatalogContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CatalogService>();

            _logger.LogInformation("Connecting to mongodb...");
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger.LogInformation("Connected to mongodb");
        }

        public void AddProduct(ProductModel item)
        {
            _logger.LogInformation($"Adding item {item.Name}");
            var category = GetCategoryById(item.CategoryId);
            if (category != null)
            {
                AddProductToCategory(item, category);
                Save(category);
            }
            else
                Fail($"Item {item.Name} was not added: catategory doesn't exist");
        }

        public void UpdateProduct(ProductModel item)
        {
            _logger.LogInformation($"Updating item {item.Id} - {item.Name}");
            var category = GetCategoryById(item.CategoryId);
            if (category != null)
            {
                var oldItem = category.Items.FirstOrDefault(x => x.Id == item.Id);
                //если категория изменилась, удалить из старой, добавить в новую
                if (oldItem == null)
                {
                    _logger.LogInformation($"Item {item.Id} was possibly moved to another category");
                    var wrongCategory = GetCategoryOfItem(item.Id);
                    if (wrongCategory != null)
                    {
                        RemoveProductFromCategory(item, wrongCategory);
                        Save(wrongCategory);
                    }
                    AddProductToCategory(item, category);
                }
                else //если не изменилась обновить
                {
                    RemoveProductFromCategory(oldItem, category);
                    AddProductToCategory(item, category);
                }
                Save(category);
            }
            else
            {
                Fail($"Item {item.Id} was not updated: category {item.CategoryId} doesn't exist");
            }
        }

        public void DeleteProduct(ProductModel item)
        {
            _logger.LogInformation($"Deleting item {item.Id} - {item.Name}");
            var category = GetCategoryById(item.CategoryId);
            if (category != null)
            {
                RemoveProductFromCategory(item, category);
                Save(category);
            }
            else
            {
                Fail($"Item {item.Id} was not deleted: catategory doesn't exist");
            }
        }

        public void AddCategory(CategoryModel category)
        {
            _logger.LogInformation($"Adding category {category.Name}");
            CreateCategory(category);
            AddToParent(category, category.ParentId);
        }

        public void UpdateCategory(CategoryModel category)
        {
            _logger.LogInformation($"Updating category {category.Id} - {category.Name}");
            var oldCategory = GetCategoryById(category.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
                Save(oldCategory);

                if (oldCategory.Parent != category.ParentId)
                {
                    _logger.LogInformation($"Category {category.Id} was moved");
                    oldCategory.Parent = category.ParentId;
                    var wrongParent = GetParentOfCategory(category.Id);
                    if (wrongParent != null)
                    {
                        RemoveCategoryFromParent(category, wrongParent);
                        Save(wrongParent);
                    }
                    AddToParent(category, category.ParentId);
                }
            }
            else
                AddCategory(category);
        }

        public void DeleteCategory(CategoryModel category)
        {
            DeleteCategory(category.Id);
            var parent = GetParentOfCategory(category.Id);
            if (parent != null)
            {
                RemoveCategoryFromParent(category, parent);
                Save(parent);
            }
        }

        private void AddToParent(CategoryModel category, int parentId)
        {
            var parent = GetCategoryById(parentId);
            if (parent != null && !parent.ChildCategories.Any(x => x.Id == category.Id))
            {
                AddCategoryToParent(category, parent);
                Save(parent);
            }
            else if (parentId == 0)
            {
                _logger.LogInformation($"Creating root category");
                var root = CategoryAssortment.Root();
                AddCategoryToParent(category, root);
                _context.Catalog.InsertOne(root);
            }
        }

        private CategoryAssortment GetCategoryById(int id)
        {
            return _context.Catalog.Find(c => c.Id == id).FirstOrDefault();
        }

        private void CreateCategory(CategoryModel category)
        {
            _logger.LogInformation($"Inserting category {category.Id} - {category.Name}");
            _context.Catalog.InsertOne(CategoryAssortment.From(category));
        }

        private void AddCategoryToParent(CategoryModel category, CategoryAssortment parent)
        {
            _logger.LogInformation($"Attaching category {category.Id} ({category.Name}) to parent {parent.Id}, {parent.Name}");
            parent.ChildCategories.Add(Category.From(category));
        }

        private void RemoveCategoryFromParent(CategoryModel category, CategoryAssortment parent)
        {
            _logger.LogInformation($"Removing category {category.Id} ({category.Name}) from parent {parent.Id}, {parent.Name}");
            parent.ChildCategories.RemoveAll(x => x.Id == category.Id);
        }

        private void DeleteCategory(int id)
        {
            _logger.LogInformation($"Deleting category {id}");
            _context.Catalog.DeleteOne(x => x.Id == id);
        }

        private CategoryAssortment GetParentOfCategory(int categoryId)
        {
            return _context.Catalog.Find(c => c.ChildCategories.Any(x => x.Id == categoryId)).FirstOrDefault();
        }

        private void AddProductToCategory(ProductModel item, CategoryAssortment category)
        {
            _logger.LogInformation($"Adding item {item.Id} - {item.Name} to category {category.Id} - {category.Name}");
            category.Items.Add(Product.From(item));
        }

        private void RemoveProductFromCategory(Product item, CategoryAssortment category)
        {
            _logger.LogInformation($"Removing item {item.Id} - {item.Name} from category {category.Id} - {category.Name}");
            category.Items.RemoveAll(x => x.Id == item.Id);
        }

        private void RemoveProductFromCategory(ProductModel item, CategoryAssortment category)
        {
            _logger.LogInformation($"Removing item {item.Id} - {item.Name} from category {category.Id} - {category.Name}");
            category.Items.RemoveAll(x => x.Id == item.Id);
        }

        private CategoryAssortment GetCategoryOfItem(int itemId)
        {
            return _context.Catalog.Find(c => c.Items.Any(c => c.Id == itemId)).FirstOrDefault();
        }

        private void Save(CategoryAssortment category)
        {
            _context.Catalog.ReplaceOne(x => x.Id == category.Id, category);
            _logger.LogInformation($"Category {category.Id} updated");
        }

        private void Fail(string message = "failed")
        {
            _logger.LogWarning(message);
        }

    }
}
