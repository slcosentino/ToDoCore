using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Repositories;
using NUnit.Framework;
using ToDo.Entities;
using System.Linq;
using FluentAssertions;
using ToDo.Core;
using Assert = NUnit.Framework.Assert;
using ToDo.Service;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace ToDoService.Tests
{
    //[TestClass()]
    public class FolderServiceTests
    {
        private DbContextOptions<ContextDB> dbContextOptions = new DbContextOptionsBuilder<ContextDB>()
            .UseInMemoryDatabase(databaseName: "ToDo")
            .Options;
        private FolderService service;
        private UnitOfWork unitOfWork;
        private ContextDB context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new ContextDB(dbContextOptions);
            unitOfWork = new UnitOfWork(context);
            SeedDb();
            service = new FolderService(unitOfWork);

        }

        private void SeedDb()
        {
            var rootFolder = new Folder
            {
                Id = 1,
                Name = "Root",
                Enabled = true
            };

            context.Folders.Add(rootFolder);

            context.SaveChanges();
        }

        private Folder AddFolderTest() 
        {
            string folderName = Guid.NewGuid().ToString();
            var newFolder = new Folder() { Name = folderName, Enabled = true };
            context.Folders.Add(newFolder);
            context.SaveChanges();

            return newFolder;
        }

        [Test]
        public async Task AddAsync_OK()
        {
            string folderName = Guid.NewGuid().ToString();
            var newFolder = new Folder() { Name = folderName };
            
            await service.AddAsync(newFolder);            
            var folder = context.Folders.Where(e => e.Name.Equals(folderName)).SingleOrDefault();

            folder.Should().NotBeNull();
            folder.Name.Should().BeEquivalentTo(folderName);
            folder.Enabled.Should().BeTrue();
        }

        [Test]
        public void AddAsync_FieldError_LogicException()
        {
            var newFolder = new Folder();
            Assert.ThrowsAsync<LogicException>(async () => await service.AddAsync(newFolder));
        }

        [Test]
        public async Task UpdateAsync_OK()
        {
            string folderName = Guid.NewGuid().ToString();
            var folderTestAdded = AddFolderTest();
            folderTestAdded.Name = folderName;

            await service.UpdateAsync(folderTestAdded);

            var folder = context.Folders.Where(x => x.Id == folderTestAdded.Id).SingleOrDefault();

            folder.Should().NotBeNull();
            folder.Name.Should().BeEquivalentTo(folderName);
            folder.Enabled.Should().BeTrue();
        }

        [Test]
        public void UpdateAsync_EntityNotExist_LogicException()
        {
            var folderNotExist = new Folder() { Name = "NotExist", Id = 1356 };
            Assert.ThrowsAsync<LogicException>(async () => await service.UpdateAsync(folderNotExist));
        }

        [Test]
        public void UpdateAsync_FieldError_LogicException()
        {
            var folderTestAdded = AddFolderTest();
            folderTestAdded.Name = null;
            Assert.ThrowsAsync<LogicException>(async () => await service.UpdateAsync(folderTestAdded));
        }

        [Test]
        public async Task DeleteAsync_OK()
        {
            string folderName = Guid.NewGuid().ToString();
            var folderTestAdded = AddFolderTest();          

            await service.DeleteAsync(folderTestAdded);

            var folder = context.Folders.Where(x => x.Id == folderTestAdded.Id).SingleOrDefault();

            folder.Should().NotBeNull();            
            folder.Enabled.Should().BeFalse();
        }

        [Test]
        public void DeleteAsync_EntityNotExist_LogicException()
        {
            Folder folderNotExist = null;
            Assert.ThrowsAsync<LogicException>(async () => await service.DeleteAsync(folderNotExist));
        }

  
        //ToDo: Check what happen within or without todos in the folder

        //[Test]
        //public void AddAsync_TryToAddExistId()
        //{
        //    var folder = context.Folders.Where(e => e.Enabled).SingleOrDefault();
        //    var newFolder = new Folder() { Name = folder.Name, Id = folder.Id };
        //    Assert.ThrowsAsync<InvalidOperationException>(async () => await service.AddAsync(newFolder));
        //}


    }

    //Pagination pagination = new Pagination();
    //pagination.Page = 1;
    //pagination.RecordsPerPage = 10;

    //var folders = await service.GetAllAsync(pagination);
    /*

    public void DeleteAsync()
    {

    }

    public void GetAllAsync()
    {

    }

    public void GetByIdAsync()
    {

    }

    public void UpdateAsync()
    {

    }

    public void CountAsync()
    {

    }*/


}